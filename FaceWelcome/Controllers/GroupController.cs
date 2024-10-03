using FaceWelcome.API.Constants;
using FaceWelcome.Service.DTOs.Request.Group;
using FaceWelcome.Service.DTOs.Request.Guest;
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
    }
}
