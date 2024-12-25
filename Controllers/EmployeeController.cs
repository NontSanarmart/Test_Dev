using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Dev.Data;
using Test_Dev.DTOs.Request;
using Test_Dev.DTOs.Response;
using Test_Dev.Interface;
using Test_Dev.Model;

namespace Test_Dev.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employeeService;
        public EmployeeController(IEmployee employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllEmployee()
        {
            return (await employeeService.GetAll()).Select(EmployeeResponse.FromEmployee).ToList();
        }


        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> SearchEmployee([FromQuery] SearchEmployee criteria)
        {

            var search = new Employee
            {
                EmployeeId = criteria.EmployeeId,
                Fullname = criteria.Fullname,
                TelNo = criteria.TelNo,
                PositionId = criteria.PositionId
            };

            var employee = (await employeeService.SearchEmployee(search)).Select(EmployeeResponse.FromEmployee).ToList();

            if (employee == null)
            {
                return NotFound(new { message = "ไม่พบข้อมูลที่ค้นหา" });
            }

            return Ok(employee);
        }



        [HttpGet]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById(int id)
        {
            var employee = await employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound(new { message = "ไม่พบข้อมูลที่ค้นหา" });
            }

            return EmployeeResponse.FromEmployee(employee);
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromForm] EmployeeRequest employee)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(new { message = "กรุณากรอกข้อมูลให้ครบถ้วน" });
            }

            var emp = new Employee
            {
                Fullname = employee.Fullname,
                TelNo = employee.TelNo,
                PositionId = employee.PositionId
            };
            await employeeService.Create(emp);
            return CreatedAtAction("GetAllEmployee", new { id = employee.Fullname }, employee);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeRequest employee)
        {
            if (employee.EmployeeId != id || !employeeService.Exist(id))
            {
                return NotFound(new { message = "ข้อผิดพลาด ไม่สามารถแก้ไขได้" });
            }

            if (string.IsNullOrWhiteSpace(employee.Fullname))
            {
                return BadRequest(new { message = "กรุณากรอกชื่อ-สกุล" });
            }

            var existingEmployee = await employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound(new { message = "ไม่พบข้อมูลพนักงานที่ต้องการแก้ไข" });
            }

            existingEmployee.Fullname = employee.Fullname;
            existingEmployee.TelNo = employee.TelNo;
            existingEmployee.PositionId = employee.PositionId;

            await employeeService.Update(existingEmployee);
            return NoContent();
        }



        [HttpDelete]
        public async Task<ActionResult> DeletePosition(int id)
        {
            var employee = await employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            await employeeService.Delete(employee);
            return NoContent();
        }
    }
}