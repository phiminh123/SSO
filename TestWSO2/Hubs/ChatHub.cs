using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NewProject.Hubs
{
    public class ChatHubs : Hub
    {
        public async Task SendMessage(int receiver)
        {
            await Clients.All.SendAsync("ReceiveMessage", receiver);
        }
    }
}
