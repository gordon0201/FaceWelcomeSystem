using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Response.Event;
using FaceWelcome.Service.DTOs.Response.Guest;
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
        private IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateEventAsync(PostEventRequest postEventRequest)
        {

        }

        public async Task<GetListGuestsByEventResponse> GetListGuestsByEventAsync(Guid id)
        {
            try
            {
                var existedEvent = await _unitOfWork.EventRepository.GetEventByIdAsync(id);
                if (existedEvent == null)
                {
                    throw new Exception(@"Event with {id} was not found");
                }
                var guestList = await _unitOfWork.GuestRepository.GetGuestsByEventIdAsync(id);
                var guestListResponse =  this._mapper.Map<List<GetGuestResponse>>(guestList);


                var response = new GetListGuestsByEventResponse
                {
                    Code = existedEvent.Code,
                    CreatedAt = existedEvent.CreatedAt,
                    Description = existedEvent.Description,
                    EndDate = existedEvent.EndDate,
                    GroupNumber = existedEvent.GroupNumber,
                    GuestList = guestListResponse,
                    GuestNumber = existedEvent.GuestNumber,
                    Location = existedEvent.Location,
                    Name = existedEvent.Name,
                    StartDate = existedEvent.StartDate,
                    Status = existedEvent.Status,
                    Type = existedEvent.Type
                };
                return response;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
