namespace Backend_Final.Areas.Client.ViewModels.Home
{
    public class ClientFeedbackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public ClientFeedbackViewModel(int id, string name, string surName, string role, string content, string imageUrl)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Role = role;
            Content = content;
            ImageUrl = imageUrl;
        }
    }
}
