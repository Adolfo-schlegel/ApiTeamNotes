using Microsoft.AspNetCore.Mvc;
using TeamsNotesApi.Models;
using TeamsNotesApi.Services.Interfaces;
using TeamsNotesApi.Models.UserLogin;
using Microsoft.AspNetCore.Authorization;

namespace TeamsNotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private Reply? oR;
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("LoginUser")]
        [AllowAnonymous]
        public Reply? Login([FromBody] User user)
        {
            try
            {
                oR = _loginService.ValidateUser(user);
            }
            catch(Exception ex)
            {
                oR.message = "Error en el servidor";
                return oR;
            }
           
            return oR;
        }
    }
}
