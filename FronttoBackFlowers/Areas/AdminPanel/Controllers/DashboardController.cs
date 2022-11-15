using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
