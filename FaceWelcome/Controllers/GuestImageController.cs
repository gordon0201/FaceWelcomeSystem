using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.GuestImage;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    public class GuestImageController : ControllerBase
    {
        private IGuestImageService _guestImageService;

        public GuestImageController(IGuestImageService guestImageService)
        {
            _guestImageService = guestImageService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.GuestImage.GuestImagesEndpoint)]
        public async Task<IActionResult> PostCreateGuestImageAsync([FromForm] PostGuestImageRequest postGuestImageRequest)
        {
            if (postGuestImageRequest.ImageFile == null || postGuestImageRequest.ImageFile.Length == 0)
            {
                return BadRequest("Please upload an image file.");
            }

            if (postGuestImageRequest.guestId == Guid.Empty)
            {
                return BadRequest("Invalid guest ID.");
            }

            try
            {
                await this._guestImageService.CreateGuestImageAsync(postGuestImageRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi trong quá trình tạo guest image
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
