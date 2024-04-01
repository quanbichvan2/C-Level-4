using Lab_5_2_Net_4.Models;

using Microsoft.AspNetCore.Mvc;

namespace Lab_5_2_Net_4.Controllers
{
    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Student Details Page";
            ViewData["Header"] = "Student Details";
            List<Student> students = new()
            {
                new()
                {
                    StudentId = Guid.NewGuid(),
                    Name= "Quan Bích Vân",
                    Brand="Hồ Chí Minh",
                    Section="công viên phần mềm Quang Trung"
                },
                new()
                {
                    StudentId = Guid.NewGuid(),
                    Name= "Yun Quan",
                    Brand="HCM",
                    Section="Q12"
                },
                new()
                {
                    StudentId = Guid.NewGuid(),
                    Name= "Yun Guan",
                    Brand="Chinese",
                    Section="Guang Dong"
                }
            };

            ViewData["Students"] = students;

            return View();
        }
    }
}
