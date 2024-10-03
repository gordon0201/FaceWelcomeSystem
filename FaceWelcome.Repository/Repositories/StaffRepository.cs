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
    }
}
