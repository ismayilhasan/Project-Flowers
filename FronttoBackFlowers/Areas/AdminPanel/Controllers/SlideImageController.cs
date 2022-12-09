using FronttoBackFlowers.Areas.AdminPanel.Models;
using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Data;
using FronttoBackFlowers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    
    public class SlideImageController : BaseController
    {
      
        private readonly AppDbContext _dbContext;

        public SlideImageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var slideImages = await _dbContext.SliderImages.ToListAsync();

            return View(slideImages);
        }
        public async Task<IActionResult> Create()
        {
            var slideImages = await _dbContext.SliderImages.ToListAsync();
                
            var SliderImage = new SlideImageCreateViewModel {
                SliderImages = slideImages
            };

            return View(SliderImage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideImageCreateViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View();

            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Sekil secilmelidir");
                return View();
            }

            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("Image", "Sekilin hecmi max 10mb ola biler");
                return View();
            }

            var unicalFileName = await model.Image.GenerateFile(Constants.RootPath);

            await _dbContext.SliderImages.AddAsync(new SliderImage
            {
                Name = unicalFileName,
            });

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var slideImage = await _dbContext.SliderImages.FindAsync(id);

            return View(new SlideImageUpdateViewModel
            {
                ImageUrl = slideImage.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SlideImageUpdateViewModel model)
        {
            if (id == null) return NotFound();

            var slideImage = await _dbContext.SliderImages.FindAsync(id);

            if (slideImage == null) return NotFound();

            if (slideImage.Id != id) BadRequest();

            if (!ModelState.IsValid)
            {
                return View(new SlideImageUpdateViewModel
                {
                    ImageUrl = slideImage.Name
                });
            }

            if (!model.Image.IsImage())
            {
                ModelState.AddModelError("Image", "Sekil secilmelidir");

                return View(new SlideImageUpdateViewModel
                {
                    ImageUrl = slideImage.Name
                });
            }

            if (!model.Image.IsAllowedSize(10))
            {
                ModelState.AddModelError("Image", "Sekilin hecmi max 10mb ola biler");

                return View(new SlideImageUpdateViewModel
                {
                    ImageUrl = slideImage.Name
                });
            }

            var path = Path.Combine(Constants.RootPath, "img", slideImage.Name);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            var unicalFileName = await model.Image.GenerateFile(Constants.RootPath);

            slideImage.Name = unicalFileName;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var slideImage = await _dbContext.SliderImages.FindAsync(id);

            if (slideImage == null) return NotFound();

            if (slideImage.Id != id) BadRequest();

            var path = Path.Combine(Constants.RootPath, "img", slideImage.Name);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _dbContext.SliderImages.Remove(slideImage);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
