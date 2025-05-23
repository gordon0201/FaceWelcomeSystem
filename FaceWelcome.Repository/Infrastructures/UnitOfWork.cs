﻿using FaceWelcome.Repository.FirebaseStorages.Repositories;
using FaceWelcome.Repository.Models;
using FaceWelcome.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private FaceWelcomeContext _dbContext;
        private EventRepository _eventRepository;
        private OrganizationGroupRepository _organizationGroupRepository;
        private GuestRepository _guestRepository;
        private GuestImageRepository _guestImageRepository;
        private OrganizationRepository _organizationRepository;
        private PersonRepository _personRepository;
        private StaffRepository _staffRepository;
        private WelComeTemplateRepository _welcomeTemplateRepository;
        private FirebaseStorageRepository _firebaseStorageRepository;
        private GroupRepository _groupRepository;



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

        public PersonRepository PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new PersonRepository(_dbContext);
                }
                return _personRepository;
            }
        }

        public OrganizationRepository OrganizationRepository
        {
            get
            {
                if (_organizationRepository == null)
                {
                    _organizationRepository = new OrganizationRepository(_dbContext);
                }
                return _organizationRepository;
            }
        }

        public StaffRepository StaffRepository
        {
            get
            {
                if (_staffRepository == null)
                {
                    _staffRepository = new StaffRepository(_dbContext);
                }
                return _staffRepository;
            }
        }

        public WelComeTemplateRepository WelComeTemplateRepository
        {
            get
            {
                if (_welcomeTemplateRepository == null)
                {
                    _welcomeTemplateRepository = new WelComeTemplateRepository(_dbContext);
                }
                return _welcomeTemplateRepository;
            }
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

        public GuestRepository GuestRepository
        {
            get
            {
                if (_guestRepository == null)
                {
                    _guestRepository = new GuestRepository(_dbContext);
                }
                return _guestRepository;
            }
        }
        public GuestImageRepository GuestImageRepository
        {
            get
            {
                if (_guestImageRepository == null)
                {
                    _guestImageRepository = new GuestImageRepository(_dbContext);
                }
                return _guestImageRepository;
            }
        }

        public FirebaseStorageRepository FirebaseStorageRepository
        {
            get
            {
                if (_firebaseStorageRepository == null)
                {
                    _firebaseStorageRepository = new FirebaseStorageRepository(_dbContext);
                }
                return _firebaseStorageRepository;
            }
        }

        public GroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_dbContext);
                }
                return _groupRepository;
            }
        }



        // Thêm phương thức Dispose để giải phóng tài nguyên
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
