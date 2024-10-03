using AutoMapper;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Profiles.Person
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<FaceWelcome.Repository.Models.Person, GetPersonResponse>()
                .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name));
        }
    }
}
