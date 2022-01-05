using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LabTest.Domain.Models
{
    public class LabReport
    {
        /// <summary>
        /// Patient Id (Primary Key)
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        ///  LabTestId forien key of LabTestMaster
        /// </summary>
        [ForeignKey("LabTestMaster")]
        public int LabTestId { get; set; }

        /// <summary>
        ///  PatientId forien key of Patient
        /// </summary>
        [ForeignKey("Patient")]
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
        /// Report updated on datetime
        /// </summary>
        public DateTime UpdatedOn { get; set; }
        /// <summary>
        /// Flag for soft delete
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Descriptions if any
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// Collection of LabTestMaster
        /// </summary>
        public virtual LabTestMaster LabTestMaster { get; set; }

        /// <summary>
        /// Collections of Patient
        /// </summary>
        public virtual Patient Patient { get; set; }
    }
}
