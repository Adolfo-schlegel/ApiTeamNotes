using System.Data.SqlClient;
using TeamsNotesApi.Models.Notification;
using TeamsNotesApi.Services.Connections;
using TeamsNotesApi.Services.Interfaces;

namespace TeamsNotesApi.Services.Notifications
{
    public class CountStatusNoteService : ICountStatusNoteService
    {
        private SqlConnection? conn;
        private SqlCommand? cmd;
        private SqlDataReader? reader;
        private string? query = "";

        public CountStatusNoteService()
        {
            conn = CVM_Connection.Connect();
        }

        public List<CountStatusNote> SelectCountNotes()
        {
            query = "SELECT r1.id_usuario, k1.token, qt=COUNT(*) FROM vs_reg_destinos1 r1 INNER JOIN userToken K1 ON r1.id_usuario = k1.id_user WHERE r1.cd_medio = 'M' and r1.cd_lectura_estado = 'P' GROUP BY r1.id_usuario, k1.token";

            cmd = new SqlCommand(query, conn);

            List<CountStatusNote> lst = new();

            using (reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {                    
                    while(reader.Read())
                    {
                        CountStatusNote countStatusNote = new();

                        countStatusNote.id_user = (int)reader.GetDecimal(0);
                        countStatusNote.token = reader.GetString(1);
                        countStatusNote.countNotesPending = reader.GetInt32(2);
                        
                        lst.Add(countStatusNote);
                    }
                }
            }

            return lst;
        }
    }
}
