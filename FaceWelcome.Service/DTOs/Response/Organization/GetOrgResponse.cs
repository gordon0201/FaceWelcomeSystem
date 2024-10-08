using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.DTOs.Response.Organization
{
    public class GetOrgResponse
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Status { get; set; }

        public Guid? OrganizationGroupId { get; set; }
    }
}
