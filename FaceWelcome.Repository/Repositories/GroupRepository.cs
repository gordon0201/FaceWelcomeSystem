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
        public async Task<List<Group>> GetAllAsync()
        {
            return await _dbContext.Groups.ToListAsync();
        }

        // Lấy nhóm theo ID
        public async Task<Group> GetByIdAsync(Guid id)
        {
            return await _dbContext.Groups.FindAsync(id);
        }

        // Cập nhật nhóm
        public void Update(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Group cannot be null");
            }

            _dbContext.Groups.Update(group);
        }

        // Xóa nhóm
        public void Delete(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group), "Group cannot be null");
            }

            _dbContext.Groups.Remove(group);
        }
    }
}
