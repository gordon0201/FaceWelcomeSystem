using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.DTOs.Response.OrganizationGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IOrganizationGroupService
    {
        public Task CreateOrganizationGroupAsync(PostOrganizationGroupRequest postOrganizationGroupRequest);
        Task<GetOrganizationGroupResponse> GetOrganizationGroupByIdAsync(Guid id);

        Task UpdateOrganizationGroupAsync(Guid id, UpdateOrganizationGroupRequest updateOrganizationGroupRequest);

        Task DeleteOrganizationGroupAsync(Guid id);

        Task<GetAllOrganizationGroupsResponse> GetAllOrganizationGroupsAsync(GetAllOrganizationGroupsRequest getAllOrganizationGroupsRequest);
    }
}
