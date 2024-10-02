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

        public GuestService(IUnitOfWork unitOfWork)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
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
                Image = postGuestRequest.image,
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
            guest.Image = updateGuestRequest.image;
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
        }

        Task<GetGuestsResponse> IGuestService.GetGuestsAsync()
        {
            throw new NotImplementedException();
        }*/
    }
}
