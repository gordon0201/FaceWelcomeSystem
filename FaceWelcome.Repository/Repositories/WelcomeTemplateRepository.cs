using FaceWelcome.Repository.Models;
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
    }
}
