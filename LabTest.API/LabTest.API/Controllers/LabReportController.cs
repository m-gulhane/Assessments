using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabTest.Application.DTO;
using LabTest.Application.IBusinessService;
using LabTest.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabTest.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabReportController : ControllerBase
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable ILabReportBusinessService
        /// </summary>
        private readonly ILabReportBusinessService LabReportBusinessService; 
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="LabReportBusinessService">ILabReportBusinessService</param>
        public LabReportController(ILabReportBusinessService LabReportBusinessService)
        {
            this.LabReportBusinessService = LabReportBusinessService;
        } 
        #endregion

        #region APIs
        /// <summary>
        /// Create labreport API
        /// </summary>
        /// <param name="LabReport">Model to add</param>
        /// <returns>sting status</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create([FromBody] AddLabReportDTO LabReport)
        {
            var reponse = await LabReportBusinessService.SaveLabReport(LabReport,StaticMessage.Add);
            return Ok(reponse);
        }

        /// <summary>
        /// Update labreport API
        /// </summary>
        /// <param name="LabReport">Model to add</param>
        /// <returns>sting status</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<string>> Update([FromBody] AddLabReportDTO LabReport)
        {
            var reponse = await LabReportBusinessService.SaveLabReport(LabReport, StaticMessage.Update);
            return Ok(reponse);
        }

        /// <summary>
        /// Delete labreport API
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>sting status</returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int Id)
        {
            var reponse = await LabReportBusinessService.DeleteLabReport(Id);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the reports based on filters or all if all null API
        /// </summary>
        /// <param name="labReportId"> Filter to fetch report baed on labReportId</param>
        /// <param name="labTestId">Filter to fetch report baed on labTestId</param>
        /// <param name="patientId">Filter to fetch report baed on patientId</param>
        /// <param name="startDate">Filter to fetch report baed on startDate</param>
        /// <param name="endDate">Filter to fetch report baed on endDate</param>
        /// <returns>Colletion of LabReports</returns>
        [HttpGet("GetLabReports")]
        public async Task<ActionResult<List<GetDetailLabReport>>> GetLabReports(int? labReportId, int? labTestId, int patientId, DateTime? startDate, DateTime? endDate)
        {
            var reponse = await LabReportBusinessService.GetDetailLabReports(labReportId, labTestId, patientId, startDate, endDate);
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        } 
        #endregion
    }
}
