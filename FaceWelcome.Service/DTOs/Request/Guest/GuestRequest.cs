using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FaceWelcome.Service.DTOs.Request.Guest
{
    public class GuestRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
