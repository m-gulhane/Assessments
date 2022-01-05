using LabTest.Application.IBusinessService;
using LabTest.Application.Services;
using LabTest.Domain.IDataRepository;
using LabTest.Domain.Interfaces;
using LabTest.Infrastructure.DataContext;
using LabTest.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabTest.Application
{
    public static class ApplicationExtension
    {
        public static void ConfigureApplicationDependencyInjection(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<DbContext, LabTestDataContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserBusinessService, UserBusinessService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPatientBusinessService, PatientBusinessService>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<ILabTestBusinessService, LabTestBusinessService>();
            services.AddTransient<ILabTestRepository, LabTestRepository>();
            services.AddTransient<ILabReportBusinessService, LabReportBusinessService>();
            services.AddTransient<ILabReportRepository, LabReportRepository>();

        }
    }
}
