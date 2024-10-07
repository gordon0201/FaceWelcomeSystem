using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.WelcomeTemplate
{
    public class GetTemplateResponse
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string Status { get; set; }

        public string Image { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? EventId { get; set; }

        public string? EventName { get; set; }
    }
}
