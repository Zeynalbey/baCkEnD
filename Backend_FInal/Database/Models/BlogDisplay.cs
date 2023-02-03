using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class BlogDisplay : BaseEntity<int>
    {
        public string? FileName { get; set; }
        public string? FileNameInSystem { get; set; }
        public bool IsImage { get; set; }
        public bool IsVidio { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
