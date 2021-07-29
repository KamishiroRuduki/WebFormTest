using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountNote.DBSource;
using System.Data;
using System.Drawing;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] == null)
            {               
                Response.Redirect("/login.aspx");
                return;
            }
            string account = this.Session["UserLoginInfo"] as string;
            var dr = UserInfoManager.GETUserInoAccount(account);

            if(dr == null )
            {
                Response.Redirect("/login.aspx");
                return;
            }

            var dt = AccountingManager.GetAccountingList(dr["UserInfo"].ToString());
            if(dt.Rows.Count>0)
            { 
            this.gvAccountList.DataSource = dt;
            this.gvAccountList.DataBind();
            }
            else
            {
                this.gvAccountList.Visible = false;
                this.plcNoData.Visible = true;

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.Session["UserLoginInfo"] == null)
            {
                Response.Redirect("/SystemAdmin/UserInfo.aspx");
                return;
            }
            else
                Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = row.FindControl("lbl") as Label;
                //Literal ltl = row.FindControl("ltlActType") as Literal;
                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");

                if (actType == 0)
                {
                   // ltl.Text = "支出";
                    lbl.Text = "支出";
                }
                    
                else
                {
                 //   ltl.Text = "收入";
                    lbl.Text = "收入";
                }

                if( dr.Row.Field<int>("Amount") > 1500 )
                {
                    lbl.ForeColor = Color.Red;
                }
                    
            }
        }
    }
}