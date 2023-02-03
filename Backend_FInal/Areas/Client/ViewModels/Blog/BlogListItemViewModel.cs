namespace Backend_Final.Areas.Client.ViewModels.Blog
{
    public class BlogListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }



        public BlogListItemViewModel(int id, string title, string content,
             List<TagViewModel> tags, List<CategoryViewModeL> categories)
        {
            Id = id;
            Title = title;
            Content = content;
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
            public CategoryViewModeL(string title)
            {
                Title = title;

            }

            public string Title { get; set; }

        }
    }
}
