namespace MonArtisan.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using MonArtisan.Web.Models;

    public class ChatHub : Hub
    {
        public async Task Send(string message, string userId)
        {
            await this.Clients.All.SendAsync("NewMessage", new ChatMessage { Message = message, UserId = userId });
        }
    }
}
