using Microsoft.AspNetCore.Mvc;
using prjMSIT158View.DTO;
using prjMSIT158View.Models;

namespace prjMSIT158View.Controllers
{
    //這個controller作為API
    public class APIController : Controller
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _environment;

        //建構子注入------------------------------------------
        public APIController(MyDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        //自己寫的action => 回傳json---------------------------------------
        //傳純文字
        public IActionResult returnContent()
        {
            System.Threading.Thread.Sleep(10000);
            return Content("世界你好", "text/plain", System.Text.Encoding.UTF8);
        }
        //傳圖檔
        public IActionResult returnFile(int id = 1)
        {
            Member? member = _context.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                if (img != null)
                {
                    return File(img, "image/jpeg");
                }
            }
            return NotFound();
        }
        //下拉選單 城市>鄉鎮區>道路
        public IActionResult returnCity()
        {
            var datas = _context.Addresses.Select(x => x.City).Distinct();
            return Json(datas);
        }
        public IActionResult returnDist(string city)//接收前端傳來的所選城市 //query string的key就是參數
        {
            var datas = _context.Addresses.Where(x => x.City==city).Select(x => x.SiteId).Distinct();
            return Json(datas);
        }
        public IActionResult returnRoad(string dist)//接收前端傳來的所選鄉鎮區 //query string的key就是參數
        {
            var datas = _context.Addresses.Where(x => x.SiteId==dist).Select(x => x.Road);
            return Json(datas);
        }
        //會員註冊表單
        public IActionResult checkAccount(string name)
        {
            //Any: 有/無結果只回傳True/False => 減少資料傳輸量
            var isUsed = _context.Members.Any(x => x.Name == name);
            return Content(isUsed.ToString(), "text/plain", System.Text.Encoding.UTF8);
        }
        //public IActionResult showMemberInfo(string name, string email, int age = 20)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        name = "訪客";

        //    return Content($"Hello {name}，{age} 歲了，電子郵件是 {email}", "text/html", System.Text.Encoding.UTF8);
        //}
        public IActionResult showMemberInfo([FromForm] Member member, IFormFile avatar)
        {
            if (string.IsNullOrEmpty(member.Name))
                member.Name = "訪客";
           
            //照片存到本地硬碟
            string path = Path.Combine(_environment.WebRootPath, "Uploads", avatar.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                avatar.CopyTo(fileStream);
            }
            //照片轉二進位存到資料庫
            byte[]? imgByte = null; //? 可為null
            using (var memoryStream = new MemoryStream())
            {
                avatar.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }

            //寫進資料庫
            member.FileName = avatar.FileName;
            member.FileData = imgByte;
            _context.Members.Add(member);
            _context.SaveChanges();
            
            string info = $"{avatar.FileName} - {avatar.Length} - {avatar.ContentType}";
            return Content($"Hello {member.Name}，{member.Age} 歲了，電子郵件是 {member.Email}。上傳照片資訊: {info}", "text/plain", System.Text.Encoding.UTF8);
        }

        public IActionResult searchSpots([FromBody]SearchDTO search)
        {
            //根據分類編號搜尋景點資料
            var spots = search.categoryId == 0 ? _context.SpotImagesSpots : _context.SpotImagesSpots.Where(s => s.CategoryId == search.categoryId);

            //根據關鍵字搜尋景點資料(title、desc)
            if (!string.IsNullOrEmpty(search.keyword))
            {
                spots = spots.Where(s => s.SpotTitle.Contains(search.keyword) || s.SpotDescription.Contains(search.keyword));
            }

            //總共有多少筆資料
            int totalCount = spots.Count();
            //每頁要顯示幾筆資料
            int pageSize = search.pageSize;   
            //目前第幾頁
            int page = search.page;
            //計算總共有幾頁
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //分頁
            spots = spots.Skip((page - 1) * pageSize).Take(pageSize);

            //包裝要傳給client端的資料
            SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            spotsPaging.TotalCount = totalCount;
            spotsPaging.TotalPages = totalPages;
            spotsPaging.SpotsResult = spots.ToList();

            return Json(spotsPaging);
        }

        //系統自動內建的action, 不管他-------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }
    }
}
