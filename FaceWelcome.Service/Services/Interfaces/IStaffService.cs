using FaceWelcome.Service.DTOs.Response.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IStaffService
    {
        public Task<GetStaffResponse> GetStaffByIdAsync(Guid id);
    }
}
