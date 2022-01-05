using System;

namespace LabTest.Shared
{
    public static class StaticMessage
    {
        public static string Success ="Success";
        public static string NotFound = "No record found";
        public static string NotExists = "{0} does not exists";
        public static string PatientExists = "Patient exists with same emailid or contact no.";
        public static string InUse = "{0} can not be deleted as it is being used in Lab Report";
        public static string Add = "Add";
        public static string Update = "Update";
    }
}
