using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using LearnerCenter.API.Data;

namespace LearnerCenter.API;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LearnerCenterDbContext>
{
    public LearnerCenterDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LearnerCenterDbContext>();
        
        // Use production PostgreSQL for migrations when specified
        if (args.Length > 0 && args[0] == "--use-postgresql")
        {
            optionsBuilder.UseNpgsql("Host=aws-1-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.hpqliofwxllnsoaqaolv;Password=Password123!;Sslmode=Require");
        }
        else
        {
            // Default to SQL Server for local development
            optionsBuilder.UseSqlServer("Server=MATTHEWSPC;Database=LearnerCenterDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
        
        return new LearnerCenterDbContext(optionsBuilder.Options);
    }
}