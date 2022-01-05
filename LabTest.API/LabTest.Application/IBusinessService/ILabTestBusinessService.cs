using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Application.DTO;

namespace LabTest.Application.IBusinessService
{
    public interface ILabTestBusinessService
    {
        /// <summary>
        /// Declaration of Add/Update method
        /// </summary>
        /// <param name="labTestDto">Model to add/update</param>
        /// <returns>status</returns>
        Task<string> SaveLabTest(AddLabTestDTO labTestDto, string action);

        /// <summary>
        /// Declaration of Delete lab test based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        Task<string> DeleteLabTest(int Id);

        /// <summary>
        /// Declaration of get all the lab tests based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of GetLabTestDTO</returns>
        Task<List<GetLabTestDTO>> GetLabTests(int Id = 0);
    }
}
