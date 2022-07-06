using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamsNotesApi.Models.Notification.Expo
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PushReceiptRequest
    {

        [JsonProperty(PropertyName = "ids")]
        public List<string> PushTicketIds { get; set; }
    }
}
