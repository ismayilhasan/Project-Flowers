using FronttoBackFlowers.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ProductViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _dbContext.Products.ToListAsync();

            return View(products);
        }
    }
}
