using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountNote.DBSource
{
   public class DBhelper
    {
        public static string GetConnectionString()
        {
            // string val = ConfigurationManager.AppSettings["ConnectionString"];
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        public static DataTable ReadDataTable(string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {

                    //    command.Parameters.AddWithValue("@userID", userID);//確保資料安全性
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    return dt;


                }
            }
        }
        public static DataRow ReadDataRow(  string connStr, string dbCommand, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {

                    command.Parameters.AddRange(list.ToArray());


                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    return dt.Rows[0];


                }
            }
        }
        public static void ModifyData(string connectionString, string dbCommandString, List<SqlParameter> paramlist)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {

                    command.Parameters.AddRange(paramlist.ToArray());
                    connection.Open();
                    int effectRows = command.ExecuteNonQuery();




                }
            }
        }
    }
}
