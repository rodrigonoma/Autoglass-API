using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Autoglass.Infrastructure.Data
{
    public class MeuDbContextFactory : IDesignTimeDbContextFactory<MeuDbContext>
    {
        public MeuDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeuDbContext>();
            
            var path = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(path, "appsettings.json"), optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new MeuDbContext(optionsBuilder.Options);
        }
    }
}
