using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Dev.Model;

namespace Test_Dev.DTOs.Response
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string Fullname { get; set; } = "";
        public string TelNo { get; set; } = "";
        public string PositionName { get; set; } = "";

        public static EmployeeResponse FromEmployee(Employee employee)
        {
            return new EmployeeResponse
            {
                EmployeeId = employee.EmployeeId,
                Fullname = employee.Fullname,
                TelNo = employee.TelNo,
                PositionName = employee.Position.PositionName
            };
        }
    }
}