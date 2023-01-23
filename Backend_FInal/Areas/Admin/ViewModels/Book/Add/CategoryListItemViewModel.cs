namespace Backend_Final.Areas.Admin.ViewModels.Product.Add
{
    public class CategoryListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public CategoryListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
