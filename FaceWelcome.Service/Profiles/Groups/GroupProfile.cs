using AutoMapper;
using FaceWelcome.Service.DTOs.Response.Group;
using System;
using FaceWelcome.Repository.Models;

namespace FaceWelcome.Service.Profiles.Groups
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GetGroupResponse>()
                .ForMember(dest => dest.StaffId, opt => opt.MapFrom(src => src.Staff != null ? src.Staff.Id : (Guid?)null))
                .ForMember(dest => dest.WelcomeId, opt => opt.MapFrom(src => src.Welcome != null ? src.Welcome.Id : (Guid?)null))
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event != null ? src.Event.Id : (Guid?)null));
        }
    }
}
