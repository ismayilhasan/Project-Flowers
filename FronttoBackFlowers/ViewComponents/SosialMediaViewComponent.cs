using FronttoBackFlowers.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FronttoBackFlowers.ViewComponents
{
    public class SosialMediaViewComponent : ViewComponent
    {
        private readonly AppDbContext _Dbcontext;

        public SosialMediaViewComponent(AppDbContext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        public IViewComponentResult Invoke()
        {
            var sosialMedias = _Dbcontext.SosialMedias.ToList();
            return View(sosialMedias);
        }
    }
}
