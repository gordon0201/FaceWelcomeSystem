using FaceWelcome.Repository.Models; // Thêm namespace cho entity
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaceWelcome.Repository.Repositories
{
    public class OrganizationGroupRepository
    {
        private readonly FaceWelcomeContext _dbContext;

        public OrganizationGroupRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }


        // Thêm một nhóm tổ chức mới
        public async Task AddAsync(OrganizationGroup organizationGroup)
        {
            await _dbContext.OrganizationGroups.AddAsync(organizationGroup);
        }

        // Lấy tất cả nhóm tổ chức
        public async Task<IEnumerable<OrganizationGroup>> GetAllAsync()
        {
            return await _dbContext.OrganizationGroups.ToListAsync();
        }

        // Lấy nhóm tổ chức theo ID
        public async Task<OrganizationGroup> GetByIdAsync(int id)
        {
            return await _dbContext.OrganizationGroups.FindAsync(id);
        }

        // Cập nhật nhóm tổ chức
        public async Task UpdateAsync(OrganizationGroup organizationGroup)
        {
            _dbContext.OrganizationGroups.Update(organizationGroup);
            await _dbContext.SaveChangesAsync(); // Lưu thay đổi
        }

        // Xóa nhóm tổ chức
        public async Task DeleteAsync(int id)
        {
            var organizationGroup = await GetByIdAsync(id);
            if (organizationGroup != null)
            {
                _dbContext.OrganizationGroups.Remove(organizationGroup);
                await _dbContext.SaveChangesAsync(); // Lưu thay đổi
            }
        }
    }
}
