using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entities;
using UserAuthentication.Models;

namespace UserAuthentication.Data
{
    public class DataContext : DbContext
    {
      public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<ClientEntity> Users { get; set; }
    }

}
