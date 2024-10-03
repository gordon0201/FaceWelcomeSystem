using Azure.Messaging;
using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Response.Guest;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    public class GuestsController : Controller
    {
        private IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            this._guestService = guestService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.Guest.GuestsEndpoint)]
        public async Task<IActionResult> PostCreateGuestAsync([FromForm] PostGuestRequest postGuestRequest)
        {
            if (postGuestRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            try
            {
                await this._guestService.CreateGuestAsync(postGuestRequest);
                return Ok();  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi trong quá trình tạo sự kiện
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPut(APIEndPointConstant.Guest.GuestEndpoint)]
        public async Task<IActionResult> UpdateGuestAsync([FromRoute] GuestRequest guestRequest, [FromForm] UpdateGuestRequest updateGuestRequest)
        {
            if (updateGuestRequest == null)
            {
                return BadRequest("Invalid guest data");
            }

            try
            {
                await this._guestService.UpdateGuestAsync(guestRequest.Id,updateGuestRequest);
                return Ok();  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [ProducesResponseType(typeof(GetGuestsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Guest.GuestsEndpoint)]
        public async Task<IActionResult> GetGuestsAsync()
        {
            try
            {
                var data = await this._guestService.GetGuestsAsync();
                return Ok(data);  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [ProducesResponseType(typeof(GetGuestsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Guest.GuestEndpoint)]
        public async Task<IActionResult> GetGuestByIdAsync([FromRoute] GuestRequest guestRequest)
        {
            try
            {
                var data = await this._guestService.GetGuestByIdAsync(guestRequest);
                return Ok(data);  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
