using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Response.Person;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }
        public async Task<GetPersonResponse> GetPersonByIdAsync(Guid id)
        {
            try
            {
                var person = await _unitOfWork.PersonRepository.GetPersonByIdAsync(id);
                if (person == null)
                {
                    throw new Exception();
                }
                var personResponse = this._mapper.Map<GetPersonResponse>(person);
                return personResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
