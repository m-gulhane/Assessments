using System;
using System.Collections.Generic;
using System.Text;

namespace LabTest.Shared
{
    public static class Enums
    {
        /// <summary>
        /// Gender Enum
        /// </summary>
        public enum Gender
        {
            None = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }
        /// <summary>
        /// SampleType Enum
        /// </summary>
        public enum SampleType
        {
            None = 0,
            Blood = 1,
            Urine = 2,
            Glucose = 3,
            Lipid = 4,
            Other = 5
        }

        /// <summary>
        /// TestType Enum
        /// </summary>
        public enum TestType
        {
            Chemical = 1,
            Physical = 2
        }
    }
}
