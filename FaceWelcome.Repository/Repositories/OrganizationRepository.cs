using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class OrganizationRepository
    {
        private FaceWelcomeContext _dbContext;
        public OrganizationRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region Get by Id
        public async Task<Organization> GetOrganizationByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Organizations
                    .Include(org => org.OrganizationGroup)
                    .SingleOrDefaultAsync(org => org.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get all Organizations
        public async Task<List<Organization>> GetOrganizationsAsync()
        {
            try
            {
                return await _dbContext.Organizations
                    .Include(org => org.OrganizationGroup)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Create Organization
        public async Task AddAsync(Organization organization)
        {
            try
            {
                _dbContext.Organizations.AddAsync(organization);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get by Code
        public async Task<Organization> GetOrganizationByCodeAsync(string code)
        {
            try
            {
                return await this._dbContext.Organizations.SingleOrDefaultAsync(o => o.Code == code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
