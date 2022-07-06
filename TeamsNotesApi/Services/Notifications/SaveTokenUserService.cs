using System.Data.SqlClient;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class SaveTokenUserService : ISaveTokenUserService
    {
        private SqlConnection? conn;
        private SqlCommand? cmd;
        private string? query = "";

        public SaveTokenUserService()
        {
            conn = CVM_Connection.Connect();
        }

        public string InsertTokenUsers(int id_user, string token)
        {
            query = "insert into UserToken (id_user, token) VALUES('"+id_user+"', '"+token+"'); ";
            cmd = new SqlCommand(query, conn);
            string? result = "OK";

            try
            {
                cmd.ExecuteNonQuery();                                                   
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return result;
        }

    }
}
