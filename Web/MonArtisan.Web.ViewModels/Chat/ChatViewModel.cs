namespace MonArtisan.Web.ViewModels.Chat
{
    using System.Collections.Generic;

    using MonArtisan.Web.ViewModels.Users;

    public class ChatViewModel
    {
        public string UserId { get; set; }

        public ICollection<string> Messages { get; set; }

        public List<ChatUserViewModel> Users { get; set; }
    }
}
