using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.Person;
using FaceWelcome.Service.DTOs.Request.Staff;
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

        #region Get all staffs
        public async Task<GetAllStaffsResponse> GetAllStaffsAsync(GetAllStaffsRequest getAllStaffsRequest)
        {
            // Lấy danh sách Staff từ repository
            var staffs = await _unitOfWork.StaffRepository.GetAllStaffAsync();
            if (staffs == null)
            {
                throw new Exception("No staffs found.");
            }

            // Tính tổng số bản ghi và số trang
            int totalRecords = staffs.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / getAllStaffsRequest.PageSize);

            // Phân trang cho danh sách Staffs
            var paginatedList = staffs.Skip((getAllStaffsRequest.PageNumber - 1) * getAllStaffsRequest.PageSize)
                                      .Take(getAllStaffsRequest.PageSize)
                                      .ToList();

            // Map danh sách Staffs sang DTO response
            var responseList = _mapper.Map<List<GetStaffResponse>>(paginatedList);

            // Tạo đối tượng phản hồi chứa thông tin phân trang và danh sách Staffs
            var response = new GetAllStaffsResponse
            {
                PageSize = getAllStaffsRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getAllStaffsRequest.PageNumber,
                Staffs = responseList
            };

            return response;
        }
        #endregion


        #region Get Staff By ID
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
        #endregion

        #region Update Staff
        public async Task UpdateStaffAsync(Guid id, UpdateStaffRequest updateStaffRequest)
        {
            var existedStaff = await _unitOfWork.StaffRepository.GetStaffByIdAsync(id);
            if (existedStaff == null)
            {
                throw new Exception($"Staff with ID {id} cannot be found.");
            }

            try
            {
                // Cập nhật các thuộc tính của Staff từ request
                existedStaff.FullName = updateStaffRequest.FullName;
                existedStaff.Phone = updateStaffRequest.Phone;
                existedStaff.DateOfBirth = updateStaffRequest.DateOfBirth;
                existedStaff.Gender = updateStaffRequest.Gender;
                existedStaff.Email = updateStaffRequest.Email;
                existedStaff.Address = updateStaffRequest.Address;
                existedStaff.Role = updateStaffRequest.Role;
                existedStaff.EventId = updateStaffRequest.EventId;


                // Cập nhật Staff trong cơ sở dữ liệu
                await _unitOfWork.StaffRepository.UpdateStaffAsync(existedStaff);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Create Staff
        public async Task PostStaffAsync(PostStaffRequest postStaffRequest)
        {
            try
            {
                //Tạo đối tượng Person
                var staff = new Staff
                {
                    Id = Guid.NewGuid(), // Tạo mới Guid cho Staff
                    Code = postStaffRequest.Code,
                    FullName = postStaffRequest.FullName,
                    Phone = postStaffRequest.Phone,
                    DateOfBirth = postStaffRequest.DateOfBirth, // Nếu sử dụng DateOnly trong DB, hãy chuyển đổi DateTime? thành DateOnly? nếu cần
                    Gender = postStaffRequest.Gender,
                    Email = postStaffRequest.Email,
                    Address = postStaffRequest.Address,
                    Role = postStaffRequest.Role,
                    EventId = postStaffRequest.EventId
                };
                await _unitOfWork.StaffRepository.AddAsync(staff);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
