using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.Event
{
    public class EventIdRequest
    {
        [FromRoute(Name ="id")]
        public Guid Id { get; set; }
    }
}
