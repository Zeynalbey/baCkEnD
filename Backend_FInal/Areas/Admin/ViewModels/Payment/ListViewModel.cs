namespace Backend_Final.Areas.Admin.ViewModels.Payment
{
    public class ListViewModel
    {
        public ListViewModel(int id,
            string title,
            string content,
            string iconImage)
        {
            Id = id;
            Title = title;
            Content = content;
            IconImage = iconImage;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string IconImage { get; set; }
    }
}
