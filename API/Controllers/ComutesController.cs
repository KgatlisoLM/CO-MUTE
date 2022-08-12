using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComutesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ComutesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<PagedList<Route>>> GetComutes([FromQuery] RouteParams routeParams)
        {

            var query = _context.Routes
            .Sort(routeParams.OrderBy)
            .Search(routeParams.SearchTerm)
            .AsQueryable();

            var route = await PagedList<Route>.ToPagedList(query, routeParams.PageNumber, routeParams.PageSize);

            Response.AddPaginationHeader(route.MetaData);

            return route;

        }

        [HttpGet("{id}", Name = "GetComute")]
        public async Task<ActionResult<Route>> GetComute(int id)
        {

            var route = await _context.Routes.FindAsync(id);

            if (route == null) return NotFound();

            return route;
        }

        [HttpPost]
        public async Task<ActionResult<Route>> CreateRoute(CreateRouteDto routeDto)
        {

            var route = _mapper.Map<Route>(routeDto);

            _context.Routes.Add(route);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return CreatedAtRoute("GetComute", new { Id = route.Id }, route);

            return BadRequest(new ProblemDetails { Title = "Failed to create new comute" });
        }


        [HttpPut]
        public async Task<ActionResult> UpdateRoute(UpdateRouteDto routeDto){

            var route = await _context.Routes.FindAsync(routeDto.Id);

            if (route == null) return NotFound();

            _mapper.Map(routeDto, route);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return NoContent();

             return BadRequest(new ProblemDetails { Title = "Failed to update route" });
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteRoute(int id)
        {
            var  route = await _context.Routes.FindAsync(id);

            if (route == null)  return NotFound();

            _context.Routes.Remove(route);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Failed to delete comute" });
        }

    }
}