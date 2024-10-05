using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
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


        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Events.Include(e => e.Guests)
                    .SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
