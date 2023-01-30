namespace Backend_Final.Areas.Client.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(int id, string name, decimal price, string image)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image{ get; set; }

    }
}
