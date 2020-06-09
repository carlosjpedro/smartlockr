using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Smartlockr.SignalRService
{
    public class DomainUpdateHub : Hub
    {
        public Task SendMessage(string client, string message)
        {
            Console.WriteLine(client + " " + message);
            return Clients.All.SendCoreAsync("update", new[] { message });
        }
    }
}
