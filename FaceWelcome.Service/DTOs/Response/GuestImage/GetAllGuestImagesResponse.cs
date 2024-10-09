using FaceWelcome.Service.DTOs.Response.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.GuestImage
{
    public class GetAllGuestImagesResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<GetGuestImageResponse> GuestImages { get; set; }
    }
}
