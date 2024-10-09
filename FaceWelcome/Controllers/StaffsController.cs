using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Request.Staff;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.Services.Implementations;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    
    [ApiController]
    [Route("api/v1/staffs")]

    public class StaffsController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        #region Get Staff By Id
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Staff.StaffEndpoint)]
        public async Task<IActionResult> GetStaffByIdAsync([FromRoute]StaffIdRequest staffIdRequest)
        {

            if (staffIdRequest == null)
            {
                return BadRequest("Invalid data");
            }
            try
            {
                var data = await _staffService.GetStaffByIdAsync(staffIdRequest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update Staff
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPut(APIEndPointConstant.Staff.StaffEndpoint)]
        public async Task<IActionResult> UpdateStaffAsync([FromRoute] StaffIdRequest staffIdRequest, [FromForm] UpdateStaffRequest updateStaffRequest)
        {
            try
            {
                await this._staffService.UpdateStaffAsync(staffIdRequest.Id, updateStaffRequest);
                return Ok("Update Person successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Create Staff
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.Staff.StaffsEndpoint)]
        public async Task<IActionResult> PostStaffAsync([FromForm] PostStaffRequest postStaffRequest)
        {
            try
            {
                await _staffService.PostStaffAsync(postStaffRequest);
                return Ok("Create new Staff successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region Get All Staffs
        [ProducesResponseType(typeof(GetPersonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Staff.StaffsEndpoint)]
        public async Task<IActionResult> GetAllStaffsAsync([FromQuery] GetAllStaffsRequest getAllStaffsRequest)
        {
            try
            {
                var data = await _staffService.GetAllStaffsAsync(getAllStaffsRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
