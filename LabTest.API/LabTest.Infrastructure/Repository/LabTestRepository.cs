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
    public class LabTestRepository : ILabTestRepository
    {
        #region Global Variables
        /// <summary>
        /// Private read only dbContext
        /// </summary>
        private readonly LabTestDataContext dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContext">Datacontect</param>
        public LabTestRepository(LabTestDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add/Update LabTest
        /// </summary>
        /// <param name="labTest">Model to add/update</param>
        /// <returns>status</returns>
        public async Task<string> SaveLabTest(LabTestMaster labTest, string action)
        {
            if (action.Trim().ToLower() == StaticMessage.Update.Trim().ToLower())
            {
                if (labTest.Id <= 0 || !await UpdateLabTest(labTest))
                    return StaticMessage.NotFound;
                else
                    return StaticMessage.Success;
            }
            else
            {
                labTest.CreatedOn = DateTime.UtcNow;
                await dbContext.LabTests.AddAsync(labTest);
                return StaticMessage.Success;
            }

        }

        /// <summary>
        /// Delete lab test based on filter
        /// </summary>
        /// <param name="Id">filter record to delete based on Id</param>
        /// <returns>status</returns>
        public async Task<string> DeleteLabTest(int Id)
        {

            string result = StaticMessage.NotFound;
            if (await CheckLabTestInUse(Id))
            {
                result = string.Format(StaticMessage.InUse, "LabTest");
            }
            else
            {
                var labTestResult = await dbContext.LabTests.Where(x => x.Id == Id && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
                if (labTestResult != null)
                {
                    labTestResult.UpdatedOn = DateTime.UtcNow;
                    labTestResult.IsDeleted = true;
                    result = StaticMessage.Success;
                }
            }
            return result;
        }

        

        /// <summary>
        /// Get all the lab tests based on filters or all if all null
        /// </summary>
        /// <param name="Id">Filter to fetch report baed on labTestId</param>
        /// <returns>Colletion of LabTestMaster</returns>
        public async Task<List<LabTestMaster>> GetLabTests(int Id = 0)
        {
            return await dbContext.LabTests
                    .Where(x => x.Id == (Id == 0 ? x.Id : Id) && x.IsDeleted == false)
                    .ToListAsync();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Update LabTest
        /// </summary>
        /// <param name="labTestModel">model to update labtest</param>
        /// <returns>true for the success else false</returns>
        private async Task<bool> UpdateLabTest(LabTestMaster labTestModel)
        {
            var labTestList = await GetLabTests(labTestModel.Id);
            var labTest = labTestList.FirstOrDefault();
            if (labTest != null)
            {
                labTest.MaxLimit = labTestModel.MaxLimit;
                labTest.MinLimit = labTestModel.MinLimit;
                labTest.SampleTypeId = labTestModel.SampleTypeId;
                labTest.TestTypeId = labTestModel.TestTypeId;
                labTest.UpdatedOn = DateTime.UtcNow;
                labTest.Descriptions = labTestModel.Descriptions;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if lab test is in used in Lab Report
        /// </summary>
        /// <param name="id">LabTest Id</param>
        /// <returns>true if exists or false</returns>
        private async Task<bool> CheckLabTestInUse(int id)
        {
            var count = await dbContext.LabReports.Where(x => x.LabTestId == id && x.IsDeleted == false).CountAsync();
            if (count > 0)
                return true;
            else
                return false;
        }

        #endregion

    }
}
