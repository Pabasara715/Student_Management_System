using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Models
{
    public class Student
    {        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public double GPA { get; set; } = 0;
    }
}
