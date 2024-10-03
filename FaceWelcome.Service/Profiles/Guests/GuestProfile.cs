using AutoMapper;
using FaceWelcome.Service.DTOs.Response.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceWelcome.Repository.Models;
using System.Drawing.Drawing2D;

namespace FaceWelcome.Service.Profiles.Guests
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<Guest, GetGuestResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event != null ? src.Event.Id : (Guid?)null))
                .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group != null ? src.Group.Id : (Guid?)null))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person != null ? src.Person.Id : (Guid?)null));
        }
    }
}
