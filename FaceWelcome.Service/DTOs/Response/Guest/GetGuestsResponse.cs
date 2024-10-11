using FaceWelcome.Service.DTOs.Response.Organization;
using System.Collections.Generic;

namespace FaceWelcome.Service.DTOs.Response.Guest
{
    public class GetGuestsResponse
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public List<GetGuestResponse> Guests { get; set; }
    }
}
