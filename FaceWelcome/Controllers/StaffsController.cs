using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.DTOs.Request.Staff;
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
    }
}
