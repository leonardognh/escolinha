using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Turmas.Infrastructure.Persistence;

public class TurmasDbContextFactory : IDesignTimeDbContextFactory<TurmasDbContext>
{
    public TurmasDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Turmas.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TurmasDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A ConnectionString nao foi encontrada.");
        }

        optionsBuilder.UseNpgsql(connectionString);

        return new TurmasDbContext(optionsBuilder.Options);
    }
}
