using Magic_Interface.DTO;
using Magic_Interface.Interface;
using System.Data.SqlClient;

namespace Magic_DAL.Context
{
    public class Login_Context : Database, ILogin
    {
        private readonly string connectionstring;
        private readonly Database connection;

        public Login_Context(string cs)
        {
            connectionstring = cs;
            connection = new Database(connectionstring);
        }

        public LoginDTO AttemptLogin(LoginDTO loginDTO)
        {
            LoginDTO login = new LoginDTO();

            string username = loginDTO.Name;
            string password = loginDTO.Password;
            bool verified = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            string query = "SELECT * FROM [User] WHERE Name = (@name)";
            using (SqlCommand commandDatabase = new SqlCommand(query, connection))
            {
                commandDatabase.Parameters.AddWithValue("@name", username);

                SqlDataReader reader;

                try
                {
                    connection.Open();
                    reader = commandDatabase.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            verified = BCrypt.Net.BCrypt.Verify(password, reader["Password"].ToString().Trim());
                            if (verified)
                            {
                                // True, log in accepted
                                login = new LoginDTO()
                                {
                                    Id = reader.GetInt32(0),
                                    Email = reader.GetString(1),
                                    Name = reader.GetString(2),
                                    Password = reader.GetString(3),
                                };

                            }
                            else
                            {
                                throw new Exception("Incorrect credentials");
                            }
                        }
                    }
                }
                catch
                {
                    Console.Write("Connection Error", "Information");
                }
                finally
                {
                    connection.Close();
                }
            }
            return login;
        }

        public bool Register(LoginDTO loginDTO)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand command;
            int inUse = 0;

            connection.Open();
            string sql = "SELECT COUNT(1) FROM [User] WHERE Name = @Username";
            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", loginDTO.Name);
            inUse = (int)command.ExecuteNonQuery();
            connection.Close();


            if (inUse >= 1)
            {
                throw new Exception("Username is already in use");
            }

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(loginDTO.Password);

            connection.Open();
            sql = "INSERT INTO [User](Email, Name, Password) VALUES(" +
                "@Email," +
                "@Name," +
                "@Password)";

            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", loginDTO.Email);
            command.Parameters.AddWithValue("@Name", loginDTO.Name);
            command.Parameters.AddWithValue("@Password", passwordHashed);
            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }
    }
}
