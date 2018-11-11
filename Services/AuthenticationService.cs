using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class AuthenticationService
    {
        public User Authenticate(UsernamePasswordPair login)
        {
            int isValid = 0;  //0 to represent invalid, if it is valid it takes the value of userid

            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();

            SqlCommand cmd = new SqlCommand("spAuthenticateUser", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", login.Username);
            cmd.Parameters.AddWithValue("@Password", login.Password);

            SqlParameter p = new SqlParameter();
            p.ParameterName = "@IsValid";
            p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p);

            p = new SqlParameter();
            p.ParameterName = "@UserType";
            p.SqlDbType = SqlDbType.NVarChar;
            p.Size = 100;
            p.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p);

            cmd.ExecuteNonQuery();

            isValid = (int)cmd.Parameters["@IsValid"].Value;

            User user = new User();
            user.Id = isValid;
            if (user.Id != 0)
            {
                user.Usertype = (string)cmd.Parameters["@Usertype"].Value;
                user.Login = login;

                string SqlUpdate = "";
                int numberOfRows = 0;              

                if (user.Usertype.Equals("manager") && isValid != 0)
                {
                    SqlUpdate = "update users set [authorization] = 2 WHERE username = '" + login.Username + "'";
                    cmd = new SqlCommand(SqlUpdate, conn);
                    numberOfRows = cmd.ExecuteNonQuery();

                    user.Authorization = 2;
                }
                else if (user.Usertype.Equals("applicant") && isValid != 0)
                {
                    SqlUpdate = "update users set [authorization] = 1 WHERE username = '" + login.Username + "'";
                    cmd = new SqlCommand(SqlUpdate, conn);
                    numberOfRows = cmd.ExecuteNonQuery();

                    user.Authorization = 1;
                }
            }
           
            conn.Close();

            return user;
        }

        public bool Authorize(int userId)
        {
            bool isValid = false;
            DateTime? dt = null;
            double offset = 0;

            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();

            SqlDataReader reader = null;

            string SqlQuery = "select timestamp from users where Id = '" + userId + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dt = reader.GetDateTime(0);
            }
            reader.Close();
            offset = DateTime.Now.Subtract(dt.Value).TotalSeconds;
            if (offset > 300)//invalid if x amount of seconds have passed
            {
                string SqlUpdate = "update users set [authorization] = null WHERE Id = '" + userId + "'";
                cmd = new SqlCommand(SqlUpdate, conn);
                int numberOfRows = cmd.ExecuteNonQuery();
            }
            else
            {
                string SqlUpdate = "update users set timestamp = CURRENT_TIMESTAMP WHERE Id = '" + userId + "'";
                cmd = new SqlCommand(SqlUpdate, conn);
                int numberOfRows = cmd.ExecuteNonQuery();
                isValid = true;
            }
            conn.Close();
            return isValid;
        }
    }
}
