using Backend_Final.Areas.Admin.ViewModels.Category;
using Backend_Final.Areas.Admin.ViewModels.Size;
using Backend_Final.Areas.Admin.ViewModels.Tag;

namespace Backend_Final.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string description, 
            int price, int rate, List<CategoryViewModel> categories, 
            List<TagViewModel> tags, List<SizeViewModel> sizes, 
            List<ColorViewModel> colors)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Rate = rate;
            Categories = categories;
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
        }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
        public List<ColorViewModel> Colors { get; set; }

    }
}
