using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabTest.Domain.IDataRepository;
using LabTest.Domain.Models;
using LabTest.Infrastructure.DataContext;
using LabTest.Shared;
using Microsoft.EntityFrameworkCore;

namespace LabTest.Infrastructure.Repository
{
    public class LabReportRepository : ILabReportRepository
    {
        #region Global Variables
        /// <summary>
        /// Private read only dbContext
        /// </summary>
        private readonly LabTestDataContext dbContext;
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContext">Datacontect</param>
        public LabReportRepository(LabTestDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update LabReport
        /// </summary>
        /// <param name="labReportModel">Labreport model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SaveLabReport(LabReport labReport, string action)
        {
            var result = CheckLabTestPatient(labReport.LabTestId, labReport.PatientId);
            if (result != string.Empty)
                return result;

            if (action.Trim().ToLower() == StaticMessage.Update.Trim().ToLower())
            {
                if (labReport.Id <= 0 || !await UpdateLabReport(labReport))
                    return StaticMessage.NotFound;
                else
                    return StaticMessage.Success;
            }
            else
            {
                labReport.ReportCreatedOn = DateTime.UtcNow;
                await dbContext.LabReports.AddAsync(labReport);
                return StaticMessage.Success;
            }

        }

        /// <summary>
        /// Delete lab report baed on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeleteLabReport(int Id)
        {

            string result = StaticMessage.NotFound;
            var labReportResult = await dbContext.LabReports.Where(x => x.Id == Id && x.IsDeleted == false)
                                    .FirstOrDefaultAsync();
            if (labReportResult != null)
            {
                labReportResult.UpdatedOn = DateTime.UtcNow;
                labReportResult.IsDeleted = true;
                result = StaticMessage.Success;
            }

            return result;
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labReportId</param>
        /// <returns>Colletion of LabReports</returns>
        public async Task<List<LabReport>> GetLabReports(int Id = 0)
        {
            return await dbContext.LabReports
                            .Where(x => x.Id == (Id == 0 ? x.Id : Id) && x.IsDeleted == false)
                            .ToListAsync();
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="labReportId"> Filter to fetch report baed on labReportId</param>
        /// <param name="labTestId">Filter to fetch report baed on labTestId</param>
        /// <param name="patientId">Filter to fetch report baed on patientId</param>
        /// <param name="startDate">Filter to fetch report baed on startDate</param>
        /// <param name="endDate">Filter to fetch report baed on endDate</param>
        /// <returns>Colletion of LabReports</returns>
        public async Task<List<LabReport>> GetDetailLabReports(int? labReportId, int? labTestId, int? patientId, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<LabReport> labReports = (from x in dbContext.LabReports
                                                join y in dbContext.LabTests
                                                on x.LabTestId equals y.Id
                                                join z in dbContext.Patients
                                                on x.PatientId equals z.Id
                                                where x.IsDeleted == false && y.IsDeleted == false
                                                && z.IsDeleted == false
                                                select x);
            if (labReportId != null && labReportId > 0)
                labReports = labReports.Where(x => x.Id == labReportId);
            if (labTestId != null && labTestId > 0)
                labReports = labReports.Where(x => x.LabTestId == labTestId);
            if (patientId != null && patientId > 0)
                labReports = labReports.Where(x => x.PatientId == patientId);
            if (startDate != null)
                labReports = labReports.Where(x => x.ReportCreatedOn.Date >= startDate.GetValueOrDefault().Date);
            if (endDate != null)
                labReports = labReports.Where(x => x.ReportCreatedOn.Date <= endDate.GetValueOrDefault().Date);
            return await labReports
                            .Include(x => x.LabTestMaster).Include(x => x.Patient)
                            .ToListAsync();

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Update labreport
        /// </summary>
        /// <param name="labReportModel">model to update labreport</param>
        /// <returns>true for the success else false</returns>
        private async Task<bool> UpdateLabReport(LabReport labReportModel)
        {
            var labReportResult = await GetLabReports(labReportModel.Id);
            var labReport = labReportResult.FirstOrDefault();
            if (labReport != null)
            {
                labReport.LabTestId = labReportModel.LabTestId;
                labReport.PatientId = labReportModel.PatientId;
                labReport.ReferredBy = labReportModel.ReferredBy;
                labReport.SampleReceivedOn = labReportModel.SampleReceivedOn;
                labReport.UpdatedOn = DateTime.UtcNow;
                labReport.TestResult = labReportModel.TestResult;
                labReport.Descriptions = labReportModel.Descriptions;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checl LabTest and Patient exists
        /// </summary>
        /// <param name="labTestId">LabTest Id to check exists</param>
        /// <param name="patientId">Patient Id to check exists</param>
        /// <returns>string status else empty</returns>
        private string CheckLabTestPatient(int labTestId, int patientId)
        {
            var labTestCount = dbContext.LabTests.Where(x => x.Id == labTestId && x.IsDeleted == false).Count();
            if (labTestCount <= 0)
            {
                return string.Format(StaticMessage.NotExists, "Lab Test");
            }
            var patientCount = dbContext.Patients.Where(x => x.Id == patientId && x.IsDeleted == false).Count();
            if (patientCount <= 0)
            {
                return string.Format(StaticMessage.NotExists, "Patient");
            }
            return string.Empty;
        } 
        #endregion
        
    }
}
