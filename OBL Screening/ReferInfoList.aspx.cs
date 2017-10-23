﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBLSCREENINGModel;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using System.Data.Objects;

public partial class ReferInfoList : BasePage
{
    public List<OSSearchHistory> oOSSearchHistory = null;
    public List<vwReferList> ovwReferList = null;
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
                LoadGridDataRequested(string.Empty, string.Empty);

                LoadGridDataTask(string.Empty, string.Empty);

            }
        }

    }


    

    private void ClearControl()
    {
         
    }

    private void LoadGridDataRequested(string fromDate, string toDate)
    {
        try
        {
            DateTime fromDt = new DateTime();
            DateTime toDt = new DateTime();
            string TodayDate = DateTime.Today.ToString("dd/MM/yyyy");

            string Empid = Session["CurrentUser"].ToString();

            oOSSearchHistory = obDBCtxt.OSSearchHistories.Where(t => t.EmployeeID == Empid && t.OSREFERINFOes.Count > 0).OrderByDescending(t => t.ID).GroupBy(t => t.SearchRefNo).Select(t => t.FirstOrDefault()).ToList();
            obDBCtxt.Refresh(RefreshMode.StoreWins, oOSSearchHistory);

            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
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

            gvRequestedRefer.DataSource = oOSSearchHistory.OrderByDescending(t => t.ID).ToList();
            gvRequestedRefer.DataBind();
            lblResultCount.Text = oOSSearchHistory.Count.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "DATA is not Found due to " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
        }
    }
    private void ClearGridData()
    {
        oOSSearchHistory = null;
        gvRequestedRefer.DataSource = oOSSearchHistory;
        gvRequestedRefer.DataBind();
    }



    protected void gvRequestedRefer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        gvReferTask.PageIndex = e.NewPageIndex;
        this.LoadGridDataRequested(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
    }








    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridDataRequested(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
    }

    protected void btnSearchTask_Click(object sender, EventArgs e)
    {
        LoadGridDataTask(txtTaskFromDate.Text.Trim(), txtTaskToDate.Text.Trim());
    }
    protected void gvRequestedRefer_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            if (e.Row.Cells[6].Text.ToLower() == "1")
                e.Row.Cells[6].Text = "Requested";
            else if (e.Row.Cells[6].Text.ToLower() == "2")
                e.Row.Cells[5].Text = "Recommended";
            else if (e.Row.Cells[6].Text.ToLower() == "3")
                e.Row.Cells[6].Text = "Approved";
            else if (e.Row.Cells[6].Text.ToLower() == "4")
                e.Row.Cells[6].Text = "Refused";
        }
    }

    protected void gvReferTask_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        gvReferTask.PageIndex = e.NewPageIndex;
        this.LoadGridDataRequested(txtFromDate.Text.Trim(), txtToDate.Text.Trim());
    }

    protected void gvReferTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            if (e.Row.Cells[7].Text.ToLower() == "1")
                e.Row.Cells[7].Text = "Requested";
            else if (e.Row.Cells[7].Text.ToLower() == "2")
                e.Row.Cells[7].Text = "Recommended";
            else if (e.Row.Cells[7].Text.ToLower() == "3")
                e.Row.Cells[7].Text = "Approved";
            else if (e.Row.Cells[7].Text.ToLower() == "4")
                e.Row.Cells[7].Text = "Refused";
        }
    }


    private void LoadGridDataTask(string fromDate, string toDate)
    {
        try
        {
            DateTime fromDt = new DateTime();
            DateTime toDt = new DateTime();
            string TodayDate = DateTime.Today.ToString("dd/MM/yyyy");

            string Empid = Session["CurrentUser"].ToString();

            ovwReferList = obDBCtxt.vwReferLists.Where(t => t.REFERTO == Empid && t.INITIATOR != Empid).OrderByDescending(t => t.ID).GroupBy(t => t.SearchRefNo).Select(p => p.FirstOrDefault()).ToList();
            obDBCtxt.Refresh(RefreshMode.StoreWins, ovwReferList);
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                ovwReferList = ovwReferList.Where(t => t.SearchDate >= DateTime.Today).ToList();
            if (!string.IsNullOrEmpty(fromDate) && ovwReferList != null)
            {
                fromDt = DateTime.ParseExact(fromDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ovwReferList = ovwReferList.Where(t => t.SearchDate >= fromDt).ToList();
            }
            if (!string.IsNullOrEmpty(toDate) && ovwReferList != null)
            {
                toDt = DateTime.ParseExact(toDate.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).AddHours(23).AddMinutes(59).AddSeconds(59);
                ovwReferList = ovwReferList.Where(t => t.SearchDate <= toDt).ToList();
            }

            gvReferTask.DataSource = ovwReferList.OrderByDescending(t => t.ID).ToList();
            gvReferTask.DataBind();
            lblTaskCount.Text = ovwReferList.Count.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "DATA is not Found due to " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
        }
    }
}