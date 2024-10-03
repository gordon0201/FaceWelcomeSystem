using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class EventService : IEventService
    {
        private UnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
        }

        public async Task CreateEventAsync(PostEventRequest postEventRequest)
        {

        }
    }
}
