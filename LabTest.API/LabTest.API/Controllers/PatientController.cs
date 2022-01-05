using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabTest.Application.DTO;
using LabTest.Application.IBusinessService;
using LabTest.Domain.Models;
using LabTest.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabTest.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable of IPatientBusinessService
        /// </summary>
        private readonly IPatientBusinessService patientBusinessService;
        #endregion

        #region Contructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="patientBusinessService">IPatientBusinessService</param>
        public PatientController(IPatientBusinessService patientBusinessService)
        {
            this.patientBusinessService = patientBusinessService;
        }
        #endregion

        #region APIs
        /// <summary>
        /// Add/Update LabReport
        /// </summary>
        /// <param name="patient">Model to add/update</param>
        /// <returns>status</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create([FromBody] AddPatientDTO patient)
        {
            var reponse = await patientBusinessService.SavePatient(patient,StaticMessage.Add);
            return Ok(reponse);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<string>> Update([FromBody] AddPatientDTO patient)
        {
            var reponse = await patientBusinessService.SavePatient(patient,StaticMessage.Update);
            return Ok(reponse);
        }

        /// <summary> 
        /// Delete record based on filter API
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int Id)
        {
            var reponse = await patientBusinessService.DeletePatient(Id);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the reports based on filters API 
        /// </summary>
        /// <param name="Id">Filter to fetch report based on patient Id</param>
        /// <returns>Colletion of Patient</returns>
        [HttpGet("GetPatientById")]
        public async Task<ActionResult<GetPatientDTO>> GetPatientById(int Id)
        {
            if (Id <= 0) Id = -1;
            var reponse = await patientBusinessService.GetPatients(Id);
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the reports based on filters API 
        /// </summary>
        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<List<GetPatientDTO>>> GetAllPatients()
        {
            var reponse = await patientBusinessService.GetPatients();
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the records based on the filters or all API
        /// </summary>
        /// <param name="patientId">Filter to fetch report based on patientId</param>
        /// <param name="startDate">Filter to fetch report based on startDate</param>
        /// <param name="endDate">Filter to fetch report based on endDate</param>
        /// <returns>Collection of PatientReportModel</returns>
        [HttpGet("GetAllPatientWithReport")]
        public async Task<ActionResult<List<GetPatientReportDTO>>> GetPatientWithReport(int? patientId, DateTime? startDate, DateTime? endDate)
        {
            var reponse = await patientBusinessService.GetPatientWithReport(patientId, startDate, endDate);
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        }

        #endregion
    }
}
