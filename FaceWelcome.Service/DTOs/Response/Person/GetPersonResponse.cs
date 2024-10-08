using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Person
{
    public class GetPersonResponse
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string Position { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string OrganizationName { get; set; }
    }
}
