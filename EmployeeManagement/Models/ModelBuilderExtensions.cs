using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
            new Employee
            {
                ID = 1,
                Name = "Ahmed",
                Department = Dept.IT,
                Email = "AhmedElfequi@gmail.com"
            },
                        new Employee
                        {
                            ID = 2,
                            Name = "Mohamed",
                            Department = Dept.HR,
                            Email = "Mohamed@gmail.com"
                        }
            );
        }
    }
}
