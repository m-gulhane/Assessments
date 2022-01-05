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
    public class PatientRepository : IPatientRepository
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
        public PatientRepository(LabTestDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update Patient
        /// </summary>
        /// <param name="patient">Model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SavePatient(Patient patient, string action)
        {
            var patientList = await GetPatients(0);

            if (action.Trim().ToLower() == StaticMessage.Update.Trim().ToLower())
            {
                if (patient.Id <= 0 || !UpdatePatient(patientList, patient))
                    return StaticMessage.NotFound;
                else if (CheckPatientExists(patientList, patient))
                    return StaticMessage.PatientExists;
                else
                    return StaticMessage.Success;
            }
            else
            {
                if (CheckPatientExists(patientList, patient))
                    return StaticMessage.PatientExists;
                patient.CreatedOn = DateTime.UtcNow;
                await dbContext.Patients.AddAsync(patient);
                return StaticMessage.Success;
            }

        }


        /// <summary>
        /// Delete record based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeletePatient(int Id)
        {
            string result = StaticMessage.NotFound;
            if (await CheckLabTestInUse(Id))
            {
                result = string.Format(StaticMessage.InUse, "Patient");
            }
            else
            {
                var patientResult = await dbContext.Patients.Where(x => x.Id == Id
                                    && x.IsDeleted == false).FirstOrDefaultAsync(); ;
                if (patientResult != null)
                {
                    patientResult.UpdatedOn = DateTime.UtcNow;
                    patientResult.IsDeleted = true;
                    result = StaticMessage.Success;
                }
            }

            return result;
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report based on patient Id</param>
        /// <returns>Colletion of Patient</returns>
        public async Task<List<Patient>> GetPatients(int Id = 0)
        {
            List<Patient> patientList = await dbContext.Patients.Where(x => x.Id == (Id == 0 ? x.Id : Id)
                             && x.IsDeleted == false)
                            .ToListAsync();
            return patientList;
        }

        /// <summary>
        /// Get all the records based on the filters or all
        /// </summary>
        /// <param name="patientId">Filter to fetch report based on patientId</param>
        /// <param name="startDate">Filter to fetch report based on startDate</param>
        /// <param name="endDate">Filter to fetch report based on endDate</param>
        /// <returns>Collection of Patient</returns>
        public async Task<List<Patient>> GetPatientWithReport(int? patientId, DateTime? startDate, DateTime? endDate)
        {
            List<Patient> patientsModel = new List<Patient>();
            IQueryable<Patient> patients = dbContext.Patients.Where(x => x.IsDeleted == false &&
                                            dbContext.LabReports
                                                .Where(y => y.IsDeleted == false)
                                                .Select(z => z.PatientId).Contains(x.Id));

            if (patientId != null && patientId > 0)
                patients = patients.Where(x => x.Id == patientId);
            if (startDate != null)
                patients = patients.Where(x => x.CreatedOn.Date >= startDate.GetValueOrDefault().Date);
            if (endDate != null)
                patients = patients.Where(x => x.CreatedOn.Date <= endDate.GetValueOrDefault().Date);
            var patientList = await patients.ToListAsync();

            if (patientList.Count > 0)
            {
                foreach (var x in patients)
                {
                    patientsModel.Add(new Patient
                    {
                        Id = x.Id,
                        PatientName = x.PatientName,
                        EmailId = x.EmailId,
                        DateOfBirth = x.DateOfBirth,
                        Address = x.Address,
                        ContactNo = x.ContactNo,
                        Gender = x.Gender,
                        CreatedOn = x.CreatedOn,
                        UpdatedOn = x.UpdatedOn,
                        LabReports = await GetLabReports(x.Id).ConfigureAwait(false)
                    });
                }

            }
            return patientList;
        }
       
        #endregion

        #region Private Methods
        /// <summary>
        /// Update patient details
        /// </summary>
        /// <param name="patientList">collections of the patient </param>
        /// <param name="patient">model to update patient details</param>
        /// <returns>true for the success else false</returns>
        private bool UpdatePatient(List<Patient> patientList, Patient patient)
        {
            var patientRecord = patientList.Where(x => x.Id == patient.Id).FirstOrDefault();
            if (patientRecord != null)
            {
                patientRecord.PatientName = patient.PatientName;
                patientRecord.UpdatedOn = patient.UpdatedOn;
                patientRecord.Gender = patient.Gender;
                patientRecord.EmailId = patient.EmailId;
                patientRecord.ContactNo = patient.ContactNo;
                patientRecord.Address = patient.Address;
                patientRecord.UpdatedOn = DateTime.UtcNow;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientList">collections of the patient </param>
        /// <param name="patient">model to check patient with email or contact</param>
        /// <returns>true for the exists else false</returns>
        private bool CheckPatientExists(List<Patient> patientList, Patient patient)
        {
            var patientCount = patientList.Where(x => x.Id != patient.Id && (x.EmailId.Trim().ToLower() == patient.EmailId.Trim().ToLower()
                            || x.ContactNo == patient.ContactNo)).Count();
            if (patientCount > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Check if patient is in used in Lab Report
        /// </summary>
        /// <param name="id">Patient Id</param>
        /// <returns>true if exists or false</returns>
        private async Task<bool> CheckLabTestInUse(int id)
        {
            var count = await dbContext.LabReports.Where(x => x.PatientId == id && x.IsDeleted == false).CountAsync();
            if (count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get LabReport baed on patient Id
        /// </summary>
        /// <param name="PatientId">Patient Id</param>
        /// <returns>Collections of Labreport</returns>
        private async Task<List<LabReport>> GetLabReports(int PatientId)
        {
            return await dbContext.LabReports
                        .Where(x => x.PatientId == PatientId && x.IsDeleted == false).Include(x => x.LabTestMaster)
                        .ToListAsync();
        }
        #endregion

    }
}
