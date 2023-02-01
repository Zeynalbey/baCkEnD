namespace Backend_Final.Areas.Admin.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string title)
        {
            Id = id;
            Title = title;      
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
