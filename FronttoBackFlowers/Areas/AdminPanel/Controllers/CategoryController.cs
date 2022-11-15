using FronttoBackFlowers.Areas.AdminPanel.Models;
using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
    public CategoryController(AppDbContext dbContext)
    {
            _dbContext = dbContext;
    }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null)
                return NotFound();

            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateModel category)
        {
            if (!ModelState.IsValid)
                return View();
            //return Json(errorList);

            var existName = await _dbContext.Categories.AnyAsync(x => x.Name.ToLower().Equals(category.Name.ToLower()));

            if (existName)
            {
                ModelState.AddModelError("name", "eyni ad tekrarlana bilmez");
                return View();
            }

            var categoryEntity = new Category
            {
                Name = category.Name,
                Description = category.Description,
            };

            await  _dbContext.Categories.AddAsync(categoryEntity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return View(new CategoryUpdateModel
            {
                Name = category.Name,
                Description = category.Description
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,CategoryUpdateModel model)
        {
            if (id == null)
                return NotFound();

            if(!ModelState.IsValid)
            {
                return View();
            }
            var category = await _dbContext.Categories.FindAsync(id);


            if (category == null)
                return NotFound();

            var isExistName = await _dbContext.Categories.AnyAsync(c => c.Name.ToLower() == model.Name.ToLower() && c.Id == id);
            
            if(isExistName)
            {
                ModelState.AddModelError("Name", "Ad tekrarlana Bilmez");
                return View(new CategoryUpdateModel { Name = category.Name, Description = category.Description });
            }
            category.Name = model.Name;
            category.Description = model.Description;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) NotFound();


            var category = await _dbContext.Categories.FindAsync(id);

            if(category == null) return NotFound();

            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
