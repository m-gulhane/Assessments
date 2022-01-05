using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Application.DTO
{
    public class AddPatientDTO
    {
        /// <summary>
        /// Primary key of Patient
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Patient name
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Patient date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Patient Gender
        /// </summary>
        public int Gender { get; set; }

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
    }
}
