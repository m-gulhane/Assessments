using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Domain.Models;

namespace LabTest.Domain.IDataRepository
{
    public interface ILabTestRepository
    {
        /// <summary>
        /// Declaration of Add/Update method
        /// </summary>
        /// <param name="labTest">Model to add/update</param>
        /// <returns>status</returns>
        Task<string> SaveLabTest(LabTestMaster labTest, string action);

        /// <summary>
        /// Declaration of delete lab test based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        Task<string> DeleteLabTest(int Id);

        /// <summary>
        /// Declaration of get all the lab tests based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of LabTestMaster</returns>
        Task<List<LabTestMaster>> GetLabTests(int Id = 0);
    }
}
