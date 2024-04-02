using Microsoft.EntityFrameworkCore;
using Bulky.Models;
namespace Bulky.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id =1,
                    Name = "Machines",
                    DisplayOrder = 1
                },
                 new Category
                 {
                     Id = 2,
                     Name = "Laundries",
                     DisplayOrder = 2
                 },
                  new Category
                  {
                      Id = 3,
                      Name = "PC",
                      DisplayOrder = 3
                  }
                );
        }
    }
}
