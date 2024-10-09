using AutoMapper;
using FaceWelcome.Service.DTOs.Response.GuestImage;
using FaceWelcome.Repository.Models;

namespace FaceWelcome.Service.Profiles.GuestImages
{
    public class GuestImageProfile : Profile
    {
        public GuestImageProfile()
        {
            CreateMap<GuestImage, GetGuestImageResponse>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path)) // Đảm bảo ánh xạ đúng
                .ForMember(dest => dest.guestId, opt => opt.MapFrom(src => src.GuestId)); // Chuyển đổi guestId
        }
    }
}
