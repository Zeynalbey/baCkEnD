﻿namespace Backend_Final.Areas.Admin.ViewModels.Slider
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }
        public string Button { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public ListViewModel(int id, string mainTitle, string content, string imageName, string button, string buttonRedirectUrl, int order, DateTime createdAt)
        {
            Id = id;
            MainTitle = mainTitle;
            Content = content;
            ImageName = imageName;
            Button = button;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
            CreatedAt = createdAt;
        }
    }
}
