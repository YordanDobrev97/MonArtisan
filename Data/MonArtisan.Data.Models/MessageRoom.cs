namespace MonArtisan.Data.Models
{
    using System;

    using MonArtisan.Data.Common.Models;

    public class MessageRoom : IDeletableEntity
    {
        public int Id { get; set; }

        public int MessageId { get; set; }

        public Message Message { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public string ReciverId { get; set; }

        public ApplicationUser Reciver { get; set; }

        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
