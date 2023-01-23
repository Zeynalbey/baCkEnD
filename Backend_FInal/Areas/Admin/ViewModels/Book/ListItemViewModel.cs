namespace Backend_Final.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }

        public DateTime CreatedAt { get; set; }

        public ListItemViewModel(int id, string title, decimal price,DateTime createdAt, List<CategoryViewModeL> categories)
        {
            Id = id;
            Title = title;
            Price = price;
            CreatedAt = createdAt;
            Categories = categories;
        }

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }

            public string Title { get; set; }
            public string ParentTitle { get; set; }


        }
    }
}
