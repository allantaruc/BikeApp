using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class BikesController : BaseApiController
    {
        private readonly DataContext _context;
        public BikesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bike>>> GetBikes() {
            return await _context.Bikes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(Guid id) {
            return await _context.Bikes.FindAsync(id);
        }
    }
}