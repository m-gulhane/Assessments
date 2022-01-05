using LabTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LabTest.Infrastructure.DataContext
{
    public class LabTestDataContext :DbContext
    {
        /// <summary>
        /// Global variable
        /// </summary>
        public IConfiguration Config;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="options">DbContextOptions<LabTestDataContext></param>
        /// <param name="config">IConfiguration</param>
        public LabTestDataContext(DbContextOptions<LabTestDataContext> options,IConfiguration config) 
            : base(options)
        {
            this.Config = config;
        }

        /// <summary>
        /// User DbContext
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Patient DbContext
        /// </summary>
        public DbSet<Patient> Patients { get; set; }

        /// <summary>
        /// LabTests DbContext
        /// </summary>
        public DbSet<LabTestMaster> LabTests { get; set; }

        /// <summary>
        /// LabReports DbContext
        /// </summary>
        public DbSet<LabReport> LabReports { get; set; }
    }
}
