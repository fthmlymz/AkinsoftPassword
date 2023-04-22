using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Models;

namespace PasswordManager.API.Helpers
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<MyPassword> MyPasswords { get; set; }


    }
}
