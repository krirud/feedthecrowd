using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Events;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedTheCrowd.Controllers
{
    [Authorize]
    [Route("api/events")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser(int id)
        {
            var events = await _eventService.GetByUser(id);
            return Ok(events);
        }
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] NewEventDto newEventDto)
        {
            var createEvent = await _eventService.Create(newEventDto);

            return Ok(createEvent);
        }

         [HttpGet("{id}")]
         public async Task<ActionResult<Event>> GetEvent(int id)
         {
             var ev = await _eventService.GetById(id);
             if (ev == null)
             {
                 return NotFound();
             }

             return Ok(ev);
        }
         [HttpDelete("{id}")]
         public async Task<ActionResult<Event>> Delete(int id)
         {
             var success = await _eventService.Delete(id);
             if (success == null)
                 return BadRequest("Cannot delete recipe");

             return NoContent();
         }
         [HttpPut("{id}")]
         public async Task<IActionResult> Update(int id, [FromBody] NewEventDto newEvent)
         {
             await _eventService.Update(id, newEvent);
             return NoContent();
        }
        [HttpPost("recipe/{id}/{eventId}")]
        public async Task<IActionResult> AddRecipeToEvent(int id, int eventId)
        {
             await _eventService.AddRecipeToEvent(id, eventId);

            return NoContent();

        }
        [HttpDelete("recipe/{id}/{eventId}")]
        public async Task<IActionResult> RemoveRecipeFromEvent(int id, int eventId)
        {
            await _eventService.RemoveRecipeFromEvent(id, eventId);
            return NoContent();
        }

    }
}
