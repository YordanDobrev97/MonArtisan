namespace MonArtisan.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MonArtisan.Data;
    using MonArtisan.Data.Models;

    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext db;

        public RoomsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddMessageRoom(int messageId, int roomId, string reciverId, string senderId)
        {
            if (this.db.MessageRooms.Any(
                x => x.MessageId == messageId && x.RoomId == roomId
                && (x.ReciverId == reciverId || x.SenderId == senderId)))
            {
                return;
            }

            await this.db.MessageRooms.AddAsync(new MessageRoom
            {
                MessageId = messageId,
                RoomId = roomId,
                ReciverId = reciverId,
                SenderId = senderId,
            });

            await this.db.SaveChangesAsync();
        }

        public async Task<Room> Create(string name)
        {
            if (this.db.Rooms.Any(x => x.Name == name))
            {
                return null;
            }

            var room = new Room
            {
                Name = name,
            };

            await this.db.Rooms.AddAsync(room);
            await this.db.SaveChangesAsync();

            return room;
        }

        public int GetRoom(string userId)
        {
            int roomId = this.db.Rooms.Where(x => x.Name.Contains(userId))
                .Select(x => x.Id)
                .FirstOrDefault();

            return roomId;
        }

        public Room IsExistRoom(string name)
        {
            return this.db.Rooms.FirstOrDefault(x => x.Name.Contains(name));
        }

        public bool IsExistUser(string userId)
        {
            return this.db.Rooms.Any(x => x.Name.Contains(userId));
        }
    }
}
