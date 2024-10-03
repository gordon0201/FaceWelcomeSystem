using FaceWelcome.Service.DTOs.Request;
using FaceWelcome.Service.DTOs.Request.GuestImage;
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
    }
}
