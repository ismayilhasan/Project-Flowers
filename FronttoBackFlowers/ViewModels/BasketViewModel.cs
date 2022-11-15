using FronttoBackFlowers.Models;

namespace FronttoBackFlowers.ViewModels
{
    public class BasketViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category  Category{ get; set; }
        public int Count { get; set; }
    }
}
