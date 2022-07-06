using Newtonsoft.Json;
using System.Net.Http.Headers;
using TeamsNotesApi.Models.Notification.Expo;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class NotificationExpoService: INotificationExpoService
    {
        //Environemt Configuration
        private const string _expoBackendHost = "https://exp.host";
        private const string _pushSendPath = "/--/api/v2/push/send";
        private const string _pushGetReceiptsPath = "/--/api/v2/push/getReceipts";

        //Make this static to avoid socket saturation and limit concurrent server connections to 6, but only for instances of this class.
        private static readonly HttpClientHandler _httpHandler = new HttpClientHandler() { MaxConnectionsPerServer = 6 };
        private static readonly HttpClient _httpClient = new HttpClient(_httpHandler);

        public string AccessToken{ set {_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value); }}
        
        static NotificationExpoService()
        {
            _httpClient.BaseAddress = new Uri(_expoBackendHost);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "AAAABdFjYq4:APA91bFFXJbdDwaq6f0OPGKa4vBmaHWMPFnxik4Higx4tVi8GVRWWWkUQOmYx2N_pqh3m9W8fYuqGLVbIhzlEkyyHg8fghUZGdvGj6Vp4Ba8vLCDVdwxxeBilU_LvwkYYbI8Ub9ONAsb");
        }

        public async Task<PushTicketResponse> PushSendAsync(PushTicketRequest pushTicketRequest)
        {
            //AccessToken = "AAAABdFjYq4:APA91bFFXJbdDwaq6f0OPGKa4vBmaHWMPFnxik4Higx4tVi8GVRWWWkUQOmYx2N_pqh3m9W8fYuqGLVbIhzlEkyyHg8fghUZGdvGj6Vp4Ba8vLCDVdwxxeBilU_LvwkYYbI8Ub9ONAsb";
            var ticketResponse = await PostAsync<PushTicketRequest, PushTicketResponse>(pushTicketRequest, _pushSendPath);
            return ticketResponse;
        }

        public async Task<PushResceiptResponse> PushGetReceiptsAsync(PushReceiptRequest pushReceiptRequest)
        {
            var receiptResponse = await PostAsync<PushReceiptRequest, PushResceiptResponse>(pushReceiptRequest, _pushGetReceiptsPath);
            return receiptResponse;
        }

        public async Task<U> PostAsync<T, U>(T requestObj, string path) where T : new()
        {

            var serializedRequestObj = JsonConvert.SerializeObject(requestObj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var requestBody = new StringContent(serializedRequestObj, System.Text.Encoding.UTF8, "application/json");
            var responseBody = default(U);
            var response = await _httpClient.PostAsync(path, requestBody);

            if (response.IsSuccessStatusCode)
            {
                var rawResponseBody = await response.Content.ReadAsStringAsync();
                responseBody = JsonConvert.DeserializeObject<U>(rawResponseBody);
            }

            return responseBody;
        }

        
    }
}
