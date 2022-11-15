using System.ComponentModel.DataAnnotations;

namespace FronttoBackFlowers.Areas.AdminPanel.Models
{
    public class CategoryCreateModel
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }

    }
}
