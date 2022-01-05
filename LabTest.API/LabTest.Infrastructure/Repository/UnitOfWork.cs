using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Domain.IDataRepository;
using LabTest.Domain.Interfaces;
using LabTest.Infrastructure.DataContext;

namespace LabTest.Infrastructure.Repository
{
    public class UnitOfWork :IDisposable, IUnitOfWork
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable for DbContext
        /// </summary>
        private readonly LabTestDataContext dbContext;

        /// <summary>
        /// Private read only variable for service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Private variable to use in dispose method
        /// </summary>
        private bool disposed = false; 
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContext">LabTestDataContext</param>
        /// <param name="serviceProvider">IServiceProvider</param>
        public UnitOfWork(LabTestDataContext dbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
        }
        #endregion

        #region Implementaions
        /// <summary>
        /// Implemented IUserReporsitory
        /// </summary>
        public IUserRepository User
        {
            get
            {
                return serviceProvider.GetService(typeof(IUserRepository)) as IUserRepository;
            }
        }

        /// <summary>
        /// Implemented IPatientRepository
        /// </summary>
        public IPatientRepository Patient
        {
            get
            {
                return serviceProvider.GetService(typeof(IPatientRepository)) as IPatientRepository;
            }
        }

        /// <summary>
        /// Implemented ILabTestRepository
        /// </summary>
        public ILabTestRepository LabTest
        {
            get
            {
                return serviceProvider.GetService(typeof(ILabTestRepository)) as ILabTestRepository;
            }
        }

        /// <summary>
        /// Implemented ILabReportRepository
        /// </summary>
        public ILabReportRepository LabReport
        {
            get
            {
                return serviceProvider.GetService(typeof(ILabReportRepository)) as ILabReportRepository;
            }
        }

        /// <summary>
        /// Async method to save the changes in the dbcontext
        /// </summary>
        /// <returns>int</returns>
        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose the context
        /// </summary>
        /// <param name="disposing">boolean value to check dispose status</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// call the garbage collector
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
