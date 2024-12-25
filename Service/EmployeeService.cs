using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test_Dev.Data;
using Test_Dev.Interface;
using Test_Dev.Model;

namespace Test_Dev.Service
{
    public class EmployeeService : IEmployee
    {

        private readonly DataContext context;

        public EmployeeService(DataContext context)
        {
            this.context = context;

        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await context.employees.Include(p => p.Position).ToListAsync();
        }


        public async Task Create(Employee employee)
        {
            context.employees.Add(employee);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Employee employee)
        {
            context.employees.Remove(employee);
            await context.SaveChangesAsync();
        }

        public bool Exist(int id)
        {
            return context.employees.Any(e => e.EmployeeId == id);
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await (from e in context.employees
                          join p in context.positions on e.PositionId equals p.PositionId
                          where e.EmployeeId == id
                          select new Employee
                          {
                              EmployeeId = e.EmployeeId,
                              Fullname = e.Fullname,
                              TelNo = e.TelNo,
                              PositionId = e.PositionId,
                              Position = p
                          }).FirstOrDefaultAsync();
        }

        public async Task Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<List<Employee>> SearchEmployee(Employee employee)
        {
            var query = context.employees.Include(p => p.Position).AsQueryable();

            if (employee?.EmployeeId > 0)
            {
                query = query.Where(x => x.EmployeeId == employee.EmployeeId);
            }

            if (!string.IsNullOrWhiteSpace(employee?.Fullname))
            {
                query = query.Where(x => x.Fullname.Contains(employee.Fullname));
            }

            if (!string.IsNullOrWhiteSpace(employee?.TelNo))
            {
                query = query.Where(x => x.TelNo.StartsWith(employee.TelNo));
            }

            if (employee?.PositionId > 0)
            {
                query = query.Where(x => x.PositionId == employee.PositionId);
            }

            return (await query.ToListAsync()).Cast<Employee>().ToList();
        }
    }
}