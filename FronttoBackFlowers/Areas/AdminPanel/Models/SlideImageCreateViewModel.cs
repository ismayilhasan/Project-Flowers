using FronttoBackFlowers.Models;

namespace FronttoBackFlowers.Areas.AdminPanel.Models
{
    public class SlideImageCreateViewModel
    {
        public IFormFile Image { get; set; }

        public List<SliderImage> SliderImages { get; set; }
    }
}
