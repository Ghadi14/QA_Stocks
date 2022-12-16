using backend_iss.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_iss.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
              .HasMany(u => u.OwnedStocks)
              .WithOne(x => x.Owner).
               HasForeignKey(s=>s.OwnerId)
              .IsRequired();

            modelBuilder.Entity<User>()
           .HasMany(u => u.MemberOfStocks)
           .WithMany(s => s.Members)
           ;

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
