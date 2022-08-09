using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComutesController : ControllerBase
    {
        private readonly DataContext _context;

        public ComutesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Carpool>>> GetComutes(){

            return await  _context.Carpool.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carpool>> GetComute(int id){

            return await  _context.Carpool.FindAsync(id);
        }

    }
}