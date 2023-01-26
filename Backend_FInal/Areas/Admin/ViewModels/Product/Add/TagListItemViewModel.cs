namespace Backend_Final.Areas.Admin.ViewModels.Product.Add
{
    public class TagListItemViewModel
    {
        public TagListItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
