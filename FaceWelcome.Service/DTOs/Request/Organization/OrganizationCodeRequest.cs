using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.Organization
{
    public class OrganizationCodeRequest
    {
        [FromRoute(Name = "code")]
        public string Code { get; set; }
    }
}
