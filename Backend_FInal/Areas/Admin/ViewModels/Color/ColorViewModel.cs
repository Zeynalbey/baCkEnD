namespace Backend_Final.Areas.Admin.ViewModels.Color
{
    public class ColorViewModel
    {
        public ColorViewModel(int id, string title)
        {
            Id = id;
            Title = title;            
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
