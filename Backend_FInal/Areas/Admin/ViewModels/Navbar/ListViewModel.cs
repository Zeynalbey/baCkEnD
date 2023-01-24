using Backend_Final.Database.Models;

namespace Backend_Final.ViewModels.Admin.Navbar
{
    public class ListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public bool IsBold { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }

        public ListViewModel(int id,string title, int order, bool isBold, bool isHeader, bool isFooter)
        {
            Title = title;
            Order = order;
            IsBold = isBold;
            IsHeader = isHeader;
            IsFooter = isFooter;
            Id = id;
        }
    }
}
