using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Response.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IPersonService
    {
        public Task<GetPersonResponse> GetPersonByIdAsync(Guid id);
        public Task CreatePersonAsync(PostPersonRequest postPersonRequest);
        public Task<GetPeopleResponse> GetAllPersonsAsync(GetPeopleRequest getPeopleRequest);
    }
}
