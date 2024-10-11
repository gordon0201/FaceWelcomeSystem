using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.Group
{
    public class UpdateGroupRequest
    {
        public string Name { get; set; }
        public int? GuestNumber { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? Quantity { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? WelcomeId { get; set; }
        public Guid? EventId { get; set; }
    }
}
