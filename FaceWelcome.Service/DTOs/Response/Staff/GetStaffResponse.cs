using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Staff
{
    public class GetStaffResponse
    {
        public Guid Id { get; set; }

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
