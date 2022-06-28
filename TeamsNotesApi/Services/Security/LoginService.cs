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
        
        private SqlConnection? sqlConnection;
        private SqlDataReader? reader;
        private SqlCommand? sqlCommnad;
        private string? pstrquery = "";
        private Reply? oR;

        private IJwtAuth _jwtAuth;
        private IEncrypt _encrypt;
        public LoginService(IJwtAuth jwtAuth, IEncrypt encrypt)
        {
            oR = new Reply();
            sqlConnection = CVM_Connection.Connect();

            _jwtAuth = jwtAuth;            
            _encrypt = encrypt;
        }

        public Reply? ValidateUser(User user)
        {
            pstrquery = "Select ID_USUARIO,CD_USUARIO,DS_PASSWORD from vs_usuarios_seg1 WHERE CD_USUARIO = '" + user.user + "' and DS_PASSWORD = '" + _encrypt.encrypted(user.password) + "';";

            try
            {
                sqlCommnad = new SqlCommand(pstrquery, sqlConnection);

                reader = sqlCommnad.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.id = reader.GetInt32(0);
                    }

                    oR.result = 1;
                    oR.data = _jwtAuth.GetNewToken(user);
                    oR.message = "OK";
                }
                else
                {
                    oR.message = "Usuario no encontrado";
                }
            }
            catch (Exception ex)
            {
                oR.message = ex.ToString();
                return oR;
            }

            return oR;
        }


    }
}
