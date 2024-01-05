using System;
using System.Collections.Generic;

namespace DatabaseFirstEFCore6.Models
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? Salary { get; set; }
    }
}
