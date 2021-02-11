using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(IEmployeeRepository employeeRepository,
                                IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public ViewResult index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        public ViewResult details(int? id)
        {

            // throw new Exception("Hello Error gamd gdn");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditModelView employeeEditModelView = new EmployeeEditModelView
            {
                ID = employee.ID,
                Email = employee.Email,
                Name = employee.Name,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditModelView);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditModelView model)
        {

            if (ModelState.IsValid)
            {
                Employee Employee = _employeeRepository.GetEmployee(model.ID);
                Employee.Name = model.Name;
                Employee.Email = model.Email;
                Employee.Department = model.Department;

                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    Employee.PhotoPath = ProcessUploadedPhoto(model);
                }


                _employeeRepository.Update(Employee);
                return RedirectToAction("index");
            }

            return View();
        }


        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            string UniqueFileName = null;

            if (ModelState.IsValid)
            {
                string UploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(UploadsFolder, UniqueFileName);
                model.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = UniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.ID });
            }

            return View();
        }

        private string ProcessUploadedPhoto(EmployeeEditModelView model)
        {
            string UniqueFileName = null;

            string UploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/");
            UniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string FilePath = Path.Combine(UploadsFolder, UniqueFileName);
            using(var fileStream = new FileStream(FilePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
            
            return UniqueFileName;
        }


    }
}
