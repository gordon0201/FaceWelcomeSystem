using FaceWelcome.Service.DTOs.Request;
using FaceWelcome.Service.DTOs.Request.GuestImage;
using FaceWelcome.Service.DTOs.Response.GuestImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IGuestImageService
    {
        public Task CreateGuestImageAsync(PostGuestImageRequest postGuestImageRequest);

        public Task<GetGuestImageResponse> GetGuestImageByIdAsync(Guid id);

        public Task UpdateGuestImageAsync(Guid id, UpdateGuestImageRequest updateGuestImageRequest);

        public Task<GetAllGuestImagesResponse> GetAllGuestImagesAsync(GetAllGuestImagesRequest getAllGuestImagesRequest);

        public Task DeleteGuestImageAsync(Guid id);
    }
}
