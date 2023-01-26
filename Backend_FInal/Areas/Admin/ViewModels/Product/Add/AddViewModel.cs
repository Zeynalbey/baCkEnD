using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Admin.ViewModels.Product.Add
{
    public class AddViewModel 
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? SizeIds { get; set; }
        public List<int>? ColorIds { get; set; }
        public List<int>? TagIds { get; set; }

        public List<CategoryListItemViewModel>? Categories { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
