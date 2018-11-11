using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class ApplicationService
    {
        public Application GenerateApplication(int id)
        {
            Application application = new Application();

            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();

            SqlDataReader reader = null;

            string SqlQuery = "select * from applications where UserId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                application.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                application.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                application.ResumeId = reader.GetInt32(reader.GetOrdinal("ResumeId"));
                application.Salary = reader.GetInt32(reader.GetOrdinal("Salary"));
                application.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                application.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                application.Gender = reader.GetString(reader.GetOrdinal("Gender"));
                application.DateApplied = reader.GetDateTime(reader.GetOrdinal("DateApplied"));
                application.Job = reader.GetString(reader.GetOrdinal("Job"));
                application.Address = reader.GetString(reader.GetOrdinal("Address"));
                application.City = reader.GetString(reader.GetOrdinal("City"));
                application.State = reader.GetString(reader.GetOrdinal("State"));
                application.Zip = reader.GetInt32(reader.GetOrdinal("Zip"));
            }

            reader.Close();

            return application;
        }

        public List<int> GenerateApplicants()
        {
            List<int> users = new List<int>();

            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();
            SqlDataReader reader = null;

            string SqlQuery = "select id from users where UserType = 'applicant'";
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(reader.GetInt32(0));
            }

            reader.Close();

            return users;
        }

        public void Upload(Application application)
        {  
            try
            {
                SqlConnection conn = new SqlConnection(ADO.conn_str);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "insert into applications " +
                                    "(userid, resumeid, salary, firstname, lastname, gender, dateapplied, job, address, city, state, zip) " +
                                    "values " +
                                    "(@userid, @resumeid, @salary, @firstname, @lastname, @gender, @dateapplied, @job, @address, @city, @state, @zip)";

                cmd.Parameters.AddWithValue("@userid", application.UserId);
                cmd.Parameters.AddWithValue("@resumeid", application.ResumeId); //frontend should initially set this to 1
                cmd.Parameters.AddWithValue("@salary", application.Salary);
                cmd.Parameters.AddWithValue("@firstname", application.FirstName);
                cmd.Parameters.AddWithValue("@lastname", application.LastName);
                cmd.Parameters.AddWithValue("@gender", application.Gender);
                cmd.Parameters.AddWithValue("@dateapplied", application.DateApplied);
                cmd.Parameters.AddWithValue("@job", application.Job);
                cmd.Parameters.AddWithValue("@address", application.Address);
                cmd.Parameters.AddWithValue("@city", application.City);
                cmd.Parameters.AddWithValue("@state", application.State);
                cmd.Parameters.AddWithValue("@zip", application.Zip);

                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (SqlException ex)
            {
                string s = ex.Message.ToString();
            }
        }

    }
}
