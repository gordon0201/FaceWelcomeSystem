using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.DTOs.Response.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IOrganizationService
    {
        public Task CreateOrganizationAsync(PostOrganizationRequest request);
        public Task<GetOrgsResponse> GetAllOrganizations(GetOrganizationsRequest getOrganizationsRequest);
        public Task<GetOrgResponse> GetOrgByIdAsync(Guid id);
        public Task<Organization> GetOrganizationByCodeAsync(string code);
    }
}
