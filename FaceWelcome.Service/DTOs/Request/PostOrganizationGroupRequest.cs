using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request
{
    public class PostOrganizationGroupRequest
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }

    }
}
