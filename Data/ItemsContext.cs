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

        public DbSet<Item> Items { get; set; }

        
    }
}
