using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LabTest.Domain.Models;

namespace LabTest.Domain.IDataRepository
{
    public interface ILabReportRepository
    {
        /// <summary>
        /// Declaration of add/update method
        /// </summary>
        /// <param name="labReport">Model to add/update</param>
        /// <returns>status</returns>
        Task<string> SaveLabReport(LabReport labReport, string action);

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
        /// <returns>Colletion of LabReport</returns>
        Task<List<LabReport>> GetLabReports(int Id = 0);

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="labReportId"> Filter to fetch report baed on labReportId</param>
        /// <param name="labTestId">Filter to fetch report baed on labTestId</param>
        /// <param name="patientId">Filter to fetch report baed on patientId</param>
        /// <param name="startDate">Filter to fetch report baed on startDate</param>
        /// <param name="endDate">Filter to fetch report baed on endDate</param>
        /// <returns>Colletion of LabReport</returns>
        Task<List<LabReport>> GetDetailLabReports(int? labReportId, int? labTestId, int? patientId, DateTime? startDate, DateTime? endDate);
    }
}
