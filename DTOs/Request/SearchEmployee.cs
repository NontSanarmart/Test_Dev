using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Dev.DTOs.Request
{
    public class SearchEmployee
    {
        public int EmployeeId { get; set; }
        public string Fullname { get; set; } = "";
        public string TelNo { get; set; } = "";
        public int PositionId { get; set; }
    }
}