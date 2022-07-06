using Newtonsoft.Json;

namespace TeamsNotesApi.Models.Notification.Firebase
{
    public class Notification
    {
        [JsonProperty("deviceId")]
        public string? DeviceId { get; set; }
        [JsonProperty("isAndroiodDevice")]
        public bool IsAndroiodDevice { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("body")]
        public string? Body { get; set; }
    }
    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("title")]
            public string? Title { get; set; }
            [JsonProperty("body")]
            public string? Body { get; set; }
        }
        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";
        [JsonProperty("data")]
        public DataPayload? Data { get; set; }
        [JsonProperty("notification")]
        public DataPayload? Notification { get; set; }
    }
}
