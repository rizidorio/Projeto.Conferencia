using Domain.Entity;
using Infra.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class ConferenciaContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }

        public ConferenciaContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutcMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
            base.OnModelCreating(modelBuilder);
        }
    }
}
