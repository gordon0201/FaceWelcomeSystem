using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceWelcome.Service.Enums;

namespace FaceWelcome.Service.DTOs.Request.Organization
{
    public class PostOrganizationRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public OrganizationEnum.OrgStatus Status { get; set; }

        [Required]
        public Guid OrganizationGroupId { get; set; }
    }
}
