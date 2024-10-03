using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FaceWelcome.Service.DTOs.Response.Staff;
using FaceWelcome.Repository.Models;

namespace FaceWelcome.Service.Profiles.Staffs
{
    public class StaffProfile: Profile
    {
        public StaffProfile()
        {
            CreateMap<Staff, GetStaffResponse>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event != null ? src.Event.Id : (Guid?)null));
        }
    }
}
