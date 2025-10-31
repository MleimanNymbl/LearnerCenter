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
            optionsBuilder.UseNpgsql("Host=db.xdjtrfezlpelyebuxsji.supabase.co;Database=postgres;Username=postgres;Password=Password123!;Port=5432;SSL Mode=Require");
        }
        else
        {
            // Default to SQL Server for local development
            optionsBuilder.UseSqlServer("Server=MATTHEWSPC;Database=LearnerCenterDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
        
        return new LearnerCenterDbContext(optionsBuilder.Options);
    }
}