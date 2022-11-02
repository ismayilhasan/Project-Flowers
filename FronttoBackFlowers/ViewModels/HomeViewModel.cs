﻿using FronttoBackFlowers.Models;

namespace FronttoBackFlowers.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; } = new List<SliderImage>();
        public Slider Slider { get; set; } = new Slider();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
