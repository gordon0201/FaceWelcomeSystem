    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace FaceWelcome.Service.DTOs.Request.GuestImage
    {
        public class UpdateGuestImageRequest
        {
            public string type { get; set; }
            public IFormFile ImageFile { get; set; }
            public string description { get; set; }
            public Guid guestId { get; set; }
        }
    }
