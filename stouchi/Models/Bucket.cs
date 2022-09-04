using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stouchi.Models
{
    public class Bucket
    {
        [Key]
        public int BucketId { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }

        public int UserId { get; set; }
        [NotMapped]
        public User User { get; set; }
    }
}
