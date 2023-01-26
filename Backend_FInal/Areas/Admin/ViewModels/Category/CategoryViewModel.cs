namespace Backend_Final.Areas.Admin.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
