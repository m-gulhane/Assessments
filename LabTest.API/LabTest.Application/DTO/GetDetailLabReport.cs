using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class GetDetailLabReport: GetLabReportDTO
    {
      
        /// <summary>
        /// Patient Name
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Patient Email id 
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// MinLimit of Test
        /// </summary>
        public double MinLimit { get; set; }

        /// <summary>
        /// MaximumLimit of Test
        /// </summary>
        public double MaxLimit { get; set; }

        /// <summary>
        /// Test Type
        /// </summary>
        public string TestType { get; set; }

        /// <summary>
        /// Sample Type
        /// </summary>
        public string SampleType { get; set; }
    }
}
