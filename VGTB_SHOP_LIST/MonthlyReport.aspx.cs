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
    public partial class MonthlyReport : System.Web.UI.Page
    {
        ClassDB sv = new ClassDB();
        DataTable dt;
        private decimal totalprice = (decimal)0.0;
        private decimal totalweight = (decimal)0.0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USR"] == null)
                {
                    Response.Redirect("Loginpage.aspx");
                }
                disPlay.Visible = false;
                LblUser.Text = Session["USR"].ToString();                        
                dt = new DataTable();
               
                SelectDateYear();
           
                MakeDataTable();          
                lblHeader.Text = "THAI AGRI FOODS PUBLIC COMPANY LIMITED";

                ddlMonth.SelectedValue = "0";
            }

            

        }
        public void SelectDateYear()
        {
            DateTime reportMonth = DateTime.MinValue;
                        
            ddlMonth.DataSource = Enumerable.Range(1, 12).Select(a => new
            {
              
            MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(a),

                MonthNumber = a
              
            });

            ddlMonth.DataBind();
            ddlMonth.Items.Insert(0, new ListItem("-- Select --", "0"));

            ddlYear.DataSource = Enumerable.Range(DateTime.Now.Year - 99, 100).Reverse();
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("-- Select --", "0"));


            ddlMonth.SelectedValue = reportMonth.Month.ToString();
            ddlYear.SelectedValue = reportMonth.Year.ToString();

        }
        protected void Alert(String str, Object My)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "validationAlert", "window.alert('" + str + "');", true);
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        protected void Backbutton_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("Selectdate.aspx");
        }
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("LoginPage.aspx");

        }

        private void MakeDataTable()
        {
            dt.Columns.Add("ID");         
            dt.Columns.Add("ProductList");        
            dt.Columns.Add("TotalWeight");
            dt.Columns.Add("Totalprice");

        }
      
        private void BindGrid()
        {
            GridView_RPT.DataSource = dt;
            GridView_RPT.DataBind();
        }
        
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView_RPT.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
          
            String _RPTName = "ReportMonthly";
            DataSet DS = new DataSet();
            DS = sv.SelectReportByMonth(ddlYear.SelectedValue.ToString(), Right("00" + ddlMonth.SelectedValue.ToString(), 2), _RPTName);


            if (DS.Tables[0].Rows.Count != 0)
            {
                String _P = "";
                _P = "RPT=" + _RPTName;
                _P += "&PBYDATE=N";
                _P += "&PUSER=" + LblUser.Text;
                _P += "&PDATE=" + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedItem.Text;

                Session["DATA"] = DS;

                ResponseHelper.Redirect("ViewReport.aspx?ID=" + new Encryption64().Encrypt(_P, "!#$a54?3"), "_blank", "menubar=0,width=800,height=600,resizable=yes");
            }
            else
            {
                Alert("ไม่พบข้อมูลครับ!!", this);
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_RPT.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void GridView_RPT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalprice += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalprice"));
                totalweight += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWeight"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = String.Format("TOTAL ");
                e.Row.Cells[2].Text = String.Format(totalweight + " : Kg");
                e.Row.Cells[3].Text = String.Format("{0:c}", totalprice);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
           
            GridView_RPT.DataSource = null;
            GridView_RPT.DataBind();
            DataTable dt = sv.SelectReportByMonth(ddlYear.SelectedValue.ToString(), Right("00"+ ddlMonth.SelectedValue.ToString(), 2), "LIST").Tables[0];
            if (ddlMonth.SelectedValue == "0" || ddlYear.SelectedValue == "0")
            {
                Alert("โปรดเลือกเดือน และปีที่ต้องการพิมพ์รายงาน", this);
                return;
            }
            if (dt.Rows.Count>0)
            {
                disPlay.Visible = true;
                GridView_RPT.DataSource = dt;
                GridView_RPT.DataBind();
            }
            else
            {
                Alert("ไม่พบข้อมูลครับ!!",this);
                disPlay.Visible = false;
            }
        }

        public string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
    }
 }
