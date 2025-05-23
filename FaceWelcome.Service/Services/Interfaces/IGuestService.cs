﻿using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Request.OrganizationGroup;
using FaceWelcome.Service.DTOs.Response.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IGuestService
    {
      
        public Task CreateGuestAsync(PostGuestRequest postGuestRequest);

        public Task UpdateGuestAsync(Guid guestId, UpdateGuestRequest updateGuestRequest);

        public Task<GetGuestsResponse> GetAllGuestsAsync(GetAllGuestsRequest getAllGuestsRequest);

        public Task<GetGuestResponse> GetGuestByIdAsync(GuestRequest guestRequest);

        public Task DeleteGuestAsync(Guid id);
    }
}
