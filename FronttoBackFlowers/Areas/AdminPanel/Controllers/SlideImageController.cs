using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class SlideImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
