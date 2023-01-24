namespace Backend_Final.ViewModels.Admin.SubNavbar
{
    public class ListSubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string Navbar { get; set; }

        public ListSubViewModel(int id, string title, string url, int order, string navbar)
        {
            Id = id;
            Title = title;
            Url = url;
            Order = order;
            Navbar = navbar;
        }
    }
}
