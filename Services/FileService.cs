using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class FileService
    {
        public MemoryStream Download(int id)
        {
            MemoryStream ms = new MemoryStream();
            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();

            string SqlQuery = "select [data] from files where id = " + id;
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);

            byte[] buffer = null;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    int length = (int)reader.GetBytes(0, 0L, null, 0, 0);
                    buffer = new byte[length];
                    reader.GetBytes(0, 0L, buffer, 0, length);
                }
            }

            try
            {
                ms = new MemoryStream(buffer);
            }
            catch (ArgumentException ex)
            {
                string s = ex.Message.ToString();
            }

            conn.Close();

            return ms;
        }

        public int Upload(MemoryStream ms)
        {
            SqlConnection conn = new SqlConnection(ADO.conn_str);
            conn.Open();

            SqlCommand cmd = new SqlCommand("spInsertResume", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@resume", ms.ToArray());

            SqlParameter p = new SqlParameter();
            p.ParameterName = "@id";
            p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p);

            cmd.ExecuteNonQuery();

            int id = (int)cmd.Parameters["@id"].Value;

            conn.Close();

            return id;
        }
    }
}
