using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {

        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee {ID=1, Name="Ahmed", Email="Ahmed@gmail", Department=Dept.IT},
                new Employee {ID=2, Name="Passant", Email="Passant@gmail", Department=Dept.HR},
                new Employee {ID=3, Name="Adel", Email="Adel@gmail", Department=Dept.Finance}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.ID = _employeeList.Max(e => e.ID) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.ID == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int ID)
        {
            return _employeeList.FirstOrDefault(e => e.ID == ID );
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.ID == employeeChanges.ID);
            if (employee != null)
            {
                employee.ID = employeeChanges.ID;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
