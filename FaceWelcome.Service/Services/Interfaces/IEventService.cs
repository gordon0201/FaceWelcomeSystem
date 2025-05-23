﻿using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Response.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IEventService
    {
        public Task<GetEventResponse> GetEventByIdAsync(Guid id);
        public Task<GetListGuestsByEventResponse> GetListGuestsByEventAsync(Guid id, GetAllGuestsRequest getAllGuestsRequest);
        public Task CreateEventAsync(PostEventRequest postEventRequest);
    }
}
