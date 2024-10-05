using FaceWelcome.Service.DTOs.Response.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Event
{
    public class GetListGuestsByEventResponse
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int? GuestNumber { get; set; }

        public int? GroupNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public List<GetGuestResponse> GuestList { get; set; } = new List<GetGuestResponse>();
    }
}
