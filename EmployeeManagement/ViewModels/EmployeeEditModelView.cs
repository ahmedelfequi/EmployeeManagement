using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeEditModelView : EmployeeCreateViewModel
    {
        public int ID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
