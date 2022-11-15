using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers.ViewComponents
{
    public class DiscountProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public DiscountProductViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = await _dbContext.Products.Where(x => x.Discount != 0).Include(x => x.Category).ToListAsync();
            return View(products);
        }
    }
}
