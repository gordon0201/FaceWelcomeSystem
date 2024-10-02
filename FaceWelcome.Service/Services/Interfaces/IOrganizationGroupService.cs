using FaceWelcome.Service.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IOrganizationGroupService
    {
        public Task CreateOrganizationGroupAsync(PostOrganizationGroupRequest postOrganizationGroupRequest);
    }
}
