using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class AddLabTestDTO
    {
        /// <summary>
        /// Primary key of LabTest Master table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Test Type Id
        /// </summary>
        public int TestTypeId { get; set; }

        /// <summary>
        /// Sample Type Id
        /// </summary>
        public int SampleTypeId { get; set; }
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

    }
}
