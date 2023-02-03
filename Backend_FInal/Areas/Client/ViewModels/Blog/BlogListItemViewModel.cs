namespace Backend_Final.Areas.Client.ViewModels.Blog
{
    public class BlogListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }



        public BlogListItemViewModel(int id, string title, string content,
            DateTime createdAt, List<TagViewModel> tags, List<CategoryViewModeL> categories)
        {
            Id = id;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Tags = tags;
            Categories = categories;
        }


        //****************************************************************


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
