using FaceWelcome.Repository.Infrastructures;
using AutoMapper;

using FaceWelcome.Service.DTOs.Request.Organization;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceWelcome.Repository.Models;

namespace FaceWelcome.Service.Services.Implementations
{
    public class OrganizationService : IOrganizationService
    {
        UnitOfWork _unitOfWork;
        //private IMapper _mapper;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            //this._mapper = mapper;
        }

        public async Task<Organization> GetOrganizationByCodeAsync(string code)
        {
            try
            {
                var organization = await _unitOfWork.OrganizationRepository.GetOrganizationByCodeAsync(code);
                if (organization == null)
                {
                    throw new Exception("aaaaa");
                }
                return organization;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
