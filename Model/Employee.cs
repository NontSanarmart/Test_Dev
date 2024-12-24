using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Dev.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Fullname { get; set; } = null!;
        public string TelNo { get; set; } = null!;
        public int PositionId { get; set; }
        public virtual Position Position { get; set; } = null!;

    }
}