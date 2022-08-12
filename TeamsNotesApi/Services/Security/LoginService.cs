using System.Data.SqlClient;
using TeamsNotesApi.Models;
using TeamsNotesApi.Models.UserLogin;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;
using TeamsNotesApi.Tools;
using TeamsNotesApi.Tools.Interface;

namespace TeamsNotesApi.Services.Security
{
    public class LoginService : ILoginService
    {        
        private Reply? oR;
        private IJwtAuth _jwtAuth;
        private IEncrypt _encrypt;
        public LoginService(IJwtAuth jwtAuth, IEncrypt encrypt)
        {
            oR = new Reply();
            _jwtAuth = jwtAuth;
            _encrypt = encrypt;
        }

        public Reply? ValidateUser(User user)
        {

            using (var db = new CVM_GPA_SEG_01Context())
            {
                try
                {
                    user = (from d in db.Usuarios
                            where d.CdUsuario == user.user && d.DsPassword == _encrypt.encrypted(user.password)
                            select new User
                            {
                                id = d.IdUsuario,
                                user = d.DsUsuario
                            }).First();

                    if (user.id > 0)
                    {
                        oR.result = 1;
                        oR.data = _jwtAuth.GetNewToken(user);
                        oR.message = "OK";
                    }
                    return oR;
                }
                catch (Exception ex)
                {
                    oR.message = ex.Message;
                    return oR;
                }
            };

        }

    }
}
