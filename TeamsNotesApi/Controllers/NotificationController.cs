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
        ISaveTokenUserService _userService;
        public NotificationController(ISaveTokenUserService userService)
        {
            oR = new();
            _userService = userService;
        }

        
        [HttpPost]
        [Route("SendNotification")]
        [Authorize]
        public async Task<Reply> SaveToken([FromBody] PushTokenUser tokenUser)
        {
            var a = HttpContext.Response.Headers;
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

    }
}
