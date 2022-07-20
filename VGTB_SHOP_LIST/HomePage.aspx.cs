using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Globalization;


namespace VGTB_SHOP_LIST
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        
        ClassDB sv = new ClassDB();
        DataTable dt, ddl;
        private decimal totalprice = (decimal)0.0;
        private decimal weightship = (decimal)0.0;
      

        protected void Page_Load(object sender, EventArgs e)
        {          
          
            if (!IsPostBack)
            {
                if (Session["USR"] == null)
                {
                    Response.Redirect("Loginpage.aspx");
                }
                LblUser.Text = Session["USR"].ToString();
                //disPlay.Visible = false;
                //SaveData.Visible = false;
                dt = new DataTable();
                MakeDataTable();
                lblHeader.Text = "THAI AGRI FOODS PUBLIC COMPANY LIMITED";
                GetDDL();


                if (Request.QueryString["D"] != null)
                {
                    txtDate.Text = Request.QueryString["D"];                   
                    GetData();
                }

            }
           

        }
        private void GetDDL()
        {
            ddl = new DataTable();
            ddl = sv.GetDataToTBL();
            VegetableList.DataSource = ddl;
            VegetableList.DataTextField = "PRODUCTNAME";
            VegetableList.DataValueField = "PRODUCTKEY";
            VegetableList.DataBind();
            VegetableList.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        private void MakeDataTable()
        {
            dt.Columns.Add("ID");
            dt.Columns.Add("DETAILKEY");
            dt.Columns.Add("ProductList");
            dt.Columns.Add("Price");
            dt.Columns.Add("Weight");
            dt.Columns.Add("Totalpricre");

        }

        private void AddToDataTable()
        {
            if (VegetableList.SelectedValue == "0" || txtPrice.Text == String.Empty ||
                txtWeight.Text == String.Empty || txtDate.Text == String.Empty)
            {
                Alert("โปรดเลือกรายการ ระบุราคา น้ำหนัก และเลือกวันที่!!", this);
                return;
            }
            //SaveData.Visible = true;
           
            dt = new DataTable();
            MakeDataTable();
            for (int i = 0; i <= GridView_LIST.Rows.Count - 1; i++)
            {
                Label lblDTID = (Label)GridView_LIST.Rows[i].FindControl("lblDTID");
                Label lblID = (Label)GridView_LIST.Rows[i].FindControl("lblID");
                Label lblList = (Label)GridView_LIST.Rows[i].FindControl("lblList");
                Label lblPrice = (Label)GridView_LIST.Rows[i].FindControl("lblPrice");
                Label lblWeight = (Label)GridView_LIST.Rows[i].FindControl("lblWeight");
                Label lblTotalPrice = (Label)GridView_LIST.Rows[i].FindControl("lblTotalPrice");

                dt.Rows.Add(new object[]{ lblID.Text, lblDTID.Text, lblList.Text, lblPrice.Text, lblWeight.Text, lblTotalPrice.Text });
            }
            double price = double.Parse(txtPrice.Text);
            double weight = double.Parse(txtWeight.Text);
            double amount = price * weight;
            dt.Rows.Add(new object[] { VegetableList.SelectedValue.ToString(),"", VegetableList.SelectedItem.Text, txtPrice.Text, txtWeight.Text, amount.ToString() });
         

        }
        protected void Alert(String str, Object My)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "validationAlert", "window.alert('" + str + "');", true);
        }

        protected void ButtonSenddata_Click(object sender, EventArgs e)
        {
            string id = "";
            string curday = Convert.ToDateTime(txtDate.Text.ToString()).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            DataTable dt = sv.CheckOrderKey(curday);

            if (VegetableList.SelectedValue == "0" || txtPrice.Text == String.Empty ||
               txtWeight.Text == String.Empty )
            {
                Alert("โปรดเลือกรายการ ระบุราคา น้ำหนัก!!", this);
                return;
            }

            if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0][0].ToString();
                }
                else
                {
                    AddToDataTable();
                    sv.InsertDataHead(curday);
                    dt = sv.CheckOrderKey(curday);

                    if (dt.Rows.Count > 0)
                    {
                        id = dt.Rows[0][0].ToString();

                    }
                }

                double price = double.Parse(txtPrice.Text);
                double weight = double.Parse(txtWeight.Text);
                double amount = price * weight;
                string resualt = sv.InsertData(id, VegetableList.SelectedValue, txtPrice.Text, txtWeight.Text, amount.ToString());


                if (resualt == "OK")
                {
                    Alert("บันทึกข้อมูลสำเร็จครับ!!", this);
                    VegetableList.SelectedValue = "0";
                    txtPrice.Text = "";
                    txtWeight.Text = "";
                    GetData();
                }
                else
                {
                    Alert("บันทึกข้อมูลไม่สำเร็จ!!" + resualt, this);
                }

            
        }

        private void BindGrid()
        {
            GridView_LIST.DataSource = dt;
            GridView_LIST.DataBind();
          
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_LIST.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        //protected void SaveData_Click(object sender, EventArgs e)
        //{
        //    string id = "";
        //    string curday = Convert.ToDateTime(txtDate.Text.ToString()).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
        //    DataTable dt = sv.CheckOrderKey(curday);
        //    if (dt.Rows.Count > 0)
        //    {
        //        id = dt.Rows[0][0].ToString();
        //    }
        //    else
        //    {

        //        sv.InsertDataHead(curday);
        //        dt = sv.CheckOrderKey(curday);
        //        if (dt.Rows.Count > 0)
        //        {
        //            id = dt.Rows[0][0].ToString();
        //        }

        //    }

        //    sv.DelDetailbyDate(curday);

        //    for (int i = 0; i <= GridView_LIST.Rows.Count - 1; i++)
        //    {

        //        Label lblID = (Label)GridView_LIST.Rows[i].FindControl("lblID");
        //        Label lblList = (Label)GridView_LIST.Rows[i].FindControl("lblList");
        //        Label lblPrice = (Label)GridView_LIST.Rows[i].FindControl("lblPrice");
        //        Label lblWeight = (Label)GridView_LIST.Rows[i].FindControl("lblWeight");
        //        Label lblTotalPrice = (Label)GridView_LIST.Rows[i].FindControl("lblTotalPrice");
        //        string resualt = sv.InsertData(id, lblID.Text, lblPrice.Text, lblWeight.Text, lblTotalPrice.Text);

        //    }
            
           
        //    Alert("บันทึกข้อมูลเสร็จเรียบร้อยครับ", this);
        //    txtPrice.Text = string.Empty;
        //    txtWeight.Text = string.Empty;            
        //    GetData();
        //}

        protected void GridView_LIST_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                totalprice += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Totalpricre"));
                weightship += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Weight"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = String.Format("TOTAL ");
                e.Row.Cells[3].Text = String.Format(weightship + " : Kg");
                e.Row.Cells[4].Text = String.Format("{0:c}", totalprice);
            }
        }
    
        private void GetData()
        {
            string date = Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));

            DataTable dt = sv.SelectOldDataToTBL(date, VegetableList.SelectedValue.ToString());
            GridView_LIST.DataSource = null;
            GridView_LIST.DataBind();
            if (dt.Rows.Count > 0)
            {
                disPlay.Visible = true;
                dt = sv.SelectOldDataToTBL(date, VegetableList.SelectedValue.ToString());
                GridView_LIST.DataSource = dt;
                GridView_LIST.DataBind();
             
            }
            else
            {
                disPlay.Visible = false;
            }
           
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
         
            txtDate.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            Calendar1.Visible = false;

            GridView_LIST.DataSource = null;
            GridView_LIST.DataBind();
            GetData();
        }

        protected void btnDate_Click(object sender, ImageClickEventArgs e)
        {

            if (Calendar1.Visible == false)
                Calendar1.Visible = true;
            else
                Calendar1.Visible = false;


        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            Button BtnEdit = (Button)sender;
            GridViewRow gr = BtnEdit.NamingContainer as GridViewRow;

            Label lblID = (Label)gr.FindControl("lblID");
            Label lblList = (Label)gr.FindControl("lblList");
            Label lblPrice = (Label)gr.FindControl("lblPrice");
            Label lblWeight = (Label)gr.FindControl("lblWeight");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");

            TextBox txtPrice = (TextBox)gr.FindControl("txtPrice");
            TextBox txtWeight = (TextBox)gr.FindControl("txtWeight");
           
            Button BtnSave = (Button)gr.FindControl("BtnSave");
            Button BtnCancel = (Button)gr.FindControl("BtnCancel");

            DropDownList ddlList= (DropDownList)gr.FindControl("ddlList");

            lblList.Visible = false;
            BtnEdit.Visible = false;
            BtnCancel.Visible = true;
            BtnSave.Visible = true;

            lblPrice.Visible = false;
            lblWeight.Visible = false;
           

            txtPrice.Visible = true;
            txtWeight.Visible = true;
           
            ddlList.Visible = true;


            DataTable dtList = new DataTable();
            dtList = sv.GetDataToTBL();
            ddlList.DataSource = dtList;
            ddlList.DataTextField = "PRODUCTNAME";
            ddlList.DataValueField = "PRODUCTKEY";
            try
            {
                ddlList.DataBind();
               
            }
            catch (ArgumentOutOfRangeException)
            {
                ddlList.SelectedValue = lblID.Text;
            }
        }
     
        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            Button BtnDelete = (Button)sender;
            GridViewRow gr = BtnDelete.NamingContainer as GridViewRow;

            Label lblDTID = (Label)gr.FindControl("lblDTID");
            Label lblID = (Label)gr.FindControl("lblID");
            Label lblList = (Label)gr.FindControl("lblList");
            Label lblPrice = (Label)gr.FindControl("lblPrice");
            Label lblWeight = (Label)gr.FindControl("lblWeight");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");


            if (lblDTID.Text.Trim()!="")
            {
                sv.DelDetail(lblDTID.Text.Trim());
            }
            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["DTID"].ToString() == lblDTID.Text && dr["ID"].ToString() == lblID.Text && dr["Price"].ToString() == lblPrice.Text)
            //    {
            //        dt.Rows.Remove(dr);

            //    }
            // }
            //dt.AcceptChanges();

            dt = new DataTable();
            MakeDataTable();
            for (int i = 0; i <= GridView_LIST.Rows.Count - 1; i++)
            {
                Label lblDTID1 = (Label)GridView_LIST.Rows[i].FindControl("lblDTID");
                Label lblID1 = (Label)GridView_LIST.Rows[i].FindControl("lblID");
                Label lblList1 = (Label)GridView_LIST.Rows[i].FindControl("lblList");
                Label lblPrice1 = (Label)GridView_LIST.Rows[i].FindControl("lblPrice");
                Label lblWeight1 = (Label)GridView_LIST.Rows[i].FindControl("lblWeight");
                Label lblTotalPrice1 = (Label)GridView_LIST.Rows[i].FindControl("lblTotalPrice");

                if(i != gr.RowIndex)
                {
                    dt.Rows.Add(new object[] { lblID1.Text, lblDTID1.Text, lblList1.Text, lblPrice1.Text, lblWeight1.Text, lblTotalPrice1.Text });
                }

                //if (lblDTID.Text != lblDTID1.Text && lblID.Text != lblID1.Text )
                //{
                //    dt.Rows.Add(new object[] { lblID1.Text, lblDTID1.Text, lblList1.Text, lblPrice1.Text, lblWeight1.Text, lblTotalPrice1.Text });
                //}
            }
            GridView_LIST.DataSource = null;
            GridView_LIST.DataBind();

            GridView_LIST.DataSource = dt;
            GridView_LIST.DataBind();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Button BtnCancel = (Button)sender;
            GridViewRow gr = BtnCancel.NamingContainer as GridViewRow;

            Label lblID = (Label)gr.FindControl("lblID");
            Label lblList = (Label)gr.FindControl("lblList");
            Label lblPrice = (Label)gr.FindControl("lblPrice");
            Label lblWeight = (Label)gr.FindControl("lblWeight");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");

            TextBox txtPrice = (TextBox)gr.FindControl("txtPrice");
            TextBox txtWeight = (TextBox)gr.FindControl("txtWeight");
           

            Button BtnSave = (Button)gr.FindControl("BtnSave");
            Button BtnEdit = (Button)gr.FindControl("BtnEdit");

            DropDownList ddlList = (DropDownList)gr.FindControl("ddlList");

            lblList.Visible = true;
            BtnEdit.Visible = true;
            BtnCancel.Visible = false;
            BtnSave.Visible = false;

            lblPrice.Visible = true;
            lblWeight.Visible = true;
        
            txtPrice.Visible = false;
            txtWeight.Visible = false;
            ddlList.Visible = false;
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {    
                Session.RemoveAll();
                Response.Redirect("LoginPage.aspx");
            
        }

        protected void BTNVIEW_Click(object sender, EventArgs e)
        {
            if(txtDate.Text.Trim()=="")
            {
                Alert("กรุณาเลือกวันที่ครับ!!", this);
                return;
            }
            GetData();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            Button BtnSave = (Button)sender;
            GridViewRow gr = BtnSave.NamingContainer as GridViewRow;

            Label lblDTID = (Label)gr.FindControl("lblDTID");
            Label lblID = (Label)gr.FindControl("lblID");
            Label lblList = (Label)gr.FindControl("lblList");
            Label lblPrice = (Label)gr.FindControl("lblPrice");
            Label lblWeight = (Label)gr.FindControl("lblWeight");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");

            TextBox txtPrice = (TextBox)gr.FindControl("txtPrice");
            TextBox txtWeight = (TextBox)gr.FindControl("txtWeight");
         
            Button BtnCancel = (Button)gr.FindControl("BtnCancel");
            Button BtnEdit = (Button)gr.FindControl("BtnEdit");

            DropDownList ddlList = (DropDownList)gr.FindControl("ddlList");


            string id = "";
            string curday = Convert.ToDateTime( txtDate.Text.ToString()).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            DataTable dt = sv.CheckOrderKey(curday);
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0][0].ToString();
            }
            else
            {
                AddToDataTable();
                sv.InsertDataHead(curday);
                dt = sv.CheckOrderKey(curday);
               
                if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0][0].ToString();
                   
                }
            }
            

            string resualt = "";
            if (lblDTID.Text == "")
            {
                 resualt = sv.InsertData(id, ddlList.SelectedValue, txtPrice.Text, txtWeight.Text, lblTotalPrice.Text);
                
            }
            else
            {
                resualt = sv.UpdateData(lblDTID.Text, ddlList.SelectedValue, txtPrice.Text, txtWeight.Text, lblTotalPrice.Text);

            }

            if(resualt=="OK")
            {
                Alert("บันทึกข้อมูลสำเร็จครับ!!", this);
                GetData();
            }
            else
            {
                Alert("บันทึกข้อมูลไม่สำเร็จ!!"+ resualt, this);
            }

        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            TextBox txtPrice = (TextBox)sender;
            GridViewRow gr = txtPrice.NamingContainer as GridViewRow;

            TextBox txtWeight = (TextBox)gr.FindControl("txtWeight");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");
           
            if (txtWeight.Text.Trim() != "" && txtWeight.Text.Trim() != "")
            {
                double price = double.Parse(txtPrice.Text);
                double weight = double.Parse(txtWeight.Text);
                double amount = price * weight;
                lblTotalPrice.Text = String.Format("{0:0.00}", amount);
            }
        }

        protected void Backbutton_Click(object sender, ImageClickEventArgs e)
        {
            this.Response.Redirect("Selectdate.aspx");
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string date = Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));
            String _RPTName = "ReportVegetablelist";
            DataSet DS = new DataSet();
            DS = sv.RPTData(date, VegetableList.SelectedValue.ToString(), _RPTName);

            if (DS.Tables[0].Rows.Count != 0)
            {
                String _P = "";
                _P = "RPT=" + _RPTName;
                _P += "&PDate=" + txtDate.Text;
                _P += "&PUSER=" + LblUser.Text;

                Session["DATA"] = DS;

                ResponseHelper.Redirect("ViewReport.aspx?ID=" + new Encryption64().Encrypt(_P, "!#$a54?3"), "_blank", "menubar=0,width=800,height=600,resizable=yes");
            }
            else
            {
                Alert("ไม่พบข้อมูลครับ!!", this);
            }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {

            string datejob = e.Day.Date.ToString("yyyyMMdd ", CultureInfo.CreateSpecificCulture("en-US"));
            DataTable dt = sv.CheckDateJob(datejob);

            if (dt.Rows.Count > 0)
            {
                System.Web.UI.WebControls.Image image;
                image = new System.Web.UI.WebControls.Image();
                image.ImageUrl = "Images/bullet_ball_glass_green.png";
                image.Width = 10;
                e.Cell.Controls.Add(image);
                //e.Cell.BackColor = Color.Firebrick;
            }
        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            TextBox txtWeight = (TextBox)sender;
            GridViewRow gr = txtWeight.NamingContainer as GridViewRow;

            TextBox txtPrice = (TextBox)gr.FindControl("txtPrice");
            Label lblTotalPrice = (Label)gr.FindControl("lblTotalPrice");
           

            if (txtWeight.Text.Trim() != "" && txtWeight.Text.Trim() != "")
            {
                double price = double.Parse(txtPrice.Text);
                double weight = double.Parse(txtWeight.Text);
                double amount = price * weight;
                lblTotalPrice.Text = String.Format("{0:0.00}", amount);
            }
        }


    }
}