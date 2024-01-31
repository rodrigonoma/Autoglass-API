using Autoglass.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autoglass.Infrastructure.Data
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options)
            : base(options) { }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("Produtos");
        }
    }
}
