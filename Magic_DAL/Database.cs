using System.Data.SqlClient;

namespace Magic_DAL
{
    public class Database
    {
        private string connectiestring;
        SqlConnection cnn;
        SqlCommand command;
        SqlDataReader reader;

        private readonly string connect;

        public Database(string cs)
        {
            connect = cs;
        }
        public Database()
        {
            connectiestring = "Server=mssqlstud.fhict.local;Database=dbi485841_magic;User Id=dbi485841_magic;Password=Welkom01;";
        }
    }
}
