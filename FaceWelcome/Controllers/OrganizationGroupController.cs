using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.DTOs.Response.OrganizationGroup;
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

        #region Create Organization Group
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
        #endregion

        #region Get Organization Group By Id
        [ProducesResponseType(typeof(GetOrganizationGroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpGet(APIEndPointConstant.OrganizationGroup.OrganizationGroupEndpoint)]
        public async Task<IActionResult> GetOrganizationGroupByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var data = await _organizationGroupService.GetOrganizationGroupByIdAsync(id);
                if (data == null)
                {
                    return NotFound($"Organization Group with ID {id} not found.");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update Organization Group
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpPut(APIEndPointConstant.OrganizationGroup.OrganizationGroupEndpoint)]
        public async Task<IActionResult> UpdateOrganizationGroupAsync([FromRoute] Guid id, [FromForm] UpdateOrganizationGroupRequest updateOrganizationGroupRequest)
        {
            try
            {
                await _organizationGroupService.UpdateOrganizationGroupAsync(id, updateOrganizationGroupRequest);
                return Ok("Update Organization Group successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete Organization Group
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpDelete(APIEndPointConstant.OrganizationGroup.OrganizationGroupEndpoint)]
        public async Task<IActionResult> DeleteOrganizationGroupAsync([FromRoute] Guid id)
        {
            try
            {
                await _organizationGroupService.DeleteOrganizationGroupAsync(id);
                return Ok("Organization Group deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get All Organization Groups
        [ProducesResponseType(typeof(GetAllOrganizationGroupsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.OrganizationGroup.OrganizationGroupsEndpoint)]
        public async Task<IActionResult> GetAllOrganizationGroupsAsync([FromQuery] GetAllOrganizationGroupsRequest getAllOrganizationGroupsRequest)
        {
            try
            {
                var data = await _organizationGroupService.GetAllOrganizationGroupsAsync(getAllOrganizationGroupsRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
