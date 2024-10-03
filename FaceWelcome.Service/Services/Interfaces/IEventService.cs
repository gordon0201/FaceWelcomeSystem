using FaceWelcome.Service.DTOs.Request.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IEventService
    {
        public Task CreateEventAsync(PostEventRequest postEventRequest);
    }
}
