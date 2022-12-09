using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class BaseController : Controller
    {
    
      
    }
}
