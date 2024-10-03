using FaceWelcome.Repository.Models;
using FaceWelcome.Service.DTOs.Request.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Service.Services.Interfaces
{
    public interface IOrganizationService
    {
        public Task<Organization> GetOrganizationByCodeAsync(string code);
    }
}
