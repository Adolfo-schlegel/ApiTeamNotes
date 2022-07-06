using Newtonsoft.Json;

namespace TeamsNotesApi.Models.Notification.Firebase
{
    public class Response
    {
       [JsonProperty("isSuccess")]
       public bool IsSuccess { get; set; }
       [JsonProperty("message")]
       public string? Message { get; set; }
    }
}
