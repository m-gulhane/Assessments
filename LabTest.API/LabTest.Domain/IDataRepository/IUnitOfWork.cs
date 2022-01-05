using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Domain.IDataRepository;

namespace LabTest.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Declaration of IUserRepository
        /// </summary>
        IUserRepository User { get; }

        /// <summary>
        /// Declaration of IPatientRepository
        /// </summary>
        IPatientRepository Patient { get; }

        /// <summary>
        /// Declaration of ILabTestRepository
        /// </summary>
        ILabTestRepository LabTest { get; }

        /// <summary>
        /// Declaration of ILabReportRepository
        /// </summary>
        ILabReportRepository LabReport { get; }
        
        /// <summary>
        /// Declaration of SaveAsync
        /// </summary>
        Task<int> SaveAsync();
    }
}
