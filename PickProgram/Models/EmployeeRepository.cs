using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickProgram.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly InvoiceTrackerContext _dbConnection;
        public EmployeeRepository(InvoiceTrackerContext dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _dbConnection.Employee.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbConnection.Employee;
        }
    }
}
