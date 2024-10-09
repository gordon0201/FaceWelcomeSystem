using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.OrganizationGroup
{
    public class GetOrganizationGroupResponse
    {
        public Guid Id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
