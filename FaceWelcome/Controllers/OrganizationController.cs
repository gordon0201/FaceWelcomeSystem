using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request;
using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    //[Route("api/v1/organizations")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            this._organizationService = organizationService;
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Organization.OrganizationEndpoint)]
        public async Task<IActionResult> GetOrganizationByCodeAsync([FromRoute] OrganizationCodeRequest organizationCodeRequest)
        {
            if (organizationCodeRequest == null)
            {
                return BadRequest("Invalid data");
            }
            try
            {
                var data =  await _organizationService.GetOrganizationByCodeAsync(organizationCodeRequest.Code);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
