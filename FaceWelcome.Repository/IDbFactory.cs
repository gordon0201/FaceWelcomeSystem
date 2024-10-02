using FaceWelcome.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository
{
    public interface IDbFactory
    {
        public FaceWelcomeContext InitDbContext();
    }
}
