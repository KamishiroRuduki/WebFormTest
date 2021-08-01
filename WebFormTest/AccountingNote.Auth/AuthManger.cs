using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AccountNote.DBSource;

namespace AccountingNote.Auth
{
    public class AuthManger
    {
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }
        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;
            if (account == null)
                return null;
            DataRow dr = UserInfoManager.GETUserInoAccount(account);

            if (dr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.ID = dr["ID"].ToString();
            model.Account = dr["Account"].ToString();
            model.Name = dr["Name"].ToString();
            model.Email = dr["Email"].ToString();

            return model;
        }

        public static void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null; //清除登入資訊
        }

        public static bool TryLogin(string account, string pwd, out string errMsg )
        {
            //檢查帳號/密碼是否正確
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errMsg = "帳號/密碼錯誤";
                return false;
            }
            //檢查此帳號是否存在
            var dr = UserInfoManager.GETUserInoAccount(account);
            if (dr == null)
            {
                errMsg = $"帳號{account}不存在";
                return false;
            }
            if (string.Compare(dr["Account"].ToString(), account, true) == 0 && string.Compare(dr["PWD"].ToString(), pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errMsg = string.Empty;
                return true;
            }
            else
            {
                errMsg = "登入失敗，請檢查帳號或密碼是否正確";
                return false;
            }
            
        }
    }
}
