using AutoMapper;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.DTOs.Response.Guest;
using FaceWelcome.Service.DTOs.Response.OrganizationGroup;

namespace FaceWelcome.Service.Profiles.OrganizationGroups
{
    public class OrganizationGroupProfile : Profile
    {
        public OrganizationGroupProfile()
        {
            CreateMap<OrganizationGroup, GetOrganizationGroupResponse>();
        }
    }
}
