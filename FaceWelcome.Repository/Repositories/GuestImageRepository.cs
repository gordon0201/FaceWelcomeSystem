using FaceWelcome.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class GuestImageRepository
    {
        private FaceWelcomeContext _dbContext;

        public GuestImageRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

    }
}
