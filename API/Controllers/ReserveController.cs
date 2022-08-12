using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReserveController : ControllerBase
    {
        private readonly DataContext _context;

        public ReserveController(DataContext context)
        {
            _context = context;
        }


        [HttpGet(Name = "GetReserved")]
        public async Task<ActionResult<ReserveDto>> GetReserved()
        {
            var reserve = await GetReserves();

            if (reserve == null) return NotFound();

            return MapReserveToDto(reserve);
        }

      

        [HttpPost]
        public async Task<ActionResult> JoinComute(int routeId, int quantity, DateTime departureTime){

            var reserve = await GetReserves();

            if(reserve == null) reserve = JoinCarpool();

            var route = await _context.Routes.FindAsync(routeId);

            if (route == null ) return NotFound();

            reserve.JoinComute(route, quantity, departureTime);

            var result = await _context.SaveChangesAsync() > 0 ;

            if (result) return CreatedAtRoute("GetReserved", MapReserveToDto(reserve));

            return BadRequest(new ProblemDetails{Title = "Failed to join carpool"});
        }

       

        [HttpDelete]
        public async Task<ActionResult> CancelComute(int routeId, int quantity){

            var reserve = await GetReserves();

            if (reserve == null) return NotFound();

            reserve.CancelComute(routeId, quantity);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails{Title = "Failed to cancel carpool"});
        }


           private async Task<Reserve> GetReserves()
        {
            return await _context.Reserves
                    .Include(i => i.Comutes)
                    .ThenInclude(r => r.Route)
                    .FirstOrDefaultAsync(x => x.UserId == Request.Cookies["userId"]);
        }

         private Reserve JoinCarpool()
        {
            var userId = Guid.NewGuid().ToString();

            var cookieOptions = new CookieOptions{IsEssential = true};

            Response.Cookies.Append("userId", userId, cookieOptions);

            var reserve = new Reserve{UserId = userId};

            _context.Reserves.Add(reserve);
            
            return reserve;
        }

          private ReserveDto MapReserveToDto(Reserve reserve)
        {
            return new ReserveDto
            {
                Id = reserve.Id,
                UserId = reserve.UserId,
                DateJoined = reserve.DateJoined,
                Comutes = reserve.Comutes.Select(comute => new ReserveComuteDto
                {

                    RouteId = comute.RouteId,
                    DepartureTime = comute.Route.DepartureTime,
                    ExpectedArrival = comute.Route.ExpectedArrival,
                    Origin = comute.Route.Origin,
                    Days = comute.Route.Days,
                    Destination = comute.Route.Destination,
                    AvailableSeats = comute.Route.AvailableSeats,
                    Notes = comute.Route.Notes,
                    AddedOn = comute.Route.AddedOn

                }).ToList()
            };
        }
    
    }
}