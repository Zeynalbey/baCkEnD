namespace Backend_Final.Areas.Client.ViewModels.ShopCart
{
    public class ListViewModel
    {
        public List<ItemViewModel>? Products { get; set; }
        public SummaryViewModel? Summary { get; set; }


        public class ItemViewModel
        {
            public int Id { get; set; }
            public string? ImageUrl { get; set; }
            public string? ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total { get; set; }
        }

        public class SummaryViewModel
        {
            public decimal Total { get; set; }
        }
    }
}
