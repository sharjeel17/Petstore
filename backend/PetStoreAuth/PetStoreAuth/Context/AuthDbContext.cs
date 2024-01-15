using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetStoreAuth.Models;

namespace PetStoreAuth.Context
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> UsersInfo { get; set; }
    }
}
