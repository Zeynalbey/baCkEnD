namespace Backend_Final.Areas.Client.ViewModels.Blog
{
    public class BlogListViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? BlogFiles { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        public List<CategoryViewModeL>? Categories { get; set; }

        public BlogListViewModel(int id, string title, string content, string blogFiles, bool isImage, bool isVideo,
            List<TagViewModel> tags, List<CategoryViewModeL> categories)
        {
            Id = id;
            Title = title;
            Content = content;
            BlogFiles = blogFiles;
            IsImage = isImage;
            IsVideo = isVideo;
            Tags = tags;
            Categories = categories;
        }

        public class TagViewModel
        {
            public TagViewModel(string title)
            {
                Title = title;
            }

            public string Title { get; set; }
        }
        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }


        }
    }
}
