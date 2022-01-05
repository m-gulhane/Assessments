using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class AddLabReportDTO
    {
        /// <summary>
        /// Patient Id (Primary Key)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  LabTestId forien key of LabTestMaster
        /// </summary>
        public int LabTestId { get; set; }

        /// <summary>
        ///  PatientId forien key of Patient
        /// </summary>
        public int PatientId { get; set; }

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
        public string Descriptions { get; set; }
    }
}
