using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models;
using FronttoBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FronttoBackFlowers.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("session", "Hello");
            Response.Cookies.Append("cookie", "p324");

            var sliderImages = _dbContext.SliderImages.ToList();
            var slider = _dbContext.Sliders.SingleOrDefault();
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.ToList();
            var homeViewModel = new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products

            };

            return View(homeViewModel);
        }

        public IActionResult Search(string searchText)
        {
            var products = _dbContext.Products.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList();

            return Json(products);
        }

        public IActionResult Basket()
        {
            var basketJson = Request.Cookies["basket"];
            var products =  JsonConvert.DeserializeObject<List<Product>>(basketJson);
            return Json(products);
        }

        public IActionResult AddToBasket(int id)
        {
            var product = _dbContext.Products.Include(x => x.Category).SingleOrDefault(x => x.Id == id);
            if(product == null)
                return NotFound();

            var products = new List<Product>();
            products.Add(product);
 
            var productJson = JsonConvert.SerializeObject(products, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Error }); ;


            Response.Cookies.Append("basket",productJson);

            return RedirectToAction(nameof(Basket));
        }
    }
}
