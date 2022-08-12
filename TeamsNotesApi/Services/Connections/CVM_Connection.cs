using System.Data.SqlClient;

namespace TeamsNotesApi.Services.Connections
{
    public class CVM_Connection
    {
        public static SqlConnection Connect()
        {
            SqlConnection? connection;

            //string lstrConn = "Data Source = localhost; Trusted_Connection=True; Initial Catalog = CVM_GPA_CTN_01; User ID = SA; Password = 10deagosto";
            string lstrConn = "Data Source=localhost;Initial Catalog=CVM_GPA_CTN_01;User ID=SA;Password=10deagosto; trustServerCertificate=true; Trusted_Connection=False; MultipleActiveResultSets=true;";
            connection = new SqlConnection(lstrConn);

            connection.Open();

            return connection;
        }
    }
}
