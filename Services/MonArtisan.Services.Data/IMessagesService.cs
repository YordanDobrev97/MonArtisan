namespace MonArtisan.Services.Data
{
    using MonArtisan.Web.ViewModels.Chat;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task<int> Create(string message);

        List<MessageViewModel> GetMessages(string clientId, int clientRoomId, string professionalId, int professionalRoomId);
    }
}
