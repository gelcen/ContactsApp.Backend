using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Models
{
    public class ContactsAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<DeletedContact> DeletedContacts { get; set; }

        public ContactsAppContext(DbContextOptions<ContactsAppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   FirstName = "Admin",
                   LastName = "User",
                   Username = "admin",
                   Password = "admin",
                   Role = Role.Admin
               },
                new User
                {
                    Id = 2,
                    FirstName = "Normal",
                    LastName = "User",
                    Username = "user",
                    Password = "user",
                    Role = Role.User
                }
            );
        }
    }
}
