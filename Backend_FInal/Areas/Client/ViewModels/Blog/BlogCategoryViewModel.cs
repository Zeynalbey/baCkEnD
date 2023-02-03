namespace Backend_Final.Areas.Admin.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }

        public BlogCategoryViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
