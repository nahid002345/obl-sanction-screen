using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBLSCREENINGModel;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Xml;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Security;
using System.Text.RegularExpressions;

public partial class XMLFileUpload : BasePage
{
    public class InsertTableInfo
    {
        public string TableName = string.Empty;
        public bool IsInserted = true;
        public int InsertedRow = 0;

        public string GetInfoMsg()
        {
            return "INSERTED TABLE NAME : <b> " + TableName + " </b> -> TOTAL INSERTED : " + InsertedRow.ToString();
        }
    }

    private static double progressPercentage = 0;

    public List<InsertTableInfo> oInsertTableInfo;
    public string[] sameNameTblList = new string[] { "nationality" };
    //2D Array for ChangeTableName for example
    //For OFAC changeTblNameList[0][0]
    //For UN changeTblNameList[0][1]
    //2nd Index number indicate Type
    public string[,] changeTblNameList = new string[,]
	{
	    {"nationality", "NATIONALITYUNS"}        
	};

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CheckEndSession())
                Response.Redirect("~/LogIn.aspx");
            else
            {
                progressPercentage = 0;
                oInsertTableInfo = new List<InsertTableInfo>();
            }
        }

    }




    private void ClearControl()
    {

    }

    void SetTheProgress(HtmlGenericControl bar, string value)
    {
        bar.Attributes.Add("style", string.Format("width:{0};", value + "%"));
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "UpdateProgressBar("+value+")", true);
        //ClientScript.RegisterStartupScript(this.GetType(), "", "UpdateProgressBar(" + value + ")",true);
    }


    protected void btnUploadFile_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(inputFileUpload.Value))
        {
            string fileExtension = Path.GetExtension(inputFileUpload.PostedFile.FileName);

            string filePath = Server.MapPath("~/UploadedXML/") + inputFileUpload.PostedFile.FileName;
            if (File.Exists(@filePath))
            {
                File.Delete(@filePath);

            }
            inputFileUpload.PostedFile.SaveAs(filePath);

            if (fileExtension.Equals(".xml"))
            {
                try
                {
                    DataSet dsXMLData = new DataSet();
                    //XmlReader xmlFile;
                    XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        progressPercentage = 0;
                        using (XmlReader xmlFile = XmlReader.Create(new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1"))))
                        {
                            //xmlFile = XmlReader.Create(new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1")));
                            FILEUPLOADLOG oFILEUPLOADLOG = new FILEUPLOADLOG();
                            dsXMLData.ReadXml(xmlFile);
                            int totalDataCount = DataSetTotalDataCount(dsXMLData);
                            int tableRowCount = 0;
                            double percentage = 0;
                            oInsertTableInfo = new List<InsertTableInfo>();
                            string tableNames = dsXMLData.Tables.ToString();
                            foreach (DataTable dtble in dsXMLData.Tables)
                            {
                                
                                if (sameNameTblList.Contains(dtble.TableName.ToLower().Trim()))
                                {
                                    dtble.TableName = changeTblNameList[Array.FindIndex(sameNameTblList, t => t.Contains(dtble.TableName.ToLower().Trim())), ddlType.SelectedIndex - 1];
                                }
                                if (!IsTableExist(dtble.TableName))
                                    CreateDBTable(dtble);
                                InsertBulkDataIntoTable(dtble);
                                tableRowCount = tableRowCount + dtble.Rows.Count;
                                percentage = (tableRowCount * 100 / totalDataCount);
                                progressPercentage = percentage;
                                //SetTheProgress(bar1, percentage.ToString());
                            }
                            oFILEUPLOADLOG.FileName = inputFileUpload.PostedFile.FileName;
                            oFILEUPLOADLOG.FilePath = filePath;
                            oFILEUPLOADLOG.FileType = ddlType.SelectedItem.Text;
                            oFILEUPLOADLOG.FileUploadedBy = Session["CurrentUser"].ToString().Trim();
                            oFILEUPLOADLOG.FileUploadedOn = DateTime.Now;
                            oFILEUPLOADLOG.FileTotalTableNo = dsXMLData.Tables.Count;
                            oFILEUPLOADLOG.FileTotalUploadedData = tableRowCount;
                            oFILEUPLOADLOG.CreatedBy = Session["CurrentUser"].ToString().Trim();
                            oFILEUPLOADLOG.CreatedOn = DateTime.Now;
                            obDBCtxt.FILEUPLOADLOGs.AddObject(oFILEUPLOADLOG);
                            obDBCtxt.SaveChanges();
                            xmlFile.Close();
                        }

                        string Msg = string.Empty;
                        string unSuccessMsg = string.Empty;
                        foreach (InsertTableInfo oTableInfo in oInsertTableInfo.Where(t=>t.IsInserted).OrderBy(t => t.TableName))
                        {
                            Msg += oTableInfo.GetInfoMsg() + ".<br />";
                        }
                        Msg += "TOTAL TABLE INSERTED : <b> " + oInsertTableInfo.Where(t => t.IsInserted).ToList().Count + " </b> & TOTAL DATA INSERTED : <b>" + oInsertTableInfo.Where(t => t.IsInserted).Sum(t => t.InsertedRow).ToString() + "</b>";
                        lblSuccessMsg.Text = Msg;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowSuccessBox()", true);

                        if (oInsertTableInfo.Where(t => !t.IsInserted).ToList().Count > 0)
                        {
                            foreach (InsertTableInfo oTableInfo in oInsertTableInfo.Where(t => !t.IsInserted).OrderBy(t => t.TableName))
                            {
                                unSuccessMsg += oTableInfo.GetInfoMsg() + ".<br />";
                            }                            
                            lblMsg.Text = unSuccessMsg;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
                        }
                    }
                    //btnUploadFile.Enabled = false;
                }
                catch (Exception ex)
                {
                    var line = new StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                    string Msg = (ex.InnerException == null) ? ex.Message + "LINE: " + line : ex.InnerException.Message;
                    foreach (InsertTableInfo oTableInfo in oInsertTableInfo.Where(t => !t.IsInserted).OrderBy(t => t.TableName))
                    {
                        Msg += oTableInfo.GetInfoMsg() + ".<br />";
                    }
                    Msg += "TOTAL TABLE INSERTED : <b> " + oInsertTableInfo.Where(t => !t.IsInserted).ToList().Count + " </b> & TOTAL DATA INSERTED : <b>" + oInsertTableInfo.Where(t => !t.IsInserted).Sum(t => t.InsertedRow).ToString() + "</b>";
                    lblMsg.Text = Msg;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
                    if (oInsertTableInfo.Where(t=>t.IsInserted).ToList().Count > 0)
                    {
                        string successMsg = "TOTAL TABLE INSERTED : <b> " + oInsertTableInfo.Where(t => t.IsInserted).ToList().Count + " </b> & TOTAL DATA INSERTED : <b>" + oInsertTableInfo.Where(t => t.IsInserted).Sum(t => t.InsertedRow).ToString() + "</b>";
                        lblSuccessMsg.Text = successMsg;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowSuccessBox()", true);
                    }

                }
            }
        }
    }

    private int DataSetTotalDataCount(DataSet dt)
    {
        int tableRowcount = 0;

        foreach (DataTable dtble in dt.Tables)
        {
            tableRowcount += dtble.Rows.Count;
        }

        return tableRowcount;
    }

    [WebMethod]
    public static double GetData()
    {
        return progressPercentage;
    }

    private bool IsTableExist(string oTableName)
    {
        bool IsExist = false;
        string oSqlQuery = "IF  EXISTS (SELECT * FROM sys.objects " +
                            "WHERE object_id = OBJECT_ID(N'[dbo].[" + oTableName + "]') AND type in (N'U')) SELECT 1 ELSE SELECT 0 ";
        int tt = obDBCtxt.ExecuteStoreQuery<int>(@"IF EXISTS (SELECT * FROM sys.tables WHERE name = '" + oTableName + "') SELECT 1 ELSE SELECT 0").SingleOrDefault();
        IsExist = Convert.ToBoolean(obDBCtxt.ExecuteStoreQuery<int>(@"IF EXISTS (SELECT * FROM sys.tables WHERE name = '"+oTableName+"') SELECT 1 ELSE SELECT 0").SingleOrDefault());
        return IsExist;
    }

    private void CreateDBTable(DataTable dtInfo)
    {
        try
        {
            string oExistSqlQuery = "IF  NOT EXISTS (SELECT * FROM sys.objects " +
                                "WHERE object_id = OBJECT_ID(N'[dbo].[" + dtInfo.TableName + "]') AND type in (N'U')) " +
                                "BEGIN ";
            DataColumn[] pkey = dtInfo.PrimaryKey;
            string pKeyStr = string.Empty;
            string oCreatesqlQuery = string.Empty;
            if (pkey.Count() > 0 )
            {
                pKeyStr = pkey[0].ColumnName;
                
            }
            else
            {
                pKeyStr = dtInfo.Columns[dtInfo.Columns.Count - 1].ColumnName;
                
            }
            oCreatesqlQuery = "CREATE TABLE " + dtInfo.TableName + "(";
            //string oCreatesqlQuery = "CREATE TABLE " + dtInfo.TableName + "( [ID] [bigint] IDENTITY(1,1) NOT NULL,";
            for (int i = 0; i < dtInfo.Columns.Count; i++)
            {
                oCreatesqlQuery += "\n" + dtInfo.Columns[i].ColumnName;
                if (dtInfo.Columns[i].DataType.ToString().Contains("System.Int32"))
                    oCreatesqlQuery += " int ";
                else if (dtInfo.Columns[i].DataType.ToString().Contains("System.DateTime"))
                    oCreatesqlQuery += " datetime NULL";
                else if (dtInfo.Columns[i].DataType.ToString().Contains("System.String"))
                    oCreatesqlQuery += " nvarchar(max) ";
                else if (dtInfo.Columns[i].DataType.ToString().Contains("System.Double"))
                    oCreatesqlQuery += " double";
                else
                    oCreatesqlQuery += " nvarchar(max) ";

                if (dtInfo.Columns[i].ColumnName != pKeyStr && (dtInfo.Columns[i].DataType.ToString().Contains("System.Double") ||dtInfo.Columns[i].DataType.ToString().Contains("System.DateTime") ||dtInfo.Columns[i].DataType.ToString().Contains("System.Int32")) )
                    oCreatesqlQuery += " NULL";
                if (dtInfo.Columns[i].ColumnName != pKeyStr && (dtInfo.Columns[i].DataType.ToString().Contains("System.String")))
                    oCreatesqlQuery += " COLLATE SQL_Latin1_General_CP1_CI_AS NULL ";

                //if (!dtInfo.Columns[i].AllowDBNull)
                //    sqlsc += " NOT NULL ";
                if (i != (dtInfo.Columns.Count - 1)) oCreatesqlQuery += " , \n";
            }
            oCreatesqlQuery += " ) END";
            string oPkQuery = "CONSTRAINT [PK_" + dtInfo.TableName + "] PRIMARY KEY CLUSTERED ( " + pKeyStr + " ) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY] ) ON [PRIMARY] END";
            //string oCreateTableQuery = oExistSqlQuery + oCreatesqlQuery + oPkQuery;
            string oCreateTableQuery = oExistSqlQuery + oCreatesqlQuery;

            bool IsCreated = Convert.ToBoolean(obDBCtxt.ExecuteStoreCommand(oCreateTableQuery));
        }
        catch (Exception ex)
        {
        }
    }

    private void InsertDataIntoTable(DataTable dtInfo)
    {
        try
        {
            
            Regex re = new Regex("[;\\/:*?\"<>|&']");
            InsertTableInfo oTableInfo = new InsertTableInfo();
            string oInsertQuery = "DELETE FROM " + dtInfo.TableName + " \n";
            foreach (DataRow drInfo in dtInfo.Rows)
            {
                oInsertQuery += " INSERT INTO  " + dtInfo.TableName + " (";
                for (int i = 0; i < dtInfo.Columns.Count; i++)
                {
                    oInsertQuery += dtInfo.Columns[i].ColumnName;
                    if (i != (dtInfo.Columns.Count - 1)) oInsertQuery += " , \n";
                }
                oInsertQuery += " ) VALUES (";
                for (int i = 0; i < dtInfo.Columns.Count; i++)
                {
                    if (dtInfo.Columns[i].DataType.ToString().Contains("System.Int32") || dtInfo.Columns[i].DataType.ToString().Contains("System.Double"))
                        oInsertQuery += string.IsNullOrEmpty(drInfo[i].ToString()) ? " NULL " : drInfo[i].ToString();
                    else if (!string.IsNullOrEmpty(drInfo[i].ToString()) && drInfo[i].ToString().Length > 10 && dtInfo.Columns[i].DataType.ToString().Contains("System.String") && dtInfo.Columns[i].ColumnName.ToLower().Contains("date"))
                        oInsertQuery += "'" + re.Replace(drInfo[i].ToString().Substring(0, 10), "") + "'";
                    else if (dtInfo.Columns[i].DataType.ToString().Contains("System.String"))
                        oInsertQuery += "'" + re.Replace(drInfo[i].ToString(), "") + "'";
                    else if (dtInfo.Columns[i].DataType.ToString().Contains("System.DateTime"))
                        oInsertQuery += string.IsNullOrEmpty(drInfo[i].ToString()) ? " NULL " : "'" + re.Replace(drInfo[i].ToString().Substring(0, 10), "") + "'";
                    else
                        oInsertQuery += string.IsNullOrEmpty(drInfo[i].ToString()) ? " NULL " : "'" + drInfo[i].ToString() + "'";

                    if (i != (dtInfo.Columns.Count - 1)) oInsertQuery += " , \n";
                }
                oInsertQuery += " )";
            }

            bool IsCreated = Convert.ToBoolean(obDBCtxt.ExecuteStoreCommand(oInsertQuery));
            if (IsCreated)
            {
                oTableInfo.TableName = dtInfo.TableName;
                oTableInfo.IsInserted = true;
                oTableInfo.InsertedRow = dtInfo.Rows.Count;
                oInsertTableInfo.Add(oTableInfo);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void InsertBulkDataIntoTable(DataTable dtInfo)
    {
        try
        {
            DataColumn[] pkey = dtInfo.PrimaryKey;
            string pKeyStr = string.Empty;
            string oCreatesqlQuery = string.Empty;
            if (pkey.Count() > 0 )
            {
                pKeyStr = pkey[0].ColumnName;                
            }
            else
            {
                pKeyStr = dtInfo.Columns[dtInfo.Columns.Count - 1].ColumnName;                
            }
            Regex re = new Regex("[;\\/:*?\"<>|&']");
            InsertTableInfo oTableInfo = new InsertTableInfo();
            string oInsertQuery = "DELETE FROM " + dtInfo.TableName + " \n";            
            bool IsCreated = Convert.ToBoolean(obDBCtxt.ExecuteStoreCommand(oInsertQuery));            
            string[] columnNames = dtInfo.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            //DataTable temp=RemoveDuplicateRows(dtInfo, pKeyStr);
            //IsCreated = InsertBulkDataToDB(RemoveDuplicateRows(dtInfo, pKeyStr));
            IsCreated = InsertBulkDataToDB(dtInfo);
            if (IsCreated)
            {
                oTableInfo.TableName = dtInfo.TableName;
                oTableInfo.IsInserted = true;
                oTableInfo.InsertedRow = dtInfo.Rows.Count;
                oInsertTableInfo.Add(oTableInfo);
            }
            else
            {
                InsertDataIntoTable(dtInfo.DefaultView.ToTable(true, columnNames));
                oTableInfo.TableName = dtInfo.TableName;
                oTableInfo.IsInserted = false;
                oTableInfo.InsertedRow = dtInfo.Rows.Count;
                oInsertTableInfo.Add(oTableInfo);
            }
            
        }
        catch (Exception ex)
        {

        }
    }
}