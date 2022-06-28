using Microsoft.AspNetCore.Mvc;
using TeamsNotesApi.Models;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        Reply oR;
        INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            oR = new();
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("SendNotification")]
        public async Task<Reply> SendMessage([FromQuery] string message)
        {
            //var pushTicketReq = new Expo.Server.Models.PushTicketRequest()
            //{
            //    PushTo = new List<string>() { "ExponentPushToken[WUax4OF8mdlthYKxh5fY23]" },
            //    PushBadgeCount = 7,
            //    PushBody = message
            //};

            var pushTicketReq = new Expo.Server.Models.PushTicketRequest()
            {
               PushTo = new List<string>() { "ExponentPushToken[WUax4OF8mdlthYKxh5fY23]" },
               PushTitle = "Nuevas notas CVM",
               PushBody = message + "🙄",
               PushSound = "default"
            };



            oR.data = await _notificationService.PushSendAsync(pushTicketReq);
            
            return oR;
        }

    }
}
