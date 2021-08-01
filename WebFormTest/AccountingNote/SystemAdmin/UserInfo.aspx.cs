using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountNote.DBSource;
using System.Data;
using AccountingNote.Auth;
namespace AccountingNote.SystemAdmin
{
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!AuthManger.IsLogined())
                {

                    Response.Redirect("/Login.aspx");
                    return;
                }
                //string account = this.Session["UserLoginInfo"] as string;
                //DataRow dr = UserInfoManager.GETUserInoAccount(account);
                var cUser = AuthManger.GetCurrentUser();
                if (cUser == null)
                {
                    Response.Redirect("/Login.aspx");
                    return;

                }
                this.ltAccount.Text = cUser.Account;
                this.ltName.Text = cUser.Name;
                this.ltEmail.Text = cUser.Email;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AuthManger.Logout();
            Response.Redirect("/Login.aspx"); //導至登入頁

        }
    }
}