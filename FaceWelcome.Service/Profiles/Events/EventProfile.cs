using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Response.Event;

namespace FaceWelcome.Service.Profiles.Events
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            // Cấu hình ánh xạ từ Event sang GetEventResponse
            CreateMap<Event, GetEventResponse>();
        }
    }
}
