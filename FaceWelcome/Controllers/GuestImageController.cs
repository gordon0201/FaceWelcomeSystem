﻿using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.GuestImage;
using FaceWelcome.Service.DTOs.Response.GuestImage;
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

        #region Create Guest Image
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
        #endregion

        #region Get Guest Image By Id
        [ProducesResponseType(typeof(GetGuestImageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpGet(APIEndPointConstant.GuestImage.GuestImageEndpoint)]
        public async Task<IActionResult> GetGuestImageByIdAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var data = await _guestImageService.GetGuestImageByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update Guest Image
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPut(APIEndPointConstant.GuestImage.GuestImageEndpoint)]
        public async Task<IActionResult> UpdateGuestImageAsync([FromRoute] Guid id, [FromForm] UpdateGuestImageRequest updateGuestImageRequest)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid guest image ID");
            }

            try
            {
                await _guestImageService.UpdateGuestImageAsync(id, updateGuestImageRequest);
                return Ok("Guest image updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get All Guest Images
        [ProducesResponseType(typeof(List<GetGuestImageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpGet(APIEndPointConstant.GuestImage.GuestImagesEndpoint)]
        public async Task<IActionResult> GetAllGuestImagesAsync([FromQuery] GetAllGuestImagesRequest getAllGuestImagesRequest)
        {
            try
            {
                var data = await _guestImageService.GetAllGuestImagesAsync(getAllGuestImagesRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete Guest Image
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpDelete(APIEndPointConstant.GuestImage.GuestImageEndpoint)]
        public async Task<IActionResult> DeleteGuestImageAsync([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid guest image ID");
            }

            try
            {
                await _guestImageService.DeleteGuestImageAsync(id);
                return Ok("Guest image deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
