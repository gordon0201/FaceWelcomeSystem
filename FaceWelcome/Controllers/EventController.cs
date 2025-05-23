﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FaceWelcome.Service.Services.Interfaces;
using FaceWelcome.API.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Request.Guest;


namespace FaceWelcome.API.Controllers
{
    [ApiController]

    public class EventController : ControllerBase
    {
        private IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._eventService = eventService;
        }

        #region Get event by Id
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Event.EventEndpoint)]
        public async Task<IActionResult> GetEventByIdAsync([FromRoute] EventIdRequest eventIdRequest)
        {
            try
            {
                var data = await _eventService.GetEventByIdAsync(eventIdRequest.Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        #region Get Guests By Event Id
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpGet(APIEndPointConstant.Event.ListGuestsEndpoint)]
        public async Task<IActionResult> GetGuestsByEventIdAsync([FromRoute] EventIdRequest eventIdRequest, [FromQuery] GetAllGuestsRequest getAllGuestsRequest)
        {
            try
            {
                var data = await _eventService.GetListGuestsByEventAsync(eventIdRequest.Id, getAllGuestsRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Create Event
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeConstant.MultipartFormData)]
        [Produces(MediaTypeConstant.ApplicationJson)]
        [HttpPost(APIEndPointConstant.Event.EventsEndpoint)]
        public async Task<IActionResult> PostCreateEventAsync([FromForm] PostEventRequest postEventRequest)
        {
            if (postEventRequest == null)
            {
                return BadRequest("Invalid event data");
            }

            try
            {
                await this._eventService.CreateEventAsync(postEventRequest);
                return Ok("Event created successfully.");  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion


    }

}
