namespace Backend_Final.Areas.Client.ViewModels.Shop
{
    public class ShopViewModel
    {
     

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<ImageViewModeL> Images { get; set; }
        public List<CatagoryViewModeL> Catagories { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<TagViewModeL> Tags { get; set; }


        public ShopViewModel(string name, string description, decimal price, List<ImageViewModeL> images, 
            List<CatagoryViewModeL> catagories, List<ColorViewModeL> colors, List<SizeViewModeL> sizes, List<TagViewModeL> tags)
        {
            Name = name;
            Description = description;
            Price = price;
            Images = images;
            Catagories = catagories;
            Colors = colors;
            Sizes = sizes;
            Tags = tags;
        }

        public class ImageViewModeL
        {
            public ImageViewModeL(string imageUrl)
            {
                ImageUrl = imageUrl;
            }
            public string ImageUrl { get; set; }
        }



        public class CatagoryViewModeL
        {
            public CatagoryViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class TagViewModeL
        {
            public TagViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }

            public int Id { get; set; }
            public string Name { get; set; }
        }



        public class SizeViewModeL
        {
            public SizeViewModeL(string name, int id)
            {
                Name = name;
                Id = id;
            }

            public int Id { get; set; }
            public string Name { get; set; }
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
