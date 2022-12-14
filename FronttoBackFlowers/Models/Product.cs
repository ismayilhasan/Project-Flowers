namespace FronttoBackFlowers.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        public int Price { get; set; }
        public Category? Category { get; set; }

        public int Discount { get; set; }
        public int CategoryId { get; set; }

    }
}
