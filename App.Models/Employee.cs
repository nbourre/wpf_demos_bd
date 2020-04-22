using System;

namespace App.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string HomePhone { get; set; }

        public override string ToString() => $"{LastName}, {FirstName}";
        
    }
}
