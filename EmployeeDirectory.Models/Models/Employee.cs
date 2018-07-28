using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime JoiningData { get; set; }
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }
        public int DesignationId { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string DesignationName { get; set; }
        public string ImagePath { get; set; }
    }

    public enum Gender
    {
        Male=0,
        Female=1
    }
}