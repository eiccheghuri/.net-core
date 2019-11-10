using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo2.Models;
using Demo2.ViewModels;

namespace Demo2.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            //PageTitle = "Employee Details";
            return View(model);
        }

        
        public ViewResult Details(int? id)
        {

            HomeDetailsViewModels homeDetailsViewModels = new HomeDetailsViewModels()
            {
                Employee = _employeeRepository.GetEmployee(id??1),
                PageTitle = "Employee Details"
            };
           // Employee model = _employeeRepository.GetEmployee(1);
            // ViewData["Employee"] = model;
            //ViewData["PageTitle"] = "Employee Details";

           // ViewBag.Employee = model;


          //  ViewBag.PageTitle = "Employee Details";
            return View(homeDetailsViewModels);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();

          
        }
    }
}
