using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LabTest.Domain.Models
{
    public class User
    {
        /// <summary>
        /// Primary key of user
        /// </summary>
        [Key]
        public string UserName { get; set; }

        /// <summary>
        /// password 
        /// </summary>
        public string Password { get; set; }
    }
}
