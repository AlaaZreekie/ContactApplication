using ContactAppBackend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactAppBackend.DB.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions op) : base(op)
        {            
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}