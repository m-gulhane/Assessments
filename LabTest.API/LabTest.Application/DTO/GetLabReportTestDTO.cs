using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class GetLabReportTestDTO
    {
        /// <summary>
        /// Patient Id (Primary Key)
        /// </summary>
        public int LabReportId { get; set; }

        /// <summary>
        ///  LabTestId forien key of LabTestMaster
        /// </summary>
        public int LabTestId { get; set; }

        /// <summary>
        /// Test type
        /// </summary>
        public string TestType { get; set; }

        /// <summary>
        /// Sample type
        /// </summary>
        public string SampleType { get; set; }
        /// <summary>
        /// Min limit of Test
        /// </summary>
        public double MinLimit { get; set; }

        /// <summary>
        /// Max Limit of Test
        /// </summary>
        public double MaxLimit { get; set; }

        /// <summary>
        /// Sample received on date time
        /// </summary>
        public DateTime SampleReceivedOn { get; set; }

        /// <summary>
        /// Sample tested on datetime
        /// </summary>
        public DateTime SampleTestedOn { get; set; }

        /// <summary>
        /// Report created on datetime
        /// </summary>
        public DateTime ReportCreatedOn { get; set; }

        /// <summary>
        /// Sample Test Result
        /// </summary>
        public double TestResult { get; set; }

        /// <summary>
        /// Report referred by
        /// </summary>
        public string ReferredBy { get; set; }

        /// <summary>
        /// Descriptions if any
        /// </summary>
        public string ReportDescriptions { get; set; }

        /// <summary>
        /// Descriptions if any
        /// </summary>
        public string TestDescriptions { get; set; }


    }
}
