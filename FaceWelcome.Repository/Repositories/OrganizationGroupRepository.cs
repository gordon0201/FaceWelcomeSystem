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
        public async Task<List<OrganizationGroup>> GetAllOrganizationGroupsAsync()
        {
            try
            {
                return await _dbContext.OrganizationGroups.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Lấy nhóm tổ chức theo ID

        public async Task<OrganizationGroup> GetOrganizationGroupByIdAsync(Guid id)

        {
            try
            {
                return await this._dbContext.OrganizationGroups.SingleOrDefaultAsync(st => st.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Cập nhật nhóm tổ chức
        public async Task UpdateOrganizationGroupAsync(OrganizationGroup organizationGroup)
        {
            try
            {
                this._dbContext.OrganizationGroups.Update(organizationGroup);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating organizationGroup", ex);
            }
        }

        // Xóa nhóm tổ chức
        public async Task DeleteOrganizationGroupAsync(OrganizationGroup organizationGroup)

        {
            if (organizationGroup == null)
            {
                throw new ArgumentNullException(nameof(organizationGroup), "Organization Group cannot be null");
            }

            _dbContext.OrganizationGroups.Remove(organizationGroup);
        }
    }
}
