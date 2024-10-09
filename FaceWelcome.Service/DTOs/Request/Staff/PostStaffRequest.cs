using System;

namespace FaceWelcome.Service.DTOs.Request.Staff
{
    public class PostStaffRequest
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public Guid? EventId { get; set; }
    }
}
