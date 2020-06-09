using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Smartlockr.DnsQuerier;

[assembly: FunctionsStartup(typeof(Smartlockr.Job.StartUp))]
namespace Smartlockr.Job
{
    public class TimerTrigger
    {
        private readonly INTA7516InfoManager _manager;

        public TimerTrigger(INTA7516InfoManager manager)
        {
            _manager = manager;
        }

        [FunctionName("UpdateRecords")]
        public async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            await _manager.UpdateRecords(DateTime.UtcNow.AddSeconds(-1));
               

            var connection = new HubConnectionBuilder()
                  .WithUrl("https://localhost:44317/hub")
                  .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("SendMessage", "Updated Records");
        }
    }
}
