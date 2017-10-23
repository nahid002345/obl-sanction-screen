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

public partial class SuspiciousList : BasePage
{
    public List<OSSearchHistory> oOSSearchHistory = null;
    public OSSearchHistory oOSSrchHis = null;
    
    public List<string> oIDList = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CheckEndSession())
                Response.Redirect("~/LogIn.aspx");
            else
            {
                LoadGridData(string.Empty, string.Empty);
            }
        }

    }


    

    private void ClearControl()
    {
         
    }

    private void LoadGridData(string fromDate, string toDate)
    {

        DateTime fromDt = new DateTime();
        DateTime toDt = new DateTime();
        string TodayDate= DateTime.Today.ToString("dd/MM/yyyy");

        string Empid= Session["CurrentUser"].ToString();

        oOSSearchHistory = obDBCtxt.OSSearchHistories.Where(t => t.EmployeeID == Empid && t.IsSuspicious == true).ToList();
        if(string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            oOSSearchHistory = oOSSearchHistory.Where(t => t.SearchDate >= DateTime.Today).ToList();
        if (!string.IsNullOrEmpty(fromDate) && oOSSearchHistory != null)
        {
            fromDt = DateTime.ParseExact(fromDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            oOSSearchHistory = oOSSearchHistory.Where(t => t.SearchDate >= fromDt).ToList();
        }
        if (!string.IsNullOrEmpty(toDate) && oOSSearchHistory != null)
        {
            toDt = DateTime.ParseExact(toDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(23).AddMinutes(59).AddSeconds(59);
            oOSSearchHistory = oOSSearchHistory.Where(t => t.SearchDate <= toDt).ToList();
        }
        
        gvSearchLog.DataSource = oOSSearchHistory.OrderByDescending(t=>t.ID).ToList();
        gvSearchLog.DataBind();
        lblResultCount.Text = oOSSearchHistory.Count.ToString();
    }
    private void ClearGridData()
    {
        oOSSearchHistory = null;
        gvSearchLog.DataSource = oOSSearchHistory;
        gvSearchLog.DataBind();
    }

    

    protected void gvSearchLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        gvSearchLog.PageIndex = e.NewPageIndex;
        this.LoadGridData(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
    }








    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridData(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
    }
    protected void gvSearchLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            if (e.Row.Cells[3].Text.ToLower() == "true")
                e.Row.Cells[3].Text = "Yes";
            else
                e.Row.Cells[3].Text = "No";
        }
    }
}