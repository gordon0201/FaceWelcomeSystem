using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Response.Staff;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public StaffService(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        public async Task<GetStaffResponse> GetStaffByIdAsync(Guid id)
        {
            try
            {
                var staff = await _unitOfWork.StaffRepository.GetStaffByIdAsync(id);
                if (staff is null)
                {
                    throw new Exception();
                }

                var staffResponse = this._mapper.Map<GetStaffResponse>(staff);
                return staffResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
