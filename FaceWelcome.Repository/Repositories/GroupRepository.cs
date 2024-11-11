using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class GroupRepository
    {
        private FaceWelcomeContext _dbContext;

        public GroupRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // Thêm một nhóm mới
        public async Task AddAsync(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Group cannot be null");
            }

            await _dbContext.Groups.AddAsync(group);
        }

        // Lấy tất cả các nhóm
        public async Task<List<Group>> GetAllGroupsAsync()
        {
            try
            {
                return await _dbContext.Groups.Include(e => e.Event).Include(s => s.Staff).Include(w => w.Staff).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Lấy nhóm theo ID
        public async Task<Group> GetGroupByIdAsync(Guid? id)
        {
            try
            {
                return await this._dbContext.Groups
                    .Include(e => e.Event)
                    .Include(s => s.Staff).Include(w => w.Staff)
                    .SingleOrDefaultAsync(g => g.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // Cập nhật nhóm
        public async Task UpdateGroupAsync(Group group)
        {
            try
            {
                this._dbContext.Groups.Update(group);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating group", ex);
            }
        }

        // Xóa nhóm
        public async Task DeleteGroupAsync(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Group cannot be null");
            }

            _dbContext.Groups.Remove(group);
        }

        public async Task<List<Group>> GetGroupsByStaffIdAsync(Guid staffId)
        {
            return await _dbContext.Groups.Where(g => g.StaffId == staffId).ToListAsync();
        }

    }
}
