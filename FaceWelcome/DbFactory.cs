using FaceWelcome.Repository;
using FaceWelcome.Repository.Models;

namespace FaceWelcome.API
{
    public class DbFactory : IDbFactory
    {
        private FaceWelcomeContext _dbContext;

        public FaceWelcomeContext InitDbContext()
        {
            if(_dbContext == null)
            {
                _dbContext = new FaceWelcomeContext();
            }
            return _dbContext;
        }
    }
}
