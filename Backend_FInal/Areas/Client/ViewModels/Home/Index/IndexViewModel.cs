using Backend_Final.Areas.Admin.ViewModels.Payment;
using Backend_Final.Areas.Admin.ViewModels.Product;
using Backend_Final.Areas.Admin.ViewModels.Slider;


namespace Backend_Final.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<ListViewModel> Sliders { get; set; }
        public List<PaymentListViewModel> Payments { get; set; }

        public List<Admin.ViewModels.Product.ListItemViewModel> Products { get; set; }




        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public IndexViewModel(int id,string title, string description,
            decimal price, string ımgUrl, List<ColorViewModeL> colors, 
            List<SizeViewModeL> sizes, List<ListViewModel> sliders = null, 
            List<PaymentListViewModel> payments = null, List<ListItemViewModel> products = null)
        {
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = ımgUrl;
            Colors = colors;
            Sizes = sizes;
            Sliders = sliders;
            Payments = payments;
            Products = products;
            Id = id;
        }


        public class SizeViewModeL
        {
            public SizeViewModeL(string title, int id)
            {
                Title = title;
                Id = id;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class ColorViewModeL
        {
            public ColorViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
