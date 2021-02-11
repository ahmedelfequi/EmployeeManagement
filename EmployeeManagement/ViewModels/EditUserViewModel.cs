using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
            Claims = new List<string>(); 
            }
            
        public String Id { get; set; }

        [Required]
        public String UserName { get; set; }
        public String City { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        public IList<string> Roles { get; set; }
        public List<string> Claims { get; set; }


    }
}

