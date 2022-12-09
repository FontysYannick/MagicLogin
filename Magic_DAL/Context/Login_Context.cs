using Dapper;
using Magic_Interface.DTO;
using Magic_Interface.Interface;
using System.Data;
using System.Data.SqlClient;

namespace Magic_DAL.Context
{
    public class Login_Context : Database, ILogin
    {
        private readonly string cons;
        private readonly IDbConnection connection;

        public Login_Context(string cs)
        {
            this.cons = cs;
            connection = new System.Data.SqlClient.SqlConnection(cs);
        }

        public LoginDTO AttemptLogin(LoginDTO loginDTO)
        {
            LoginDTO user = new LoginDTO();
            bool verified = false;


            var sql = "SELECT * FROM [User] WHERE Name = (@Name)";


            //execute statement
            try
            {
                using (connection)
                {
                    //execute query on database and return result
                    user = connection.QuerySingle<LoginDTO>(sql, new { Name = loginDTO.Name });
                    verified = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password.ToString().Trim());

                    if (!verified)
                    {
                        throw new Exception("Incorrect credentials");
                    }
                }
            }

            //catches exceptions
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //closes database connection
            finally
            {
                connection.Close();
            }

            return user;
        }


        public bool Register(LoginDTO loginDTO)
        {
            var sql = "SELECT COUNT(1) FROM [User] WHERE Name = @Name";
            string inUse = "0";

            //execute statement
            try
            {
                using (connection)
                {
                    //execute query on database and return result
                    inUse = connection.ExecuteScalar<Int32>(sql, new { Name = loginDTO.Name }).ToString();
                }
            }

            //catches exceptions
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //closes database connection
            finally
            {
                connection.Close();
            }


            if (inUse != "0")
            {
                throw new Exception("Username is already in use");
            }

            IDbConnection conn = new System.Data.SqlClient.SqlConnection(cons);
            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(loginDTO.Password);

            sql = "INSERT INTO [User](Email, Name, Password) VALUES(@Email,@Name,@Password)";


            try
            {

                using (conn)
                {
                    //execute query on database and return result
                    conn.Query<LoginDTO>(sql, new { Email = loginDTO.Email, Name = loginDTO.Name, Password = passwordHashed });
                }
            }

            //catches exceptions
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //closes database connection
            finally
            {
                conn.Close();
            }

            return true;
        }
    }
}
