using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Linq;
using System.Globalization;

namespace VGTB_SHOP_LIST
{
    public partial class ViewDataPage : System.Web.UI.Page
    {
        ClassDB sv = new ClassDB();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeader.Text = "THAI AGRI FOODS PUBLIC COMPANY LIMITED";
            lblLine1.Text = "WELCOME TO TAF VGTBAPP";
            lblLine2.Text = "Please Log in.....";
        }

        protected void Alert(String str, Object My)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "validationAlert", "window.alert('" + str + "');", true);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPwd.Text;
            DataTable dt = sv.CheckLogin(username, password);

            if (txtUserName.Text == String.Empty || txtPwd.Text == String.Empty)
            {
                Alert("โปรดกรอก username และ password", this);
                return;
            }
            Session["USR"] = txtUserName.Text.ToUpper();
             Response.Redirect("Selectdate.aspx");
            //checked login

            if (dt.Rows.Count > 0)
            {
                Session["USR"] = txtUserName.Text.ToUpper();
                Response.Redirect("Selectdate.aspx");

            }
            else
            {
                Alert(" username หรือ password ไม่ถูกต้อง", this);
            }
        }

        
    }
    }
    


    




