using FronttoBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.Controllers
{
    public class ErrorPageController : Controller
    {


        public IActionResult ErrorAction(int code)
        {
            ErrorViewModel error = new ErrorViewModel() {


                StatusCode = HttpContext.Response.StatusCode,
                Title = HttpContext.Response.Headers.ToString()

               
            
            };



            return View(error);
        }
    }
}
