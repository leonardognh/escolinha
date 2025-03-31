using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Alunos.Infrastructure.Persistence;

public class AlunosDbContextFactory : IDesignTimeDbContextFactory<AlunosDbContext>
{
    public AlunosDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Alunos.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AlunosDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("A ConnectionString nao foi encontrada.");
        }

        optionsBuilder.UseNpgsql(connectionString);

        return new AlunosDbContext(optionsBuilder.Options);
    }
}
