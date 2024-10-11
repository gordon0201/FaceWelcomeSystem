using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.Group;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FaceWelcome.Service.DTOs.Response.Group;
using AutoMapper;
using FaceWelcome.Service.DTOs.Response.Staff;

namespace FaceWelcome.Service.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateGroupAsync(PostGroupRequest postGroupRequest)
        {
            // Kiểm tra xem đối tượng yêu cầu có phải là null không
            if (postGroupRequest == null)
            {
                throw new ArgumentNullException(nameof(postGroupRequest), "Request cannot be null");
            }

            // Tạo một đối tượng Group mới từ DTO
            var group = new Group
            {
                Code = postGroupRequest.Code, // Đảm bảo sử dụng đúng tên thuộc tính
                Name = postGroupRequest.Name,
                GuestNumber = postGroupRequest.GuestNumber,
                Description = postGroupRequest.Description,
                Status = postGroupRequest.Status,
                Quantity = postGroupRequest.Quantity,
                CreatedAt = DateTime.UtcNow, // Sử dụng UTC để nhất quán về thời gian
                StaffId = postGroupRequest.StaffId,
                WelcomeId = postGroupRequest.WelcomeId,
                EventId = postGroupRequest.EventId
            };

            try
            {
                // Thêm nhóm vào cơ sở dữ liệu thông qua repository
                await _unitOfWork.GroupRepository.AddAsync(group);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Ghi lại thông tin lỗi chi tiết nếu cập nhật cơ sở dữ liệu thất bại
                Console.WriteLine(dbEx.InnerException?.Message);
                throw new Exception("Database update error occurred while creating the group.");
            }
            catch (Exception ex)
            {
                // Ghi lại bất kỳ ngoại lệ nào khác có thể xảy ra
                Console.WriteLine(ex.Message);
                throw new Exception("An error occurred while creating the group.");
            }
        }

        public async Task<GetAllGroupsResponse> GetAllGroupsAsync(GetAllGroupsRequest getAllGroupsRequest)
        {
            // Lấy danh sách Groups từ repository
            var groups = await _unitOfWork.GroupRepository.GetAllGroupsAsync();
            if (groups == null || !groups.Any())
            {
                throw new Exception("No groups found.");
            }

            // Tính tổng số bản ghi và số trang
            int totalRecords = groups.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / getAllGroupsRequest.PageSize);

            // Phân trang cho danh sách Groups
            var paginatedList = groups.Skip((getAllGroupsRequest.PageNumber - 1) * getAllGroupsRequest.PageSize)
                                      .Take(getAllGroupsRequest.PageSize)
                                      .ToList();

            // Map danh sách Groups sang DTO response
            var responseList = _mapper.Map<List<GetGroupResponse>>(paginatedList);

            // Tạo đối tượng phản hồi chứa thông tin phân trang và danh sách Groups
            var response = new GetAllGroupsResponse
            {
                PageSize = getAllGroupsRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getAllGroupsRequest.PageNumber,
                Groups = responseList
            };

            return response;
        }


        public async Task<GetGroupResponse> GetGroupByIdAsync(Guid id)
        {
            try
            {
                var group = await _unitOfWork.GroupRepository.GetGroupByIdAsync(id);
                if (group is null)
                {
                    throw new Exception();
                }

                var groupResponse = this._mapper.Map<GetGroupResponse>(group);
                return groupResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #region Update Group
        public async Task UpdateGroupAsync(Guid id, UpdateGroupRequest updateGroupRequest)
        {
            var existedGroup = await _unitOfWork.GroupRepository.GetGroupByIdAsync(id);
            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} cannot be found.");
            }

            try
            {
                // Cập nhật các thuộc tính của Group từ request
                existedGroup.Name = updateGroupRequest.Name;
                existedGroup.GuestNumber = updateGroupRequest.GuestNumber;
                existedGroup.Description = updateGroupRequest.Description;
                existedGroup.Status = updateGroupRequest.Status;
                existedGroup.Quantity = updateGroupRequest.Quantity;
                existedGroup.StaffId = updateGroupRequest.StaffId;
                existedGroup.WelcomeId = updateGroupRequest.WelcomeId;
                existedGroup.EventId = updateGroupRequest.EventId;

                // Không cần cập nhật hình ảnh
                await _unitOfWork.GroupRepository.UpdateGroupAsync(existedGroup);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete Group
        public async Task DeleteGroupAsync(Guid id)
        {
            // Lấy nhóm hiện tại từ cơ sở dữ liệu theo id
            var existedGroup = await _unitOfWork.GroupRepository.GetGroupByIdAsync(id);
            if (existedGroup == null)
            {
                throw new Exception($"Group with ID {id} cannot be found.");
            }

            try
            {
                await _unitOfWork.GroupRepository.DeleteGroupAsync(existedGroup);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the group: {ex.Message}");
            }
        }
        #endregion

    }
}
