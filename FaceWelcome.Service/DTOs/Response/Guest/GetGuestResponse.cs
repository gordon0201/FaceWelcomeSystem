using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace FaceWelcome.Service.DTOs.Response.Guest
{
    public class GetGuestResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Guid? EventId { get; set; }
        public virtual Guid? GroupId { get; set; }
        public virtual Guid? PersonId { get; set; }
    }
}

