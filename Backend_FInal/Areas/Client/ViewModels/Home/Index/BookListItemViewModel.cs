namespace Backend_Final.Areas.Client.ViewModels.Home.Index
{
    public class ProductListItemViewModel
    {
        public ProductListItemViewModel(int id, string title,decimal price, string mainImageUrl, string hoverImageUrl)
        {
            Id = id;
            Title = title;
            Price = price;
            MainImageUrl = mainImageUrl;
            HoverImageUrl = hoverImageUrl;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
    }
}
