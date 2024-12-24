using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Dev.Model
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }
        public string PositionName { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}