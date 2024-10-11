using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class StaffRepository
    {
        private FaceWelcomeContext _dbContext;

        public StaffRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Staff> GetStaffByIdAsync(Guid id)
        {
            try
            {
                return await this._dbContext.Staff.Include(e => e.Event).SingleOrDefaultAsync(st => st.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateStaffAsync(Staff staff)
        {
            try
            {
                this._dbContext.Staff.Update(staff);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating staff", ex);
            }
        }

        public async Task AddAsync(Staff staff)
        {
            try
            {
                this._dbContext.Staff.AddAsync(staff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Staff>> GetAllStaffAsync()
        {
            try
            {
                return await _dbContext.Staff.Include(p => p.Event).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteStaffAsync(Staff staff)
        {
            if (staff == null)
            {
                throw new ArgumentNullException(nameof(staff), "Staff cannot be null");
            }

            _dbContext.Staff.Remove(staff);
        }
    }
}
