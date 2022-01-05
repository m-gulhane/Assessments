using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class GetLabTestDTO
    {
        /// <summary>
        /// Primary key of LabTest Master table
        /// </summary>
        public int Id { get; set; }
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
        /// Descriptions if any
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// Lab Test created datetime
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Lab Test updated datetime
        /// </summary>
        public DateTime UpdatedOn { get; set; }
    }
}
