﻿using Lab_5_3_Net_4.Models;

using Microsoft.AspNetCore.Mvc;

namespace Lab_5_3_Net_4.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Employee Details Page";
            List<Employee> Employees = new()
            {
                new()
                {
                    EmployeeId = Guid.NewGuid(),
                    Name= "Quan Bích Vân",
                    Address="Ho Chi Minh",
                    Phone="+84 2020202020",
                },
                new()
                {
                    EmployeeId = Guid.NewGuid(),
                    Name= "Yun Quan",
                    Address="Ha Noi",
                    Phone="+84 1234567890"
                },
                new()
                {
                    EmployeeId = Guid.NewGuid(),
                    Name= "Yun Guan",
                    Address="Guang Dong",
                    Phone="+84 0987654321"
                }
            };
            //Dùng viewBag
            ViewBag.Employees = Employees;

            return View();
        }
    }
}
