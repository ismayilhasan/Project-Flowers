using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(x => x.Category).Take(4).ToList();
            return View(products);
        }
                                                                                                
        public IActionResult Details(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);
            return View(product);
        }

        public IActionResult Partial()
        {
            List<Product> products = _dbContext.Products.Include(x => x.Category).Take(4).ToList();
            return PartialView("_ProductItemPartialView", products);
        }
    }
}
