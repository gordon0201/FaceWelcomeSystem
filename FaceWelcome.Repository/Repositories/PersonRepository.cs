using FaceWelcome.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Repositories
{
    public class PersonRepository
    {
        private readonly FaceWelcomeContext _dbContext;
        public PersonRepository(FaceWelcomeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Person person)
        {
            try
            {
                _dbContext.People.AddAsync(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Person> GetPersonByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.People.Include(person => person.Organization).SingleOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            try
            {
                return await _dbContext.People.Include(p => p.Organization).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
