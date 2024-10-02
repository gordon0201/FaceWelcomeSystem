using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class GuestRepository
    {
        private FaceWelcomeContext _dbContext;

        public GuestRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddAsync(Guest guest)
        {
            await _dbContext.Guests.AddAsync(guest);
        }

        // Lấy tất cả nhóm tổ chức
        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            return await _dbContext.Guests.ToListAsync();
        }

        // Lấy nhóm tổ chức theo ID
        public async Task<Guest> GetByIdAsync(Guid id)
        {
            return await _dbContext.Guests.FindAsync(id);
        }

        // Cập nhật nhóm tổ chức
        public async Task UpdateAsync(Guest guest)
        {
            _dbContext.Guests.Update(guest);
            await _dbContext.SaveChangesAsync(); // Lưu thay đổi
        }

        // Xóa nhóm tổ chức
        public async Task DeleteAsync(Guid id)
        {
            var guest = await GetByIdAsync(id);
            if (guest != null)
            {
                _dbContext.Guests.Remove(guest);
                await _dbContext.SaveChangesAsync(); // Lưu thay đổi
            }
        }

    }
}
