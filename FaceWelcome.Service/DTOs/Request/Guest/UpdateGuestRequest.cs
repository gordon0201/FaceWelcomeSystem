using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.Guest
{
    public class UpdateGuestRequest
    {
        public string code { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public string status { get; set; }
        public string note { get; set; }

        public DateTime checkInTime { get; set; }

        public DateTime checkOutTime { get; set; }

        public Guid eventId { get; set; }
        public Guid groupId { get; set; }
        public Guid personId { get; set; }
    }
}
