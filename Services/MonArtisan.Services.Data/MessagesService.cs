namespace MonArtisan.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MonArtisan.Data;
    using MonArtisan.Data.Models;
    using MonArtisan.Web.ViewModels.Chat;

    public class MessagesService : IMessagesService
    {
        private readonly ApplicationDbContext db;

        public MessagesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Create(string message)
        {
            var newMessage = new Message
            {
                MessageContent = message,
            };

            await this.db.Messages.AddAsync(newMessage);
            await this.db.SaveChangesAsync();
            return newMessage.Id;
        }

        public List<MessageViewModel> GetMessages(string clientId, int clientRoomId, string professionalId, int professionalRoomId)
        {
            var messages = this.db.MessageRooms
                .Where(x => ((x.ReciverId == clientId && x.SenderId == professionalId) && x.RoomId == clientRoomId)
                || (x.SenderId == clientId && x.ReciverId == professionalId && x.RoomId == professionalRoomId))
                .Select(x => new MessageViewModel
                {
                    Reciver = x.Reciver.UserName,
                    Sender = x.Sender.UserName,
                    MessageContent = x.Message.MessageContent,
                })
                .ToList();
            return messages;
        }
    }
}
