using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Group;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Group;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.Services.Implementations;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FaceWelcome.API.Controllers
{
    [ApiController]
    public class GroupController : Controller
    {
        
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            this._groupService = groupService;
        }
        #region Create Group
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.Group.GroupsEndpoint)]
        public async Task<IActionResult> PostCreateGroupAsync([FromForm] PostGroupRequest postGroupRequest)
        {
            if (postGroupRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            try
            {
                await this._groupService.CreateGroupAsync(postGroupRequest);
                return Ok();  // Trả về HTTP 200 OK khi sự kiện được tạo thành công
            }
            catch (Exception ex)
            {
                // Trả về mã lỗi 500 nếu có lỗi trong quá trình tạo sự kiện
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Update Group
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPut(APIEndPointConstant.Group.GroupEndpoint)]
        public async Task<IActionResult> UpdateGroupAsync([FromRoute] GroupIdRequest groupIdRequest, [FromForm] UpdateGroupRequest updateGroupRequest)
        {
            try
            {
                await this._groupService.UpdateGroupAsync(groupIdRequest.Id, updateGroupRequest);
                return Ok("Update Group successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get Group By Id
        [ProducesResponseType(typeof(GetGroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Group.GroupEndpoint)]
        public async Task<IActionResult> GetGroupByIdAsync([FromRoute] GroupIdRequest groupIdRepuest)
        {
            try
            {
                var data = await _groupService.GetGroupByIdAsync(groupIdRepuest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region Get All Groups
        [ProducesResponseType(typeof(GetGroupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Group.GroupsEndpoint)]
        public async Task<IActionResult> GetAllGroupsAsync([FromQuery] GetAllGroupsRequest getAllGroupsRequest)
        {
            try
            {
                var data = await _groupService.GetAllGroupsAsync(getAllGroupsRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete Group
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [HttpDelete(APIEndPointConstant.Group.GroupEndpoint)]
        public async Task<IActionResult> DeleteGroupAsync([FromRoute] GroupIdRequest groupIdRequest)
        {
            try
            {
                await _groupService.DeleteGroupAsync(groupIdRequest.Id);
                return Ok("Group deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


    }
}
