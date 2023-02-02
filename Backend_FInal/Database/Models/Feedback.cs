using Backend_Final.Database.Models.Common;

namespace Backend_Final.Database.Models
{
    public class Feedback :BaseEntity<int>, IAuditable
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
