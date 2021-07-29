using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountNote.DBSource;
namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (this.Session["UserLoginInfo"] != null)
            {
                this.plcLogin.Visible = false;
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }

            else
                this.plcLogin.Visible = true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {


            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPassword.Text;
            //check
            if(string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            {
                this.ltMsg.Text = "帳號/密碼錯誤";
                return;
            }
            var dr = UserInfoManager.GETUserInoAccount(inp_Account);
            if(dr == null)
            {
                this.ltMsg.Text = "帳號/密碼不允許空白";
                return;
            }

            if(string.Compare(dr["Account"].ToString(), inp_Account,true) == 0 && string.Compare(dr["PWD"].ToString(), inp_PWD,false) == 0)
            {
                this.Session["UserLoginInfo"] = dr["Account"].ToString();
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
            else
            {
                this.ltMsg.Text = "登入失敗，請檢查帳號或密碼是否正確";
                return;
            }
        }
    }
}