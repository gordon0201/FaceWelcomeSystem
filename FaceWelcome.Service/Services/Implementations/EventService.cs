using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Repositories;
using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Event;
using FaceWelcome.Service.DTOs.Response.Guest;
using FaceWelcome.Service.Enums;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        #region Get by Id
        public async Task<GetEventResponse> GetEventByIdAsync(Guid id)
        {
            try
            {
                var getEvent = await _unitOfWork.EventRepository.GetEventByIdAsync(id);
                if (getEvent == null)
                {
                    throw new Exception("Event cannot found");
                }
                var eventResponse = this._mapper.Map<GetEventResponse>(getEvent);
                return eventResponse;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Create event
        public async Task CreateEventAsync(PostEventRequest postEventRequest)
        {
            var eventCode = await _unitOfWork.EventRepository.GetEventByCodeAsync(postEventRequest.Code);
            if (eventCode != null)
            {
                throw new Exception("Code has been existed");
            }

            try
            {
                // Chuyển đổi chuỗi datetime sang DateTime với định dạng đã cho
                var startDateTime = DateTime.ParseExact(postEventRequest.StartTime,
                                                          "HH:mm, dd/MM/yyyy",
                                                          CultureInfo.InvariantCulture);
                var endDateTime = DateTime.ParseExact(postEventRequest.EndTime,
                                                        "HH:mm, dd/MM/yyyy",
                                                        CultureInfo.InvariantCulture);
                var newEvent = new Event
                {
                    Code = postEventRequest.Code,
                    Name = postEventRequest.EventName,
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    Description = postEventRequest.Description,
                    Location = postEventRequest.Location,
                    CreatedAt = DateTime.Now,
                    Type = postEventRequest.Type.ToString(),
                    GroupNumber = postEventRequest.GroupNumber,
                    GuestNumber = postEventRequest.GuestNumber,
                    Status = postEventRequest.Status.ToString(),
                    
                };

                await _unitOfWork.EventRepository.AddAsync(newEvent);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get list guests by eventId
        public async Task<GetListGuestsByEventResponse> GetListGuestsByEventAsync(Guid id, GetAllGuestsRequest getAllGuestsRequest)
        {
            try
            {
                var existedEvent = await _unitOfWork.EventRepository.GetEventByIdAsync(id);
                if (existedEvent == null)
                {
                    throw new Exception(@"Event with {id} was not found");
                }
                var guestList = await _unitOfWork.GuestRepository.GetGuestsByEventIdAsync(id);
                int totalRecords = guestList.Count;
                int totalPages = (int)System.Math.Ceiling((double)totalRecords / getAllGuestsRequest.PageSize);
                var list = guestList.Skip((getAllGuestsRequest.PageNumber - 1) * getAllGuestsRequest.PageSize)
                    .Take(getAllGuestsRequest.PageSize)
                    .ToList();

                var guestListResponse = this._mapper.Map<List<GetGuestResponse>>(list);

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
                    Type = existedEvent.Type,
                    PageNumber = getAllGuestsRequest.PageNumber,
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageSize = getAllGuestsRequest.PageSize
                };
                return response;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
    }
}
