using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace VGTB_SHOP_LIST
{
    public class ClassDB
    {
        private SqlCommand Com;
        private SqlConnection Con;
        private String SQL;
        private String ORDBName = "TAF_TAF";

        public void New()
        {
            this.Com = new SqlCommand();
            this.Con = new SqlConnection();
            this.Con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionDBORDER"].ToString());

        }
        public void ExecuteNonQuery(String SQL)
        {
            New();
            try
            {
                this.Con.Open();
                this.Com.Connection = this.Con;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = SQL;
                this.Com.CommandTimeout = 0;
                this.Com.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                this.Con.Close();
                this.Com.Dispose();
            }
            this.Con.Close();
            this.Com.Dispose();
        }

        public DataTable ExecuteReaderDataTable(String SQL)
        {
            New();
            DataTable table = new DataTable();
            try
            {
                this.Con.Open();
                this.Com.Connection = this.Con;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = SQL;
                this.Com.CommandTimeout = 0;
                table.Load(this.Com.ExecuteReader());
            }
            catch (SqlException e)
            {
                this.Con.Close();
                this.Com.Dispose();
                return table;
            }
            this.Con.Close();
            this.Com.Dispose();
            return table;
        }

        public DataSet ExecuteReaderDataSet(String SQL, String DsName)
        {
            New();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                this.Con.Open();
                this.Com.Connection = this.Con;
                this.Com.CommandType = CommandType.Text;
                this.Com.CommandText = SQL;
                this.Com.CommandTimeout = 0;
                da = new SqlDataAdapter(Com);
                da.Fill(ds, DsName);
            }
            catch (SqlException e)
            {
                this.Con.Close();
                this.Com.Dispose();
                return ds;
            }
            this.Con.Close();
            this.Com.Dispose();
            return ds;
        }

        public Boolean CheckLogin(String _User, String _Pwd)
        {
            DataSet ds = new DataSet();
            String strSQL;
            //strSQL = "SELECT * FROM TAF_TAF.[TAF].[CMNUSR] WHERE UPPER(RTRIM(JUUSID))='" + _User.ToUpper().Trim() + "' AND CASE WHEN UPPER(RTRIM(JULSID))='' THEN UPPER(RTRIM(JUUSID)) ELSE UPPER(RTRIM(JULSID)) END='" + _Pwd.ToUpper().Trim() + "'";
            //strSQL = "SELECT JUUSID,JUTX40 FROM MVXJDTA.CMNUSR (NOLOCK) WHERE UPPER(RTRIM(JUUSID))='" + _User.ToUpper().Trim() + "' AND CASE WHEN UPPER(RTRIM(JULSID))='' THEN UPPER(RTRIM(JUUSID)) ELSE UPPER(RTRIM(JULSID)) END='" + _Pwd.ToUpper().Trim() + "'  ";
            //strSQL += " UNION ";
            strSQL = " SELECT JUUSID,JUTX40 FROM TAF_TAF TAF.TAFUSR (NOLOCK) WHERE UPPER(RTRIM(JUUSID))='" + _User.ToUpper().Trim() + "' AND CASE WHEN UPPER(RTRIM(JULSID))='' THEN UPPER(RTRIM(JUUSID)) ELSE UPPER(RTRIM(JULSID)) END='" + _Pwd.ToUpper().Trim() + "'";

            ds = ExecuteReaderDataSet(strSQL, "GET");
            if (ds.Tables["GET"].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
     
        public DataTable CheckDateJob(string date)
        {
            SQL = "SELECT * FROM  TALAADTHAI.ORDERHEAD (NOLOCK) WHERE ORDERDATE = " + date;
            return ExecuteReaderDataTable(SQL);
        }

        public DataTable GetDataToTBL()
        {
            SQL = "SELECT PRODUCTKEY,PRODUCTNAME FROM TALAADTHAI.ORDERMASTER (NOLOCK) ORDER BY PRODUCTKEY ";
            return ExecuteReaderDataTable(SQL);
        }

        public DataTable SelectOldDataToTBL(string ORDERDATE, string ProductID)
        {
            SQL = @"SELECT B.ORDERKEY,B.DETAILKEY,B.PRODUCTKEY AS ID,B.PRICE AS Price,B.WEIGHT AS Weight,B.TOTALPRICE AS Totalpricre,C.PRODUCTNAME AS ProductList
                    FROM TALAADTHAI.ORDERHEAD AS A (NOLOCK)
                    INNER JOIN TALAADTHAI.ORDERDETAIL AS B (NOLOCK) ON A.ORDERKEY=B.ORDERKEY
                    INNER JOIN TALAADTHAI.ORDERMASTER AS C (NOLOCK) ON  B.PRODUCTKEY=C.PRODUCTKEY
                    WHERE ORDERDATE=" + ORDERDATE + @"";
            if (ProductID != "0")
                SQL += " AND B.PRODUCTKEY=" + ProductID;
            SQL += " ORDER BY DETAILKEY ";
            return ExecuteReaderDataTable(SQL);

        }
        public DataTable SelectReportMonthly()
        {
            SQL = @"SELECT B.PRODUCTKEY AS ID,C.PRODUCTNAME AS ProductList                   
					,SUM(B.[WEIGHT]) AS TOTALWEIGHT
                    , SUM (B.TOTALPRICE) AS TOTALPRICE
                    FROM TALAADTHAI.ORDERHEAD AS A(NOLOCK)
                    INNER JOIN TALAADTHAI.ORDERDETAIL AS B(NOLOCK) ON A.ORDERKEY = B.ORDERKEY
                    INNER JOIN TALAADTHAI.ORDERMASTER AS C(NOLOCK) ON B.PRODUCTKEY = C.PRODUCTKEY
                      GROUP BY B.PRODUCTKEY,C.PRODUCTNAME";

            return ExecuteReaderDataTable(SQL);
        }
        public DataSet SelectReportByMonth(string Year,string Month,string RPTNAME)
        {
            SQL = @"SELECT B.PRODUCTKEY AS ID,C.PRODUCTNAME AS ProductList                   
					,SUM(B.[WEIGHT]) AS TOTALWEIGHT
                    , SUM (B.TOTALPRICE) AS TOTALPRICE
                    FROM TALAADTHAI.ORDERHEAD AS A(NOLOCK)
                    INNER JOIN TALAADTHAI.ORDERDETAIL AS B(NOLOCK) ON A.ORDERKEY = B.ORDERKEY
                    INNER JOIN TALAADTHAI.ORDERMASTER AS C(NOLOCK) ON B.PRODUCTKEY = C.PRODUCTKEY
                    WHERE LEFT(A.ORDERDATE,6)=" + Year+ Month + @"
                      GROUP BY B.PRODUCTKEY,C.PRODUCTNAME";

            return ExecuteReaderDataSet(SQL, RPTNAME);
        }    
        public DataSet RPTData (string ORDERDATE, string ProductID,string RPT)
        {
            SQL = @"SELECT A.ORDERDATE,CONVERT(NVARCHAR, CONVERT(DATETIME, CONVERT(NVARCHAR, A.ORDERDATE)), 103) AS ORDERDATE2, B.ORDERKEY,B.DETAILKEY,B.PRODUCTKEY AS ID,B.PRICE AS Price,B.WEIGHT AS Weight,B.TOTALPRICE AS Totalpricre,C.PRODUCTNAME AS ProductList
                    FROM  TALAADTHAI.ORDERHEAD AS A(NOLOCK)
                    INNER JOIN TALAADTHAI.ORDERDETAIL AS B(NOLOCK) ON A.ORDERKEY = B.ORDERKEY
                    INNER JOIN TALAADTHAI.ORDERMASTER AS C(NOLOCK) ON B.PRODUCTKEY = C.PRODUCTKEY
                   WHERE ORDERDATE=" + ORDERDATE + @"";
            if (ProductID != "0")
                SQL += " AND B.PRODUCTKEY=" + ProductID;
            SQL += " ORDER BY DETAILKEY ";

            return ExecuteReaderDataSet(SQL, RPT);
        }

        public void DelDetail(string DTKEY)
        {
            SQL = "DELETE FROM TALAADTHAI.ORDERDETAIL WHERE DETAILKEY=" + DTKEY;
            this.ExecuteNonQuery(SQL);

        }
        public void DelDetailbyDate(string ORDERDATE)
        {
            SQL = "DELETE FROM TALAADTHAI.ORDERDETAIL WHERE ORDERKEY IN (SELECT A.ORDERKEY FROM  dbo.ORDERHEAD AS A (NOLOCK) WHERE ORDERDATE=" + ORDERDATE + @")";
            this.ExecuteNonQuery(SQL);
        }

        public DataTable CheckOrderKey(string Orderdate)
        {
            string sqlquery = "SELECT ORDERKEY FROM  TALAADTHAI.ORDERHEAD (NOLOCK) WHERE[ORDERDATE] = " + Orderdate;
             return ExecuteReaderDataTable(sqlquery);
        }

        public DataTable GetmaxId()
        {   
            string sqlquery = @"  
            SELECT ISNULL (MAX(ORDERKEY),0)+1 AS  ORDERKEY FROM  TALAADTHAI.ORDERHEAD (NOLOCK)";
            return ExecuteReaderDataTable(sqlquery);
        }

        public string InsertDataHead( string DateId)
        {

            string resualt = "";
            try
            {
                string sqlquery = @"insert into [TALAADTHAI].[ORDERHEAD] (ORDERKEY,ORDERDATE) 
                values ((Select ISNULL (MAX(ORDERKEY),0)+1 AS  ORDERKEY from  TALAADTHAI.ORDERHEAD (NOLOCK)),'" + DateId + "')";

                sqlquery += "";
                ExecuteNonQuery(sqlquery);
                resualt = "OK";
            }
            catch (Exception ex)
            {
                resualt = "ERROR " + ex.ToString();
            }
            return resualt;
        }
            public string InsertData(string Orid, string Proid, string Price, string Weight, string Totalprice)
            
            {
            
            string resualt = "";
            try
            {               
                string sqlquery = @"insert into [TALAADTHAI].[ORDERDETAIL] (ORDERKEY,PRODUCTKEY,PRICE,WEIGHT,TOTALPRICE ) 
                values (" + Orid+ ",N'" + Proid + "','" + Price+"','"+ Weight + "','"+ Totalprice + @"')";

                sqlquery += "";
            ExecuteNonQuery(sqlquery);
                resualt = "OK";
            }
            catch(Exception ex)
            {
                resualt = "ERROR "+ ex.ToString();
            }
            return resualt;

            }
        public string UpdateData(string DTKey,string ProId,string Price, string Weight, string Totalprice)

        {
            string resualt = "";
            try
            {
                string sqlquery = @"UPDATE [TALAADTHAI].[ORDERDETAIL] SET PRODUCTKEY=N'" + ProId + "',PRICE=" + Price + ",WEIGHT=" + Weight + ",TOTALPRICE=" + Totalprice + "  WHERE DETAILKEY=" + DTKey;

                ExecuteNonQuery(sqlquery);
                resualt = "OK";
            }
            catch (Exception ex)
            {
                resualt = "ERROR " + ex.ToString();
            }
            return resualt;

        }
    }
}