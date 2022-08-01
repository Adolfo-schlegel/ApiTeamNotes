using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamsNotesApi.Models;
using TeamsNotesApi.Models.Notification;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        Reply oR;
        IStatusUserNotificationService _userService;
        public NotificationController(IStatusUserNotificationService userService)
        {
            oR = new();
            _userService = userService;
        }

        
        [HttpPost]
        [Route("SendNotification")]
        [Authorize]
        public async Task<Reply> SaveToken([FromBody] PushTokenUser tokenUser)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                int id_user = int.Parse(identity.Claims.First().Value);

                oR.message = _userService.InsertTokenUsers(id_user, tokenUser.token);

                if(oR.message == "OK")
                {
                    oR.result = 1;
                }

                return oR;
            }

            oR.message = "Error al insertar token del usuario";
            return oR;
        }

        [HttpPut]
        [Route("EnableNotification")]
        [Authorize]
        public Reply EnableNotification([FromQuery]int status)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                int id_user = int.Parse(identity.Claims.First().Value);

                oR.message = _userService.ChangeStatusUser(id_user,status);

                if(oR.message == "OK")
                {
                    oR.result = 1;
                    return oR;
                }                                                   
            }
            return oR;
        }

    }
}
