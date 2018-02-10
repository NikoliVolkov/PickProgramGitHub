using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int employeeId);
    }
}
