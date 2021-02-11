using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int ID);
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee);
        public Employee Delete(int id);
        public Employee Update(Employee employeeChanges);
    }
}
