using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.Security;
using System.IO;
using System.Net;
using System.Data;
using OBLSCREENINGModel;

using System.Data.SqlClient;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Net.Sockets;
using System.Transactions;
using System.Collections;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{

    public static OBLSCREENINGEntities obDBCtxt = new OBLSCREENINGEntities();
    
    public BasePage()
    {

    }
    public bool CheckEndSession()
    {
        if (Session.Count == 0)
            return true;
        else return false;
    }

    public List<SEARCHLIST_Result> SearchSanctionListByName(string SearchName, string Address, string Country,string DOB, Int32 Percentage)
    {       
        List<SEARCHLIST_Result> oSEARCHLIST_Result = null;
        oSEARCHLIST_Result = obDBCtxt.SEARCHLIST(SearchName,Address,Country,DOB, Percentage).ToList();

        return oSEARCHLIST_Result;
    }

    [WebMethod]
    public static string LogOutN()
    {

        HttpContext.Current.Session[UIUtility.CurrentClientUserId] = null;
        HttpContext.Current.Session[UIUtility.CurrentClientId] = null;
        HttpContext.Current.Session.Clear();
        return "Valid";
    }


    public static IPAddress GetIp()
    {
        string ipString;
        //  ipString = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
        {
            ipString = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        else
        {
            ipString = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();
        }

        IPAddress result;
        if (!IPAddress.TryParse(ipString, out result))
        {
            result = IPAddress.None;
        }

        return result;
    }

    public int SanctionCustomerList(string BranchCode)
    {
        int result = 0;        
        result = obDBCtxt.EXISTINGSANCCUSTINFOes.Where(t => t.LOCAL_BRANCH_CODE == BranchCode).ToList().Count;
        return result;
    }

    public int PendingReferList(string EmployeeId)
    {
        int result = 0;
        result = obDBCtxt.OSREFERINFOes.Where(t => t.REFERTO == EmployeeId).OrderByDescending(t=>t.ID).ToList().Count;
        return result;
    }

    public string GetReferUserType(string EmployeeID)
    {
        string result = string.Empty;
        OSUSER oOSUSERType = obDBCtxt.OSUSERs.FirstOrDefault(t => t.EMPLOYEEID == EmployeeID && t.ISACTIVE);
        if(oOSUSERType != null)
            result = oOSUSERType.USERTYPE.ToString();
        return result;
    }

    public bool InsertBulkDataToDB(DataTable objT)
    {
        object obTmp;
        bool result = true;
        if (objT != null && objT.Rows.Count > 0)
        {
            obTmp = objT.Rows[0];
            EntityConnection dbConn = (EntityConnection)obDBCtxt.Connection;
            if (dbConn.State == ConnectionState.Closed) dbConn.Open();
            try
            {
                
                using (TransactionScope oTS = new TransactionScope())
                {
                    SqlBulkCopy bulkInsert = new SqlBulkCopy((SqlConnection)dbConn.StoreConnection);
                    bulkInsert.DestinationTableName = objT.TableName;
                    bulkInsert.WriteToServer(objT);//GetTbl( objT));//.AsDataReader());

                    oTS.Complete();
                    dbConn.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                dbConn.Close();
                result = false;
            }
        }
        else 
            result=false;
        return result;
    }

    public DataTable RemoveDuplicateRows(DataTable table, string DistinctColumn)
    {
        try
        {
            ArrayList UniqueRecords = new ArrayList();
            ArrayList DuplicateRecords = new ArrayList();
            foreach (DataRow dRow in table.Rows)
            {
                if (UniqueRecords.Contains(dRow[DistinctColumn]) || dRow[DistinctColumn] == "" || dRow[DistinctColumn] == null)
                    DuplicateRecords.Add(dRow);
                else
                    UniqueRecords.Add(dRow[DistinctColumn]);
            }
            foreach (DataRow dRow in DuplicateRecords)
            {
                table.Rows.Remove(dRow);
            }
            return table;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
