using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class UserService
    {
        public UploadedUser Upload(User user)
        {
            UploadedUser uploaded = new UploadedUser();

            try
            {
                SqlConnection conn = new SqlConnection(ADO.conn_str);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "insert into users " +
                                    "(username, password, timestamp, usertype)" +
                                    "values " +
                                    "(@username, @password, @timestamp, @usertype)";

                cmd.Parameters.AddWithValue("@username", user.Login.Username);
                cmd.Parameters.AddWithValue("@password", user.Login.Password); //frontend should initially set this to 1
                cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@usertype", user.Usertype);

                cmd.ExecuteNonQuery();

                SqlDataReader reader = null;

                string SqlQuery = "select id from users where username = '" + user.Login.Username + "'";
                cmd = new SqlCommand(SqlQuery, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    uploaded.Id = reader.GetInt32(0);   
                }

                reader.Close();

                conn.Close();
                
            }
            catch (SqlException ex)
            {
                string s = ex.Message.ToString();
            }

            return uploaded;

        }


    }
}
