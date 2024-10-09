using FaceWelcome.Service.DTOs.Response.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Group
{
    public class GetAllGroupsResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public List<GetGroupResponse> Groups { get; set; }
    }
}
