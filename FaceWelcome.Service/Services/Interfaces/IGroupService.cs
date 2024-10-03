using FaceWelcome.Service.DTOs.Request.Event;
using FaceWelcome.Service.DTOs.Request.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IGroupService
    {
        public Task CreateGroupAsync(PostGroupRequest postGroupRequest);
    }
}
