using Backend_Final.Database.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Database.Models
{
    public class Contact : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
