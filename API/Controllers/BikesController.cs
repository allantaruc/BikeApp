using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Bikes;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BikesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Bike>>> GetBikes()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bike>> GetBike(Guid id)
        {
            return await Mediator.Send(new Details.Query {Id = id});
        }

        [HttpPost]
        public async Task<IActionResult> CreateBike(Bike bike){
            return Ok(await Mediator.Send(new Create.Command { Bike = bike}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBike(Guid id, Bike bike) {
            bike.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Bike = bike}));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBike(Bike bike) {
            return Ok(await Mediator.Send(new Delete.Command { Id = bike.Id }));
        }
    }
}