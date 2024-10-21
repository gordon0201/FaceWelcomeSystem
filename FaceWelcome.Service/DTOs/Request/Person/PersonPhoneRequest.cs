using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Request.Person
{
    public class PersonPhoneRequest
    {
        [FromRoute(Name ="phone")]
        public string phoneNumber { get; set; }
    }
}
