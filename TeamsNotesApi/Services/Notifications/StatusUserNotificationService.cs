using System.Data.SqlClient;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class StatusUserNotificationService : IStatusUserNotificationService
    {
        private SqlConnection? conn;
        private SqlCommand? cmd;
        private string? query = "";

        public StatusUserNotificationService()
        {
            conn = CVM_Connection.Connect();
        }

        public string InsertTokenUsers(int id_user, string token)
        {
            string? result = "OK";
            query = "insert into UserToken (id_user, token) VALUES('"+id_user+"', '"+token+"'); ";                       
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();                                                   
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
        public string ChangeStatusUser(int id_user,int status)
        {
            string? result = "OK";
            query = $"UPDATE UserToken SET status = {status} where id_user = {id_user}";            
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                return result;
            }
            catch(Exception ex)
            {
                result = ex.Message;

                return result;
            }
        }

    }
}
