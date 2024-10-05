using FaceWelcome.Service.DTOs.Request.Event;
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
        public Task<GetListGuestsByEventResponse> GetListGuestsByEventAsync(Guid id);
        public Task CreateEventAsync(PostEventRequest postEventRequest);
    }
}
