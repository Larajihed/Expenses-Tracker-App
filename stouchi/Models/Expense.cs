using System.ComponentModel.DataAnnotations.Schema;

namespace stouchi.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public float Value { get; set; }
        public Currency Currency { get; set; }

        public int UserId { get; set; }
        [NotMapped]
        public User User { get; set; }

    }
}
