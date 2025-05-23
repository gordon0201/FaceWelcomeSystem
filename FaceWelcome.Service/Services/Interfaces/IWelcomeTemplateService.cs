﻿using FaceWelcome.Service.DTOs.Request;
using FaceWelcome.Service.DTOs.Request.WelcomeTemplate;
using FaceWelcome.Service.DTOs.Response.WelcomeTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IWelcomeTemplateService
    {
        public Task<GetTemplateResponse> GetTemplateByPhoneNumberAndEventIdAsync(string phoneNumber, Guid eventId);
        public Task<GetTemplateResponse> GetTemplateByGuestIdAsync(Guid guestId);
        public Task<GetTemplateResponse> GetTemplateByIdAsync(Guid templateId);
        public Task<GetTemplatesResponse> GetAllTemplatesAsync(GetTemplatesRequest getTemplatesRequest);
    }
}
