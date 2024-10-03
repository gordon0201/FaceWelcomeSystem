using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class GuestImageRepository
    {
        private readonly FaceWelcomeContext _dbContext;

        public GuestImageRepository(FaceWelcomeContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Method to add a new GuestImage
        public async Task AddAsync(GuestImage guestImage)
        {
            await _dbContext.GuestImages.AddAsync(guestImage);
            await _dbContext.SaveChangesAsync(); // Save changes after adding
        }

        // Method to get all GuestImages
        public async Task<List<GuestImage>> GetAllAsync()
        {
            return await _dbContext.GuestImages
                .Include(gi => gi.Guest) // Include related Guest if necessary
                .ToListAsync();
        }

        // Method to get a GuestImage by ID
        public async Task<GuestImage> GetByIdAsync(Guid id)
        {
            return await _dbContext.GuestImages
                .Include(gi => gi.Guest) // Include related Guest if necessary
                .FirstOrDefaultAsync(gi => gi.Id == id);
        }

        // Method to update a GuestImage
        public async Task UpdateAsync(GuestImage guestImage)
        {
            _dbContext.GuestImages.Update(guestImage);
            await _dbContext.SaveChangesAsync(); // Save changes after updating
        }

        // Method to delete a GuestImage by ID
        public async Task DeleteAsync(Guid id)
        {
            var guestImage = await GetByIdAsync(id);
            if (guestImage != null)
            {
                _dbContext.GuestImages.Remove(guestImage);
                await _dbContext.SaveChangesAsync(); // Save changes after deleting
            }
        }
    }
}
