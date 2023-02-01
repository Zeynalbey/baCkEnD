using Backend_Final.Areas.Admin.ViewModels.Category;
using Backend_Final.Areas.Admin.ViewModels.Color;
using Backend_Final.Areas.Admin.ViewModels.Size;
using Backend_Final.Areas.Admin.ViewModels.Tag;

namespace Backend_Final.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string description, 
            decimal price, List<CategoryViewModel> categories, 
            List<TagViewModel> tags, List<SizeViewModel> sizes, 
            List<ColorViewModel> colors)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Categories = categories;
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
        public List<ColorViewModel> Colors { get; set; }

    }
}
