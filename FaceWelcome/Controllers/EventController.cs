using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaceWelcome.Service.Services.Interfaces;
using FaceWelcome.API.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FaceWelcome.Service.DTOs.Request.Event;


namespace FaceWelcome.API.Controllers
{
    [ApiController]

    public class EventController : ControllerBase
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.Event.EventsEndpoint)]
        public async Task<IActionResult> PostCreateEventAsync([FromForm] PostEventRequest postEventRequest)
        {
            if (postEventRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            try
            {
                await this._eventService.CreateEventAsync(postEventRequest);
                return Ok();  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }

}
