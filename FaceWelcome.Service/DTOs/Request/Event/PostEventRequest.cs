using System;
using System.ComponentModel.DataAnnotations;
using FaceWelcome.Service.Enums;

namespace FaceWelcome.Service.DTOs.Request.Event
{
    public class PostEventRequest
    {
        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name must not exceed 100 characters.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Event type is required.")]
        public EventEnum.Type Type { get; set; } // Use enum EventType

        [Required(ErrorMessage = "Guest number is required.")]
        public int? GuestNumber { get; set; }

        [Required(ErrorMessage = "Group number is required.")]
        public int? GroupNumber { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        public string EndTime { get; set; }

        [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location must not exceed 200 characters.")]
        public string Location { get; set; }

        // Additional properties can be added if needed
    }
}
