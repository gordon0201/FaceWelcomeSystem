using AutoMapper;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Response.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Profiles.Organizations
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, GetOrgResponse>()
                .ForMember(dest => dest.OrganizationGroupId, opt => opt
                .MapFrom(src => src.OrganizationGroup != null ? src.OrganizationGroup.Id : (Guid?)null));
        }
    }
}
