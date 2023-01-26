namespace Backend_Final.Areas.Admin.ViewModels.Tag
{
    public class TagViewModel
    {
        public TagViewModel(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
    }
}
