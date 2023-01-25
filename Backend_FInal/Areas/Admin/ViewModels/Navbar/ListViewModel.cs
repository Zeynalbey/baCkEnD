using Backend_Final.Database.Models;

namespace Backend_Final.Areas.Admin.ViewModels.Navbar
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public bool IsBold { get; set; }

        public ListViewModel(int id, string title, int order, string url, bool isBold)
        {
            Id = id;
            Title = title;
            Order = order;
            Url = url;
            IsBold = isBold;
        }
    }
}
