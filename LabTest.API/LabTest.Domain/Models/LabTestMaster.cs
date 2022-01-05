using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LabTest.Domain.Models
{
   public class LabTestMaster
    {
        /// <summary>
        /// Contructor to define collection initially
        /// </summary>
        public LabTestMaster()
        {
            LabReports = new HashSet<LabReport>();
        }
        /// <summary>
        /// Primary key of LabTest Master table
        /// </summary>
        [Key]
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

        /// <summary>
        /// Lab Test created datetime
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        /// <summary>
        /// Lab Test updated datetime
        /// </summary>
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Flag for soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Collections of LabReport
        /// </summary>
        public virtual ICollection<LabReport> LabReports { get; set; }
    }
}
