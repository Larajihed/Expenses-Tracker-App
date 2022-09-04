using Microsoft.EntityFrameworkCore;
using stouchi.Models;

namespace stouchi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<Expense> Expences { get; set; }
        public DbSet<Income> Incomes{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }


    }
}
