using DDR_PROJECTAPIS.Models;
using Microsoft.EntityFrameworkCore;

namespace DDR_PROJECTAPIS.Data
{
    
    public class ItemsContext : DbContext
    {
        public ItemsContext(DbContextOptions<ItemsContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BorrowedItem>().HasKey(table => new {
                table.ItemId,
                table.StudentId
            });

            builder.Entity<Fine>().HasKey(table => new {
                table.ItemId,
                table.StudentId,
            });


        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<BorrowedItem> BorrowedItems { get; set; }

        public DbSet<Fine> Fines { get; set; }


    }
}
