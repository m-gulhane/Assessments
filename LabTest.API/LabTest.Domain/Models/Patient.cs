using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LabTest.Domain.Models
{
    public class Patient
    {
        public Patient()
        {
            LabReports = new HashSet<LabReport>();
        }

        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Patient name
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Patient date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        public int? Gender { get; set; }
        /// <summary>
        /// Patient Email id
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Patient contact no
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// Patient address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Patient report created on datetime
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Patient report updated on datetime
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
