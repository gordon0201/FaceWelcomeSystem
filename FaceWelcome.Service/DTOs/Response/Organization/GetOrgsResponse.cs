using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Organization
{
    public class GetOrgsResponse
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }

        public List<GetOrgResponse> getOrgResponses { get; set; }
    }
}
