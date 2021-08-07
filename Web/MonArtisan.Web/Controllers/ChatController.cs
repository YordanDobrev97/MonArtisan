namespace MonArtisan.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MonArtisan.Services.Data;
    using MonArtisan.Web.ViewModels.Chat;

    public class ChatController : BaseController
    {
        private readonly IRoomsService roomsService;
        private readonly IMessagesService messagesService;
        private readonly IUsersService usersService;

        public ChatController(IRoomsService roomsService, IMessagesService messagesService, IUsersService usersService)
        {
            this.roomsService = roomsService;
            this.messagesService = messagesService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var loggedUser = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = new ChatViewModel
            {
                Users = await this.usersService.GetChatUsers(loggedUser),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("api/Chat/StartChat")]
        [AutoValidateAntiforgeryToken]
        public IActionResult StartChat([FromBody] StartChatViewModel input)
        {
            var recipientRoom = this.roomsService.IsExistUser(input.ClientId);
            var senderRoom = this.roomsService.IsExistUser(input.ProfessionalId);

            if (recipientRoom && senderRoom)
            {
                int clientRoomId = this.roomsService.GetRoom(input.ClientId);
                int professionalRoomId = this.roomsService.GetRoom(input.ProfessionalId);

                var messages = this.messagesService.GetMessages(input.ClientId, clientRoomId, input.ProfessionalId, professionalRoomId);
                return new JsonResult(messages);
            }

            return new JsonResult("No have messages!");
        }
    }
}
