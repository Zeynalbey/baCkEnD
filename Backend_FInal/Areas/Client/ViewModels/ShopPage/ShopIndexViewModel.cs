namespace Backend_Final.Areas.Client.ViewModels.ShopPage
{
    public class ShopIndexViewModel
    {

        public List<CategoryListItemViewModel> Categories { get; set; }
        public List<ColorListItemViewModel> Colors { get; set; }

        public List<SizeListItemViewModel> Sizes { get; set; }

        public List<TagListItemViewModel> Tags { get; set; }


        public ShopIndexViewModel(List<CategoryListItemViewModel> categories, List<ColorListItemViewModel> colors,
            List<SizeListItemViewModel> sizes, List<TagListItemViewModel> tags)
        {
            Categories = categories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
        }
    }

    public class CategoryListItemViewModel
    {
        public CategoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class ColorListItemViewModel
    {
        public ColorListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class TagListItemViewModel
    {
        public TagListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class SizeListItemViewModel
    {
        public SizeListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
  
}

