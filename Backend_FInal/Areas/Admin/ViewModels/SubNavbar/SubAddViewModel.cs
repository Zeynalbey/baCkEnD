namespace Backend_Final.ViewModels.Admin.SubNavbar
{
    public class SubAddViewModel  
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url{ get; set; }
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public List<NavbarListItemViewModel>? Navbar { get; set; }
    }
}
