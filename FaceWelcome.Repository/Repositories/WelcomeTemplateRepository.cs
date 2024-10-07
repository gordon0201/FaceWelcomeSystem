using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class WelComeTemplateRepository
    {
        private FaceWelcomeContext _dbContext;

        public WelComeTemplateRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region Get all
        public async Task<List<WelcomeTemplate>> GetAllWelcomeTemplatesAsync()
        {
            try
            {
                return await _dbContext.WelcomeTemplates
                    .Include(wp => wp.Groups)
                    .Include(wp => wp.Event)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Get template by id
        public async Task<WelcomeTemplate> GetTemplateByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.WelcomeTemplates
                    .Include(wp => wp.Groups)
                    .Include(wp => wp.Event)
                    .SingleOrDefaultAsync(wp => wp.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
