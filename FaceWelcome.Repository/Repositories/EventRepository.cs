using FaceWelcome.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class EventRepository
    {
        private FaceWelcomeContext _dbContext;

        public EventRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }

    }
}
