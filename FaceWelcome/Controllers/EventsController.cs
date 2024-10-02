using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaceWelcome.Service.Services.Interfaces;
using FaceWelcome.Service.DTOs.Request;
using FaceWelcome.API.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace FaceWelcome.API.Controllers
{
    [ApiController]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {
        private IEventService _eventService;

        public EventsController(IEventService eventService)
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
                return Ok();  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi trong quá trình tạo sự kiện
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
