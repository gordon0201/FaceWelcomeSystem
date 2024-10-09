using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Request.Group;
using FaceWelcome.Service.DTOs.Request.Staff;
using FaceWelcome.Service.DTOs.Response.Group;
using FaceWelcome.Service.DTOs.Response.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IGroupService
    {
        public Task CreateGroupAsync(PostGroupRequest postGroupRequest);

        public Task UpdateGroupAsync(Guid id, UpdateGroupRequest updateGroupRequest);

        public Task<GetGroupResponse> GetGroupByIdAsync(Guid id);
        public Task<GetAllGroupsResponse> GetAllGroupsAsync(GetAllGroupsRequest getAllGroupsRequest);

        public Task DeleteGroupAsync(Guid id);
    }
}
