using TeamsNotesApi.Models.Notification.Expo;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class MessageNotifiedService : IMessageNotifiedService
    {
        public PushTicketRequest MessageToSend(string body, string title, string token)
        {
            var pushTicketReq = new PushTicketRequest
            {
                PushTo = new List<string>() { token },
                PushTitle = title,
                PushBody = body + "🙄",
                PushSound = "default"
            };

            return pushTicketReq;
        }
    }
}
