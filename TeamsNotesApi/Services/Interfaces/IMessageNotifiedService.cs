using TeamsNotesApi.Models.Notification.Expo;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface IMessageNotifiedService
    {
        public PushTicketRequest MessageToSend(string body, string title, string token);
    }
}
