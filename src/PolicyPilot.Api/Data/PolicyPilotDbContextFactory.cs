using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PolicyPilot.Api.Data;

public class PolicyPilotDbContextFactory : IDesignTimeDbContextFactory<PolicyPilotDbContext>
{
    public PolicyPilotDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var options = new DbContextOptionsBuilder<PolicyPilotDbContext>()
            .UseSqlServer(configuration.GetConnectionString("PolicyPilot"))
            .Options;

        return new PolicyPilotDbContext(options);
    }
}
