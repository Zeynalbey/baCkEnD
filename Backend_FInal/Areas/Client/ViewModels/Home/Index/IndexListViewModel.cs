using Backend_Final.Database.Models;


namespace Backend_Final.Areas.Client.ViewModels.Home.Index
{
    public class IndexListViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Database.Models.Product> Products { get; set; }
    }
}
