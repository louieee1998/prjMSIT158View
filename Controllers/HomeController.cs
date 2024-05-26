using Microsoft.AspNetCore.Mvc;
using prjMSIT158View.Models;
using System.Diagnostics;

namespace prjMSIT158View.Controllers
{
    //�o��controller�@��MVC
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;
        //�غc�l�`�J------------------------------------------
        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        //�ۤv�g��action => �^��view---------------------------------------
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
        //�t�Φ۰ʤ��ت�action, ���ޥL-------------------------------------------------------
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
