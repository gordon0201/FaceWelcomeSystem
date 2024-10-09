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
        public async Task<GuestImage> GetGuestImageByIdAsync(Guid id)
        {
            return await _dbContext.GuestImages
                .Include(gi => gi.Guest) // Include related Guest if necessary
                .FirstOrDefaultAsync(gi => gi.Id == id);
        }

        // Method to update a GuestImage
        public async Task UpdateGuestImageAsync(GuestImage guestImage)
        {
            _dbContext.GuestImages.Update(guestImage);
            await _dbContext.SaveChangesAsync(); // Save changes after updating
        }

        // Method to delete a GuestImage by ID
        public async Task DeleteGuestImageAsync(GuestImage guestImage)
        {
            if (guestImage == null)
            {
                throw new ArgumentNullException(nameof(guestImage), "Guest image cannot be null");
            }

            _dbContext.GuestImages.Remove(guestImage);
        }


        public async Task<List<GuestImage>> GetAllGuestImagesAsync()
        {
            try
            {
                return await _dbContext.GuestImages.Include(p => p.Guest).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
