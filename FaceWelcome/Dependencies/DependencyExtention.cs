using FaceWelcome.Repository.Infrastructures;
using FaceWelcome.Service.Services.Implementations;
using FaceWelcome.Service.Services.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FaceWelcome.API.Dependencies
{
    public static class DependencyExtention
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDbFactory(this IServiceCollection services)
        {
            services.AddScoped<IDbFactory, DbFactory>();
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IOrganizationGroupService, OrganizationGroupService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IWelcomeTemplateService, WelcomeTemplateService>();

            return services;
        }

    }
}
