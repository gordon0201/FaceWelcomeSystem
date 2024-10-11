using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.GuestImage
{
    public class GetGuestImageResponse
    {
        public Guid Id { get; set;}
        public string code { get; set; }
        public string type { get; set; }
        public string Path { get; set; }
        public string description { get; set; }
        public Guid guestId { get; set; }
    }
}
