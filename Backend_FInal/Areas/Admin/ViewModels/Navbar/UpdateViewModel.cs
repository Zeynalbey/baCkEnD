namespace Backend_Final.Areas.Admin.ViewModels.Navbar
{
    public class UpdateViewModel
    {
       

        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }

        public UpdateViewModel(int id, string title, string url, int order, bool isBold, bool isHeader, bool isFooter)
        {
            Id = id;
            Title = title;
            Url = url;
            Order = order;
            IsBold = isBold;
            IsHeader = isHeader;
            IsFooter = isFooter;
        }

        public UpdateViewModel()
        {
        }
    }
}
