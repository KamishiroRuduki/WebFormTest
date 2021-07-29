using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccountNote.DBSource
{
    public class AccountingManager
    {
        public static string GetConnectionString()
        {
            // string val = ConfigurationManager.AppSettings["ConnectionString"];
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetAccountingList(string userID)
        {
            string connStr = GetConnectionString();
            string dbCommand =
                $@"SELECT ID,
                         Caption,
                         Amount,
                         ActType,
                         CreatDate
                    FROM Accounting
                    WHERE UserID = @userID
                  ";
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {

                    command.Parameters.AddWithValue("@userID", userID);//確保資料安全性
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);


                        return dt;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return null;
                    }
                }
            }
        }

        public static DataRow GetAccounting(int id , string userID)
        {
            string connStr = GetConnectionString();
            string dbCommand =
                $@"SELECT ID,
                         Caption,
                         Amount,
                         ActType,
                         CreatDate,
                         Body
                    FROM Accounting
                    WHERE ID = @id AND UserID = @userID
                  ";
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                using (SqlCommand command = new SqlCommand(dbCommand, connection))
                {

                    command.Parameters.AddWithValue("@id", id);//確保資料安全性
                    command.Parameters.AddWithValue("@userID", userID);//確保資料安全性
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);


                        return dt.Rows[0];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return null;
                    }
                }
            }
        }
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount必須介於0到1000000之間");
            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType必須介於0到1之間");


            string connectionString = GetConnectionString();

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
                    command.Parameters.AddWithValue("@creatDate", DateTime.Now);//確保資料安全性
                    command.Parameters.AddWithValue("@body", body);//確保資料安全性

                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        public static bool UpateAccount(int id, string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount必須介於0到1000000之間");
            if (actType < 0 || actType > 1)
                throw new ArgumentException("actType必須介於0到1之間");


            string connectionString = GetConnectionString();

            string dbCommandString =
            @"UPDATE [Accounting]    
              SET
                     UserID=@userID, 
                     Caption=@caption,
                     Amount=@amount,
                     ActType=@actType,
                     CreatDate=@creatDate, 
                     Body=@body
             WHERE 
                     ID=@id
             
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {

                    command.Parameters.AddWithValue("@userID", userID);//確保資料安全性
                    command.Parameters.AddWithValue("@caption", caption);//確保資料安全性
                    command.Parameters.AddWithValue("@amount", amount);//確保資料安全性
                    command.Parameters.AddWithValue("@actType", actType);//確保資料安全性
                    command.Parameters.AddWithValue("@creatDate", DateTime.Now);//確保資料安全性
                    command.Parameters.AddWithValue("@body", body);//確保資料安全性
                    command.Parameters.AddWithValue("ID", @id);//確保資料安全性

                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();
                        if (effectRows == 1)
                            return true;
                        else
                            return false;


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return false;
                    }
                }
            }
        }

        public static void  DeleteAccount(int id)
        {

           string connectionString = GetConnectionString();

            string dbCommandString =
            @"DELETE [Accounting]    
              WHERE 
                   ID=@id
             
                ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {

                    command.Parameters.AddWithValue("ID", @id);//確保資料安全性

                    try
                    {
                        connection.Open();
                        int effectRows = command.ExecuteNonQuery();



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                    }
                }
            }
        }
    }
}
