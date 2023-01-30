using Backend_Final.Database.Models;

namespace Backend_Final.Areas.Admin.ViewModels.Product.Add
{
    public class ProductListViewModel
    {
        public ProductListViewModel(int? id, string? name, string description, decimal price, List<Database.Models.ProductImage> images = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Images = images;
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Database.Models.ProductImage> Images { get; set; }


    }
}
