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
    }
}
