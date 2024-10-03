using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.DTOs.Request.Group;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FaceWelcome.Service.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly UnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
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
    }
}
