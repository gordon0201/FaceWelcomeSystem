using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    public class OrganizationGroupController : Controller
    {
        private IOrganizationGroupService _organizationGroupService;
        public OrganizationGroupController(IOrganizationGroupService organizationGroupService)
        {
            this._organizationGroupService = organizationGroupService;
        }
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.OrganizationGroup.OrganizationGroupsEndpoint)]
        public async Task<IActionResult> PostCreateOrganizationGroupAsync([FromForm] PostOrganizationGroupRequest postOrganizationGroupRequest)
        {
            if (postOrganizationGroupRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            try
            {
                await this._organizationGroupService.CreateOrganizationGroupAsync(postOrganizationGroupRequest);
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
