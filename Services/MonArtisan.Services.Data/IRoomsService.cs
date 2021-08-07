namespace MonArtisan.Services.Data
{
    using System.Threading.Tasks;

    using MonArtisan.Data.Models;

    public interface IRoomsService
    {
        Task<Room> Create(string name);

        Task AddMessageRoom(int messageId, int roomId, string reciverId, string senderId);

        Room IsExistRoom(string name);

        bool IsExistUser(string userId);

        int GetRoom(string userId);
    }
}
