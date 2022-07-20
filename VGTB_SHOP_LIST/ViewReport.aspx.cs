using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace VGTB_SHOP_LIST
{
    public partial class ViewReport : System.Web.UI.Page
    {

        private DataSet ds = new DataSet();
        private ReportDocument rpt = new ReportDocument();
        private Encryption64 Enc = new Encryption64();
        private string _Svr = "10.0.6.17";
        private string _Usr = "REPUSR";
        private string _pwd = "repusr";
        private string _DB = "TAF_TAF";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            String _URL = "Default.aspx";
            String RPTName;
            String _ID = Request.QueryString["ID"];
            Dictionary<String, String> _Dic = Enc.EncrytURL(_ID, "!#$a54?3");
            String _ValX = "";
            String _SSID = "";
            if (_Dic.TryGetValue("SESDTVAL", out _ValX))
            {
                _SSID = _Dic["SESDTVAL"];
            }
            else
            {
                _SSID = "DATA";
            }
            if (Session[_SSID] != null)
            {
                ds = (DataSet)Session[_SSID];
            }
            else
            {
                Response.Redirect(_URL);
            }
            RPTName = _Dic["RPT"];
            List<String> list = new List<String>(_Dic.Keys);

            String Xpath = "";
            foreach (String Str in list)
            {
                if (Str == "XDIR")
                {
                    Xpath = _Dic["XDIR"];
                }
            }

            rpt.Load(Server.MapPath("Report/" + RPTName.Trim() + ".rpt"));
            rpt.SetDatabaseLogon(_Usr, _pwd, _Svr, _DB);

            ParameterFields paramFields = new ParameterFields();
            ///****** NEW CODE ********
            ///Dim list As New List(Of String)(_Dic.Keys)
            foreach (String Str in list)
            {
                switch (Str.Substring(0, 1))
                {
                    case "F":
                    case "f":
                        rpt.DataDefinition.FormulaFields[Str].Text = "'" + Server.UrlDecode(_Dic[Str]) + "'";
                        break;
                    case "P":
                    case "p":
                        ParameterField paramField = new ParameterField();
                        ParameterValues myValue = new ParameterValues();
                        paramField.ParameterFieldName = Str;
                        myValue.AddValue(Server.UrlDecode(_Dic[Str]));
                        paramField.CurrentValues = myValue;
                        rpt.DataDefinition.ParameterFields[Str].ApplyCurrentValues(myValue);
                        paramFields.Add(paramField);
                        break;
                    default:
                        break;
                }
            }

            //****** NEW CODE ********
            rpt.SetDataSource(ds.Tables[RPTName]);
            //rpt.Subreports[0].SetDataSource(ds2.Tables[0]);
            CrystalReportViewer1.ParameterFieldInfo = paramFields;
            CrystalReportViewer1.ReportSource = rpt;

            ExportOptions exp;
            ExportRequestContext req;
            System.IO.Stream st;
            Byte[] b = null;
            Page pg;
            pg = CrystalReportViewer1.Page;
            exp = new ExportOptions();
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            exp.FormatOptions = new PdfRtfWordFormatOptions();
            CrystalDecisions.Shared.PdfFormatOptions pdfOptions = new PdfFormatOptions();
            pdfOptions.CreateBookmarksFromGroupTree = true;

            exp.ExportFormatOptions = pdfOptions;

            req = new ExportRequestContext();
            req.ExportInfo = exp;
            req.ReportState.ParameterFieldInfo = CrystalReportViewer1.ParameterFieldInfo;
            st = rpt.FormatEngine.ExportToStream(req);
            pg.Response.ClearHeaders();
            pg.Response.ClearContent();
            pg.Response.ContentType = "application/pdf";
            b = new byte[st.Length + 1];
            st.Read(b, 0, (int)st.Length);
            pg.Response.BinaryWrite(b);
            pg.Response.End();
        }
        protected void CrystalReportViewer1_Unload(Object sender, EventArgs e)
        {
            rpt.Dispose();
        }
    }
}