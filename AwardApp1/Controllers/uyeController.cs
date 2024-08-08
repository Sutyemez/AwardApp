using AwardApp1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
//using AwardApp1.Web.Helper;


namespace AwardApp1.Web.Controllers
{
    [Authorize]
    public class uyeController : Controller
    {
        
        private AppDbContext _context;
        //private IHelper _helper;

        public uyeController(AppDbContext context)
        {
            //DI Container
            //dependecy Injection pattern
            _context = context;
            //_helper = helper;

        }


        /*Kullancıı*/
        public async Task<IActionResult> Profile()
        {
            // Kullanıcı bilgilerini Claims'ten al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "Login"); // Kullanıcı giriş yapmamışsa login sayfasına yönlendir
            }

            int userId = int.Parse(userIdClaim);

            // Kullanıcıyı veritabanından al
            var user = await _context.uye.FindAsync(userId);
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 döndür
            }
           return View(user); // Kullanıcı bilgilerini View'a gönder
        }

        /*
        public IActionResult Index()
        {
            var uyes = _context.uye.ToList();
            return View(uyes);



        }
        */
        //LOGİN KULLANICI GETİRME 
        public async Task<IActionResult> Index()
        {
            // Kullanıcı bilgilerini Claims'ten al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "Login"); // Kullanıcı giriş yapmamışsa login sayfasına yönlendir
            }

            int userId = int.Parse(userIdClaim);

            // Kullanıcıyı veritabanından al
            var user = await _context.uye.FindAsync(userId);
            if (user == null)
            {
                return NotFound(); // Kullanıcı bulunamazsa 404 döndür
            }

            ViewData["LoggedInUser"] = user; // Kullanıcı bilgilerini View'a gönder

            var uyes = await _context.uye.ToListAsync();
            return View(uyes);
        }

        public IActionResult Remove(int id)
        {

            var products = _context.uye.Find(id);
            _context.uye.Remove(products);

            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Add()
        {
            //1.Yöntem 
            return View();
        }

        [HttpPost]
        public IActionResult SavePro(uye newProduct)
        {

            _context.uye.Add(newProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarı ile eklendi";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var products = _context.uye.Find(id);
            return View(products);


        }
        [HttpPost]
        public IActionResult Update(uye updateProduct)
        {

            _context.uye.Update(updateProduct);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarı ile güncellendi";
            return RedirectToAction("Index");


        }
    }
}