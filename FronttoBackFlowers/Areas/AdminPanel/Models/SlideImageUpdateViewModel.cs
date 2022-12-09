namespace FronttoBackFlowers.Areas.AdminPanel.Models
{
    public class SlideImageUpdateViewModel
    {
        public string ImageUrl { get; set; } = String.Empty;
        public IFormFile Image { get; set; }
    }
}

