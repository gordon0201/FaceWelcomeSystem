using AutoMapper;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Response.WelcomeTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Profiles.Templates
{
    public class TemplateProfile : Profile
    {
        public TemplateProfile()
        {
            CreateMap<WelcomeTemplate, GetTemplateResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event != null ? src.Event.Id : (Guid?)null))
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event != null ? src.Event.Name : null));

        }
    }
}
