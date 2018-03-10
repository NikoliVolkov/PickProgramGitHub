using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Employee> GetEmployeesUnassigned()
        {
            //var invoicesWithAssigned = new HashSet<int?>(_dbConnection.Invoice.Include(p => p.AssignedEmployee).Select(p => p.AssignedEmployeeId));
            //var result = _dbConnection.Employee.Where(x => !invoicesWithAssigned.Contains(x.EmployeeId));
            //var result = _dbConnection.Employee.Where(x => x.Invoice.Count == 0);
            var result = _dbConnection.Employee.FromSql("dbo.pGetUnassignedEmployees");
            return result;
        }
    }
}
