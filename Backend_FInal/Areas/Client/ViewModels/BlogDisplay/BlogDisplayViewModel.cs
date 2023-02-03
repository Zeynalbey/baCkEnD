namespace Backend_Final.Areas.Admin.ViewModels.BlogDisplay
{
    public class BlogDisplayViewModel
    {

        public class ListItem
        {

            public int Id { get; set; }
            public string FileUrl { get; set; }
            public bool IsImage { get; set; }
            public bool IsVideo { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public int BlogId { get; set; }
        public List<ListItem> Files { get; set; }
    }
}
