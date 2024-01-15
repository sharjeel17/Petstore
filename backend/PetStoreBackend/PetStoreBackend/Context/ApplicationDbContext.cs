using Microsoft.EntityFrameworkCore;
using PetStoreBackend.Models;

namespace PetStoreBackend.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Breed> Breeds { get; set; }
    }
}
