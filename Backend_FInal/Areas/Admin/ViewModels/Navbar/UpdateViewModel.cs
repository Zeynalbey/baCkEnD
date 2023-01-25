namespace Backend_Final.Areas.Admin.ViewModels.Navbar
{
    public class UpdateViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public UpdateViewModel(int id, string title, string url, int order, bool isBold)
        {
            Id = id;
            Title = title;
            Url = url;
            Order = order;
            IsBold = isBold;
        }

        public UpdateViewModel()
        {
        }
    }
}
