using Expo.Server.Models;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<PushTicketResponse> PushSendAsync(PushTicketRequest pushTicketRequest);
        public Task<PushResceiptResponse> PushGetReceiptsAsync(PushReceiptRequest pushReceiptRequest);
        public  Task<U> PostAsync<T, U>(T requestObj, string path) where T : new();
    }
}
