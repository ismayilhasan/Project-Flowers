using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    public class BaseController : Controller
    {
        [Area("AdminPanel")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
