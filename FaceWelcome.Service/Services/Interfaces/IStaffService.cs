using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Request.Staff;
using FaceWelcome.Service.DTOs.Response.Person;
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

        public Task UpdateStaffAsync(Guid id, UpdateStaffRequest updateStaffRequest);
        public Task PostStaffAsync(PostStaffRequest postStaffRequest);
        public Task<GetAllStaffsResponse> GetAllStaffsAsync(GetAllStaffsRequest getAllStaffsRequest);

        public Task DeleteStaffAsync(Guid id);
    }
}
