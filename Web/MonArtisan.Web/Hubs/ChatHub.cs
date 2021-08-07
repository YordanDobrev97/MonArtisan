namespace MonArtisan.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.Models;

    public class ChatHub : Hub
    {
        private readonly IRoomsService roomsService;
        private readonly IMessagesService messagesService;

        public ChatHub(IRoomsService roomsService, IMessagesService messagesService)
        {
            this.roomsService = roomsService;
            this.messagesService = messagesService;
        }

        public async Task Send(string message, string clientId, string professionalId)
        {
            var senderId = this.Context.UserIdentifier;

            var room = this.roomsService.IsExistRoom($"{clientId}{professionalId}");

            if (room == null)
            {
                room = await this.roomsService.Create($"{clientId}{professionalId}");
            }

            int messageId = await this.messagesService.Create(message);
            await this.roomsService.AddMessageRoom(messageId, room.Id, clientId, professionalId);

            await this.Groups.AddToGroupAsync(
               this.Context.ConnectionId,
               $"{clientId + professionalId}{messageId}");

            await this.Clients.All.SendAsync("NewMessage", new ChatMessage { Message = message });
        }
    }
}
