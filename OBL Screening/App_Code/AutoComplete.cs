
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OBLSCREENINGModel;
/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService
{

    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]


    public string[] GetCompletionList(string prefixText, int count)
    {

        //ADO.Net
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        OBLSCREENINGEntities obIdp = new OBLSCREENINGEntities();
        //dt = obIdp.OBLINTRAEMPLOYEEVIEWs.Where(d => d.OBLEMAIL.Contains(prefixText)).ToDataTable();

        //Then return List of string(txtItems) as result
        List<string> txtItems = new List<string>();
        String dbValues;

        foreach (DataRow row in dt.Rows)
        {
            //String From DataBase(dbValues)
            dbValues = row["OBLEMAIL"].ToString() + "/" + row["DEPARTMENT"].ToString() + "/" + row["BRANCHNAME"].ToString() + "/" + row["DESIGNATION"].ToString() ;
            dbValues = dbValues.ToLower();
            txtItems.Add(dbValues);

        }

        return txtItems.ToArray();

    }

}

