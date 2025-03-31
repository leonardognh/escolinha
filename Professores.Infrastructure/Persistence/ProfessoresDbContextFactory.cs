using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Professores.Infrastructure.Persistence;

public class ProfessoresDbContextFactory : IDesignTimeDbContextFactory<ProfessoresDbContext>
{
    public ProfessoresDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Professores.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ProfessoresDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A ConnectionString nao foi encontrada.");
        }

        optionsBuilder.UseNpgsql(connectionString);

        return new ProfessoresDbContext(optionsBuilder.Options);
    }
}
