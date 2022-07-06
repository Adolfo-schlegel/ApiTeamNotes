using TeamsNotesApi.Models.Notification.Firebase;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface INotificationFirebaseService
    {
        Task<Response> SendNotification(Notification notificationModel);
    }
}
