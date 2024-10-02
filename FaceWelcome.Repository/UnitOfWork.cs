using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private FaceWelcomeContext _dbContext;
        private EventRepository _eventRepository;
        private OrganizationGroupRepository _organizationGroupRepository;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
            _dbContext = _dbFactory.InitDbContext();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public OrganizationGroupRepository OrganizationGroupRepository
        {
            get
            {
                if (_organizationGroupRepository == null)
                {
                    _organizationGroupRepository = new OrganizationGroupRepository(_dbContext);
                }
                return _organizationGroupRepository;
            }
        }

        public EventRepository EventRepository
        {
            get
            {
                if (_eventRepository == null)
                {
                    _eventRepository = new EventRepository(_dbContext);
                }
                return _eventRepository;
            }
        }

        // Thêm phương thức Dispose để giải phóng tài nguyên
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
