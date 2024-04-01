using System.Diagnostics;

using Lab_7_1_Net_4.Models;

using Microsoft.AspNetCore.Mvc;

namespace Lab_7_1_Net_4.Controllers
{
    public class HomeController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] được sử dụng trong ASP.NET Core để chỉ định cách quản lý bộ nhớ cache cho các phản hồi từ một action trong controller.
        // NoStore = true: Xác định rằng không cần lưu trữ bản sao của phản hồi trong bộ nhớ cache. 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //Activity.Current?.Id: Đây là cách truy cập vào Id của hoạt động (activity) được sử dụng để tránh lỗi Null Reference Exception nếu Activity.Current là null.
            //HttpContext.TraceIdentifier: Đây là một định danh duy nhất cho mỗi yêu cầu HTTP. Nó được sử dụng trong trường hợp Activity.Current không tồn tại hoặc không chứa Id.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}