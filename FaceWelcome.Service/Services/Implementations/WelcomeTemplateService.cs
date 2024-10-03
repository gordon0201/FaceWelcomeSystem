using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Implementations
{
    public class WelcomeTemplateService : IWelcomeTemplateService
    {
        private UnitOfWork _unitOfWork;

        public WelcomeTemplateService(IUnitOfWork unitOfWork)

        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
        }

    }
}
