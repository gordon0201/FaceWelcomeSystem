﻿using FaceWelcome.Repository.Models;
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


        public async Task<Event> GetEventByCodeAsync(string code)
        {
            try
            {
                return await _dbContext.Events
                    .Include(e => e.Groups)
                    .Include(e => e.Guests)
                    .Include(e => e.Staff)
                    .Include(e => e.WelcomeTemplates)
                    .SingleOrDefaultAsync(e => e.Code == code);
            }
            catch (Exception ex)
            {
                throw new Exception(code, ex);
            }
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

        #region Add event
        public async Task AddAsync(Event eventRequest)
        {
            try
            {
                await _dbContext.Events.AddAsync(eventRequest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
