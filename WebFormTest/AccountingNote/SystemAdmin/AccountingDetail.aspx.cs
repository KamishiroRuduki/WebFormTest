using AccountNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.Auth;
namespace AccountingNote.SystemAdmin
{
    public partial class AccountingDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!AuthManger.IsLogined())
          //if(!AuthManger.IsLogined())
            {
                Response.Redirect("/login.aspx");
                return;
            }
            string account = this.Session["UserLoginInfo"] as string;
            var cUser = AuthManger.GetCurrentUser();
            if (cUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;

            }
            if (!this.IsPostBack)
            {
            if(this.Request.QueryString["ID"] == null)
            {
                this.btnDel.Visible = false;
            }
            else
            {
                this.btnDel.Visible = true;
                string idtext = this.Request.QueryString["ID"];
                int id;
                if(int.TryParse(idtext,out id))
                {
                    var drAcc = AccountingManager.GetAccounting(id, cUser.ID);
                    if(drAcc == null)
                    {
                        this.ltMsg.Text = "無資料";
                        this.btnDel.Visible = false;
                        this.btnSave.Visible = false;
                    }
                    else
                    {
                        this.ddlActType.SelectedValue = drAcc["ActType"].ToString();
                        this.txtAmount.Text = drAcc["Amount"].ToString();
                        this.txtCaption.Text = drAcc["Caption"].ToString();
                        this.txtDesc.Text = drAcc["Body"].ToString();
                    }
                }

            }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            if(!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br/>", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManger.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/login.aspx");
                return;
            }

            string userID = currentUser.ID;
            string actTypeText = this.ddlActType.SelectedValue;
            string amountText = this.txtAmount.Text;
            string caption = this.txtCaption.Text;
            string body = this.txtDesc.Text;

            int amount = Convert.ToInt32(amountText);
            int actType = Convert.ToInt32(actTypeText);

            
            string idtext = this.Request.QueryString["ID"];
            if( string.IsNullOrWhiteSpace(idtext))
            {
                AccountingManager.CreateAccounting(userID, caption, amount, actType, body);
            }
            else
            {
                int id;
            if (int.TryParse(idtext, out id))
            {
                    AccountingManager.UpateAccount(id,userID, caption, amount, actType, body);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }

        private bool CheckInput(out List<string> errorMsgList)
        {
            List<string> msgList = new List<string>();
            if(this.ddlActType.SelectedValue !="0"&& this.ddlActType.SelectedValue != "1")
            {
                msgList.Add("Type必須是0或1");
            }

            if(string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("沒有Amount資料");
            }
            else
            {
                int tempInt;
                if(!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount必須是數字");
                }                
            }
            errorMsgList = msgList;
            if (msgList.Count == 0)
                return true;
            else
                return false;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string idtext = this.Request.QueryString["ID"];
            if (string.IsNullOrWhiteSpace(idtext))
            {
                return;
            }
            else
            {
                int id;
                if (int.TryParse(idtext, out id))
                {
                    AccountingManager.DeleteAccount(id);
                }
            }
            Response.Redirect("/SystemAdmin/AccountingList.aspx");
        }
    }
}