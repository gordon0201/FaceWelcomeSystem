using AutoMapper;
using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.Guest;
using FaceWelcome.Service.DTOs.Response.Guest;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class GuestService : IGuestService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GuestService(IUnitOfWork unitOfWork, IMapper mapper)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            this._mapper = mapper;
        }

       

        public async Task CreateGuestAsync(PostGuestRequest postGuestRequest)
        {
            // Check if the request object is null
            if (postGuestRequest == null)
            {
                throw new ArgumentNullException(nameof(postGuestRequest), "Request cannot be null");
            }

            // Create a new Guest object from the DTO
            var guest = new Guest
            {
                Code = postGuestRequest.code,
                Type = postGuestRequest.type,
                Status = postGuestRequest.status,
                CheckInTime = postGuestRequest.checkInTime,
                CheckOutTime = postGuestRequest.checkOutTime,
                CreatedAt = DateTime.Now,
                GroupId = postGuestRequest.groupId,
                PersonId = postGuestRequest.personId,
                EventId = postGuestRequest.eventId
            };

            try
            {
                // Add the guest to the database via the repository
                await _unitOfWork.GuestRepository.AddAsync(guest);

                // Save changes to the database
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log detailed error information if a database update fails
                Console.WriteLine(dbEx.InnerException?.Message);
                throw new Exception("Database update error occurred while creating the guest.");
            }
            catch (Exception ex)
            {
                // Log any other exceptions that may occur
                Console.WriteLine(ex.Message);
                throw new Exception("An error occurred while creating the guest.");
            }
        }


        public async Task UpdateGuestAsync(Guid guestId, UpdateGuestRequest updateGuestRequest)
        {
            if (updateGuestRequest == null)
            {
                throw new ArgumentNullException(nameof(updateGuestRequest), "Request cannot be null");
            }

            // Tìm khách trong cơ sở dữ liệu bằng ID
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(guestId);

            if (guest == null)
            {
                throw new KeyNotFoundException($"Guest with ID {guestId} not found");
            }

            // Cập nhật thông tin khách từ DTO
            guest.Code = updateGuestRequest.code;
            guest.Type = updateGuestRequest.type;
            guest.Status = updateGuestRequest.status;
            guest.CheckInTime = updateGuestRequest.checkInTime;
            guest.CheckOutTime = updateGuestRequest.checkOutTime;
            guest.GroupId = updateGuestRequest.groupId;
            guest.PersonId = updateGuestRequest.personId;
            guest.EventId = updateGuestRequest.eventId;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _unitOfWork.CommitAsync();
        }


        /*Task<GetGuestResponse> IGuestService.GetGuestByIdAsync(int id)
        {
            throw new NotImplementedException();
        }*/

        #region Get all guests
        public async Task<GetGuestsResponse> GetAllGuestsAsync(GetAllGuestsRequest getAllGuestsRequest)
        {
            // Lấy danh sách tất cả khách từ cơ sở dữ liệu thông qua GuestRepository
            var guests = await _unitOfWork.GuestRepository.GetAllAsync();

            // Kiểm tra xem có khách nào không
            if (guests == null || !guests.Any())
            {
                throw new Exception("No guests found.");
            }

            // Tính tổng số bản ghi và số trang
            int totalRecords = guests.Count;
            int totalPages = (int)Math.Ceiling((double)totalRecords / getAllGuestsRequest.PageSize);

            // Phân trang cho danh sách Guests
            var paginatedList = guests
                .Skip((getAllGuestsRequest.PageNumber - 1) * getAllGuestsRequest.PageSize)
                .Take(getAllGuestsRequest.PageSize)
                .ToList();

            // Sử dụng AutoMapper để ánh xạ danh sách guests sang GetGuestResponse DTO
            var guestResponses = _mapper.Map<List<GetGuestResponse>>(paginatedList);

            // Tạo đối tượng phản hồi chứa thông tin phân trang và danh sách Guests
            var response = new GetGuestsResponse
            {
                PageSize = getAllGuestsRequest.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageNumber = getAllGuestsRequest.PageNumber,
                Guests = guestResponses
            };

            return response;
        }
        #endregion


        public async Task<GetGuestResponse> GetGuestByIdAsync(GuestRequest guestRequest)
        {
            // Lấy khách từ cơ sở dữ liệu thông qua GuestRepository theo ID
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(guestRequest.Id);

            // Nếu không tìm thấy khách, ném ra một ngoại lệ
            if (guest == null)
            {
                throw new KeyNotFoundException($"Guest with ID {guestRequest.Id} not found");
            }

            // Sử dụng AutoMapper để ánh xạ khách sang GetGuestResponse DTO
            var guestResponse = _mapper.Map<GetGuestResponse>(guest);

            // Trả về GetGuestResponse đã được ánh xạ
            return guestResponse;
        }

        #region Delete Guest
        public async Task DeleteGuestAsync(Guid id)
        {
            // Lấy khách hiện tại từ cơ sở dữ liệu theo id
            var existedGuest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
            if (existedGuest == null)
            {
                throw new Exception($"Guest with ID {id} cannot be found.");
            }

            // Kiểm tra ràng buộc khóa ngoại
            if (existedGuest.GuestImages.Any())
            {
                throw new Exception($"Cannot delete guest with ID {id} because there are associated guest images.");
            }

            if (existedGuest.EventId.HasValue)
            {
                throw new Exception($"Cannot delete guest with ID {id} because they are associated with an event.");
            }

            if (existedGuest.GroupId.HasValue)
            {
                throw new Exception($"Cannot delete guest with ID {id} because they are associated with a group.");
            }

            try
            {
                await _unitOfWork.GuestRepository.DeleteGuestAsync(existedGuest);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the guest: {ex.Message}");
            }
        }
        #endregion

    }
}
