using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Application.DTO;

namespace LabTest.Application.IBusinessService
{
    public interface ILabReportBusinessService
    {
        /// <summary>
        /// Declaration of add/update method
        /// </summary>
        /// <param name="labReportDto">Model to add/update</param>
        /// <returns>status</returns>
        Task<string> SaveLabReport(AddLabReportDTO labReportDto, string action);

        /// <summary>
        /// Declaration of delete lab report method
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        Task<string> DeleteLabReport(int Id);

        /// <summary>
        ///  Declaration of get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labReportId</param>
        /// <returns>Colletion of GetLabReportDTO</returns>
        Task<List<GetLabReportDTO>> GetLabReports(int Id = 0);

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="labReportId"> Filter to fetch report baed on labReportId</param>
        /// <param name="labTestId">Filter to fetch report baed on labTestId</param>
        /// <param name="patientId">Filter to fetch report baed on patientId</param>
        /// <param name="startDate">Filter to fetch report baed on startDate</param>
        /// <param name="endDate">Filter to fetch report baed on endDate</param>
        /// <returns>Colletion of GetDetailLabReport</returns>
        Task<List<GetDetailLabReport>> GetDetailLabReports(int? labReportId,int? labTestId, int? patientId, DateTime? startDate, DateTime? endDate);
    }
}
