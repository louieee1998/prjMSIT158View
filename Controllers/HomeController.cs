using Microsoft.AspNetCore.Mvc;
using prjMSIT158View.Models;
using System.Diagnostics;

namespace prjMSIT158View.Controllers
{
    //這個controller作為MVC
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;
        //建構子注入------------------------------------------
        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //自己寫的action => 回傳view---------------------------------------
        public IActionResult fetchTest()
        {
            return View();
        }
        public IActionResult selectCity()
        {
            return View();
        }
        public IActionResult registerMember()
        {
            return View();
        }
        public IActionResult showTaipeiSpots()
        {
            return View();
        }
        //系統自動內建的action, 不管他-------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
