namespace Backend_Final.Areas.Admin.ViewModels.Slider
{
    public class ListViewModel
    {
        public ListViewModel(int id,
            string mainTitle,
            string content,
            string secondTitle,
            string button,
            string buttonRedirectUrl,
            int order,
            string imageName,
            DateTime createdAt)
        {
            Id = id;
            MainTitle = mainTitle;
            Content = content;
            SecondTitle = secondTitle;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
            ImageName = imageName;
            CreatedAt = createdAt;
        }
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string SecondTitle { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
