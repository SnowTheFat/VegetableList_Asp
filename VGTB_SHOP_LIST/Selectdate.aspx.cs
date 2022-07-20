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
using Image = System.Drawing.Image;

namespace VGTB_SHOP_LIST
{
    public partial class Selectdate : System.Web.UI.Page
    {
        ClassDB sv = new ClassDB();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USR"] == null)
            {
                Response.Redirect("Loginpage.aspx");
            }
            LblUser.Text = Session["USR"].ToString();
            lblHeader.Text = "THAI AGRI FOODS PUBLIC COMPANY LIMITED";
            
        }
         public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        protected void CalendarJob_DayRender(object sender, DayRenderEventArgs e)
        {

            string datejob = e.Day.Date.ToString("yyyyMMdd ", CultureInfo.CreateSpecificCulture("en-US"));
            DataTable dt = sv.CheckDateJob(datejob);

            if (dt.Rows.Count > 0)
            {
                System.Web.UI.WebControls.Image image;
                image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "Images/bullet_ball_glass_green.png";
                e.Cell.Controls.Add(image);
                
            }
           

        }
        protected void CalendarJob_SelectionChanged(object sender, EventArgs e)
        {
            lbljobPick.Text = CalendarJob.SelectedDate.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            this.Response.Redirect("HomePage.aspx?D=" + lbljobPick.Text);
        }       
        protected void ImageMonthRPT_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("MonthlyReport.aspx");
        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("LoginPage.aspx");

        }
    }
}