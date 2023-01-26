namespace Backend_Final.Areas.Admin.ViewModels.Product.Add
{
    public class ColorListItemViewModel
    {
        public ColorListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
