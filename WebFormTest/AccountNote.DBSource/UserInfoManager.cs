using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AccountNote.DBSource
{
    public class UserInfoManager
    {
        public static DataRow GETUserInfo(string id)
        {
            string connectionString = DBhelper.GetConnectionString();
            string dbCommandString =
                @"SELECT ID, Name 
                  FROM User_info
                  WHERE ID = @id
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommandString, connection);
                command.Parameters.AddWithValue("@id", id);//確保資料安全性

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new System.Data.DataTable();
                    dt.Load(reader);
                    reader.Close();

                    if (dt.Rows.Count == 0)
                        return null;

                    DataRow dr = dt.Rows[0];
                    return dr;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }//自帶connection.close()
        }

        public static DataRow GETUserInoAccount(string account)
        {
            string connectionString = DBhelper.GetConnectionString();
            string dbCommandString =
                @"SELECT  [ID] ,[Account],[PWD] ,[Name] ,[Email]
                   FROM UserInfo
                   WHERE [Account] = @account
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(dbCommandString, connection);
                command.Parameters.AddWithValue("@account", account);//確保資料安全性

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    if (dt.Rows.Count == 0)
                        return null;

                    DataRow dr = dt.Rows[0];
                    return dr;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }//自帶connection.close()
        }
        //public static string GetConnectionString()
        //{
        //    // string val = ConfigurationManager.AppSettings["ConnectionString"];
        //    string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    return val;
        //}
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount必須介於0到1000000之間");
            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType必須介於0到1之間");


            string connectionString = DBhelper.GetConnectionString();

            string dbCommandString =
            @"INSERT INTO Accounting
                       (UserID,Caption,Amount,ActType,CreatDate,Body)
                    VALUES
                       (
                         @userID, 
                         @caption,
                         @amount,
                         @actType,
                         @creatDate, 
                         @body);
             
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {

                    command.Parameters.AddWithValue("@userID", userID);//確保資料安全性
                    command.Parameters.AddWithValue("@caption", caption);//確保資料安全性
                    command.Parameters.AddWithValue("@amount", amount);//確保資料安全性
                    command.Parameters.AddWithValue("@actType", actType);//確保資料安全性
                                                                         // command.Parameters.AddWithValue("@creatDate", acc);//確保資料安全性
                    command.Parameters.AddWithValue("@body", body);//確保資料安全性

                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();
                        Console.WriteLine($"{effectRows} has changed");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }

            //}
        }
    }
}
