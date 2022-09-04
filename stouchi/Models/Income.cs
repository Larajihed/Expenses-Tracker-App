using System.ComponentModel.DataAnnotations.Schema;

namespace stouchi.Models
{
    public class Income
    {
        public int IncomeId { get; set; }
        public float Value { get; set; }
        public int BucketId { get; set; }
        [NotMapped]
        public Bucket Bucket { get; set; }

    }
}
