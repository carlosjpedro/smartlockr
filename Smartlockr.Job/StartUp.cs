using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smartlockr.DnsQuerier;
using Smartlockr.Persistence;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Smartlockr.Job.StartUp))]
namespace Smartlockr.Job
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var options = new DbContextOptionsBuilder().UseSqlServer(config.GetConnectionString("domain-database")).Options;
            builder.Services.AddHttpClient();
            builder.Services.AddLogging();
            builder.Services.AddSignalR();

            builder.Services.AddTransient((context) => new DomainDataContext(options));

            builder.Services.AddTransient<INTA7516InfoManager, NTA7516InfoManager>();
            builder.Services.AddTransient<IDomainFromEmailParser, DomainFromEmailParser>();
            builder.Services.AddTransient<IRecordFetcher, RecordFetcher>();
            builder.Services.AddTransient<INNTA7516InfoExtractor, NTA7516InfoExtractor>();
            builder.Services.AddTransient<INTA7516InfoRepository, NTA7516InfoRepository>();
        }
    }
}
