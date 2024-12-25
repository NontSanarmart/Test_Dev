using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Dev.Model;

namespace Test_Dev.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<List<Employee>> SearchEmployee(Employee employee);
        Task<Employee?> GetEmployeeById(int id);
        Task Create(Employee employee);
        Task Update(Employee employee);
        Task Delete(Employee employee);
        bool Exist(int id);
    }
}