using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabTest.Application.DTO;
using LabTest.Application.IBusinessService;
using LabTest.Domain.Models;
using LabTest.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabTest.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestController : ControllerBase
    {
        #region Global Variables
        /// <summary>
        /// Private read only variable of ILabTestBusinessService
        /// </summary>
        private readonly ILabTestBusinessService LabTestBusinessService;
        #endregion

        #region Contructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="LabTestBusinessService">ILabTestBusinessService</param>
        public LabTestController(ILabTestBusinessService LabTestBusinessService)
        {
            this.LabTestBusinessService = LabTestBusinessService;
        }
        #endregion

        #region APIs

        /// <summary>
        /// Create Lab Test API
        /// </summary>
        /// <param name="labTest">Model to add</param>
        /// <returns>status</returns>
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create([FromBody] AddLabTestDTO LabTest)
        {
            var reponse = await LabTestBusinessService.SaveLabTest(LabTest, StaticMessage.Add);
            return Ok(reponse);
        }

        /// <summary>
        /// Update Lab Test API
        /// </summary>
        /// <param name="labTest">Model to update</param>
        /// <returns>status</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<string>> Update([FromBody] AddLabTestDTO LabTest)
        {
            var reponse = await LabTestBusinessService.SaveLabTest(LabTest, StaticMessage.Update);
            return Ok(reponse);
        }

        /// <summary>
        /// Delete lab test API
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int Id)
        {
            var reponse = await LabTestBusinessService.DeleteLabTest(Id);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the lab tests based on filters API
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of LabReports</returns>
        [HttpDelete("GetLabTestById")]
        public async Task<ActionResult<GetLabTestDTO>> GetLabTestById(int Id)
        {
            if (Id <= 0) Id = -1;
            var reponse = await LabTestBusinessService.GetLabTests(Id);
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        }

        /// <summary>
        /// Get all the lab tests based on filters or all if all null API
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of GetLabTestDTO</returns>
        [HttpDelete("GetAllLabTest")]
        public async Task<ActionResult<GetLabTestDTO>> GetAllLabTest()
        {
            var reponse = await LabTestBusinessService.GetLabTests();
            if (reponse == null || reponse.Count <= 0)
                return Ok(StaticMessage.NotFound);
            return Ok(reponse);
        } 
        #endregion

    }
}
