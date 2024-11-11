using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class GuestRepository
    {
        private FaceWelcomeContext _dbContext;

        public GuestRepository(FaceWelcomeContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public async Task<Guest> GetGuestByEventIdAndPersonIdAsync(Guid eventId, Guid personId)
        {
            return await _dbContext.Guests
                .Include(g => g.Event)
                .Include(g => g.Person)
                .SingleOrDefaultAsync(g => g.PersonId == personId && g.EventId == eventId);
        }

        public async Task<List<Guest>> GetGuestsByEventIdAsync(Guid eventId)
        {
            return await _dbContext.Guests
                .Where(g => g.EventId == eventId)
                .ToListAsync();
        }

        public async Task AddAsync(Guest guest)
        {
            await _dbContext.Guests.AddAsync(guest);
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            return await _dbContext.Guests
                .Include(g => g.Event) // Event might be null if not assigned
                .Include(g => g.Group) // Group might be null if not assigned
                .Include(g => g.Person) // Person might be null if not assigned
                .ToListAsync();
        }


        public async Task<Guest> GetByIdAsync(Guid id)
        {
            return await _dbContext.Guests
                .Include(g => g.Event)  
                .Include(g => g.Group)  
                .Include(g => g.Person)  
                .FirstOrDefaultAsync(g => g.Id == id);
        }


        public async Task UpdateAsync(Guest guest)
        {
            _dbContext.Guests.Update(guest);
            await _dbContext.SaveChangesAsync(); // Lưu thay đổi
        }

        public async Task DeleteGuestAsync(Guest guest)
        {
            if (guest == null)
            {
                throw new ArgumentNullException(nameof(guest), "guest cannot be null");
            }

            _dbContext.Guests.Remove(guest);
        }

    }
}
