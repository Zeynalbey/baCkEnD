namespace Backend_Final.Areas.Admin.ViewModels.Blog
{
    public class BlogTagViewModel
    {
      

        public int Id { get; set; }
        public string Title { get; set; }

        public BlogTagViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
