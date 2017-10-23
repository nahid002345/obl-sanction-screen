using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBLSCREENINGModel;
using System.Globalization;
using System.Data.Objects;

public partial class SearchSanctionList : BasePage
{
    private OSSearchHistory oOSSearchHistory = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CheckEndSession())
                Response.Redirect("~/LogIn.aspx");
            else
            {
                var oSDNListSdnType = obDBCtxt.SDNLISTs.Select(t => t.SDNTYPE).Distinct().ToList();
                UIUtility.FillCombo(ddlSdnType, oSDNListSdnType.ToArray(), true, false);

                var oSDNListCountry = obDBCtxt.SDNLISTs.Where(p=>!string.IsNullOrEmpty(p.COUNTRY)).Select(t => t.COUNTRY.Trim()).Distinct().ToList();
                UIUtility.FillCombo(ddlCountry, oSDNListCountry.ToArray(), true, false);
                
                if ((Request.QueryString["NAME"] != null && !string.IsNullOrEmpty(Request.QueryString["NAME"].ToString().Trim())) ||
                    (Request.QueryString["ADRS"] != null && !string.IsNullOrEmpty(Request.QueryString["ADRS"].ToString().Trim())) ||
                    (Request.QueryString["SDNTYPE"] != null && !string.IsNullOrEmpty(Request.QueryString["SDNTYPE"].ToString().Trim())) ||
                    (Request.QueryString["PURPOSE"] != null && !string.IsNullOrEmpty(Request.QueryString["PURPOSE"].ToString().Trim())) ||
                    (Request.QueryString["PERCENTAGE"] != null && !string.IsNullOrEmpty(Request.QueryString["PERCENTAGE"].ToString().Trim())) ||
                    (Request.QueryString["DOB"] != null && !string.IsNullOrEmpty(Request.QueryString["DOB"].ToString().Trim())) ||
                    (Request.QueryString["COUNTRY"] != null && !string.IsNullOrEmpty(Request.QueryString["COUNTRY"].ToString().Trim())))
                {
                    if (Request.QueryString["NAME"] != null && !string.IsNullOrEmpty(Request.QueryString["NAME"].ToString().Trim()))
                    {
                        txtSrchName.Text = (Request.QueryString["NAME"] != null) ? Request.QueryString["NAME"].ToString().Trim() : "";
                    }
                    if (Request.QueryString["ADRS"] != null && !string.IsNullOrEmpty(Request.QueryString["ADRS"].ToString().Trim()))
                    {
                        txtAddress.Text = (Request.QueryString["ADRS"] != null) ? Request.QueryString["ADRS"].ToString().Trim() : "";
                    }
                    if (Request.QueryString["SDNTYPE"] != null && !string.IsNullOrEmpty(Request.QueryString["SDNTYPE"].ToString().Trim()))
                    {
                        ddlSdnType.SelectedValue = ddlSdnType.Items.FindByText(Request.QueryString["SDNTYPE"].ToString().Trim()).Value;
                    }
                    if (Request.QueryString["PURPOSE"] != null && !string.IsNullOrEmpty(Request.QueryString["PURPOSE"].ToString().Trim()))
                    {
                        ddlPurpose.SelectedValue = ddlPurpose.Items.FindByText(Request.QueryString["PURPOSE"].ToString().Trim()).Value;
                    }
                    if (Request.QueryString["PERCENTAGE"] != null && !string.IsNullOrEmpty(Request.QueryString["PERCENTAGE"].ToString().Trim()))
                    {
                        txtPercentage.Text = (Request.QueryString["PERCENTAGE"] != null) ? Request.QueryString["PERCENTAGE"].ToString().Trim() : "50";
                    }
                    if (Request.QueryString["DOB"] != null && !string.IsNullOrEmpty(Request.QueryString["DOB"].ToString().Trim()))
                    {
                        txtDOB.Text = (Request.QueryString["DOB"] != null) ? Request.QueryString["DOB"].ToString().Trim() : "";
                    }
                    if (Request.QueryString["COUNTRY"] != null && !string.IsNullOrEmpty(Request.QueryString["COUNTRY"].ToString().Trim()))
                    {
                        ddlCountry.SelectedValue = ddlCountry.Items.FindByText(Request.QueryString["COUNTRY"].ToString().Trim()).Value;

                    }
                    LoadGrid();

                    if (Request.QueryString["SRCHID"] != null && !string.IsNullOrEmpty(Request.QueryString["SRCHID"].ToString().Trim()))
                    {
                        string SrchId = Request.QueryString["SRCHID"].ToString().Trim();
                        oOSSearchHistory = obDBCtxt.OSSearchHistories.FirstOrDefault(t => t.SearchRefNo == SrchId);
                        lblSearchID.Text= oOSSearchHistory.SearchRefNo;
                        txtACOpen.Text = (oOSSearchHistory.IsSuspicious == true && !string.IsNullOrEmpty(oOSSearchHistory.ACOpenBy)) ? oOSSearchHistory.ACOpenBy : string.Empty;
                        //btnSuspicious.InnerText = (oOSSearchHistory.IsSuspicious == true && !string.IsNullOrEmpty(oOSSearchHistory.ACOpenBy)) ? "Suspicious(+)" : "Suspicious";                         
                        SetReferInfo(oOSSearchHistory.SearchRefNo);
                    }

                    else if (Session["SRCHID"] != null && !string.IsNullOrEmpty(Session["SRCHID"].ToString().Trim()))
                    {
                        string SrchId = Request.QueryString["SRCHID"].ToString().Trim();
                        oOSSearchHistory = obDBCtxt.OSSearchHistories.FirstOrDefault(t => t.SearchRefNo == SrchId);
                        lblSearchID.Text = oOSSearchHistory.SearchRefNo;
                        txtACOpen.Text = (oOSSearchHistory.IsSuspicious == true && !string.IsNullOrEmpty(oOSSearchHistory.ACOpenBy)) ? oOSSearchHistory.ACOpenBy : string.Empty;
                        SetReferInfo(oOSSearchHistory.SearchRefNo);
                        //btnSuspicious.InnerText = (oOSSearchHistory.IsSuspicious == true && !string.IsNullOrEmpty(oOSSearchHistory.ACOpenBy)) ? "Suspicious(+)" : "Suspicious";
                    }
                    
                    //Session["SText"] = "NAME=" + Request.QueryString["NAME"].ToString().Trim() + "&SDNTYPE=" + Request.QueryString["SDNTYPE"].ToString().Trim();
                }
                else
                {

                }
                

            }
        }

    }

    private void LoadGrid()
    {
        
        string srchDob =(!string.IsNullOrEmpty(txtDOB.Text.Trim())) ? txtDOB.Text.Trim() : "";
        Int32 srchPer= (!string.IsNullOrEmpty(txtPercentage.Text.Trim())) ? Convert.ToInt32(txtPercentage.Text.Trim()) : 80;
        //if(!string.IsNullOrEmpty(txtDOB.Text.Trim())) 
        //DateTime srchDobDt = DateTime.ParseExact(srchDob, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        var oList = SearchSanctionListByName(txtSrchName.Text.Trim(), txtAddress.Text.Trim(), (ddlCountry.SelectedItem.Value != "0") ? ddlCountry.SelectedItem.Text.Trim() : "", srchDob, srchPer);
        

        if (oList != null && oList.Count > 0)
        {
            if (ddlSdnType.SelectedValue.ToString() != "0")
               oList=oList.Where(t => t.SDNTYPE.Equals(ddlSdnType.SelectedItem.Text.ToString().Trim())).ToList();
        }
        oList = oList.GroupBy(t => t.SDNENTRY_ID).Select(t => t.FirstOrDefault()).ToList();
        gridSanctionList.DataSource = oList.GroupBy(t=>t.SDNENTRY_ID).Select(t=>t.FirstOrDefault());
        gridSanctionList.DataBind();
        lblResultCount.Text = "("+ oList.Count.ToString() +" result(s) found)";

        if (oList.Count > 0)
        {
            btnSuspSave.Enabled = true;
        }
        else
        {
            btnSuspSave.Enabled = false;
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!CheckEndSession())
        {
            string strSearchDetail = (string.IsNullOrEmpty(txtSrchName.Text.Trim())) ? "" : "NAME=" + txtSrchName.Text.Trim() + "&";
            strSearchDetail = (string.IsNullOrEmpty(txtAddress.Text.Trim())) ? strSearchDetail + "" : strSearchDetail + "ADRS=" + txtAddress.Text.Trim() + "&";
            strSearchDetail = (ddlSdnType.SelectedValue.ToString() == "0") ? strSearchDetail + "" : strSearchDetail + "SDNTYPE=" + ddlSdnType.SelectedItem.Text.Trim() + "&";
            strSearchDetail = (ddlPurpose.SelectedValue.ToString() == "0") ? strSearchDetail + "" : strSearchDetail + "PURPOSE=" + ddlPurpose.SelectedItem.Text.Trim() + "&";
            strSearchDetail = (string.IsNullOrEmpty(txtPercentage.Text.Trim())) ? strSearchDetail + "" : strSearchDetail + "PERCENTAGE=" + txtPercentage.Text.Trim() + "&";
            strSearchDetail = (string.IsNullOrEmpty(txtDOB.Text.Trim())) ? strSearchDetail + "" : strSearchDetail + "DOB=" + txtDOB.Text.Trim() + "&";
            strSearchDetail = (ddlCountry.SelectedValue.ToString() == "0") ? strSearchDetail + "" : strSearchDetail + "COUNTRY=" + ddlCountry.SelectedItem.Text.Trim();
            Session["SText"] = strSearchDetail;
            string EmpId=Session["CurrentUser"].ToString();
            string srchRefNo = DateTime.Now.ToString("yyMMddHHmmssff", CultureInfo.InvariantCulture) + EmpId.Substring(EmpId.Length - 4, 4);

            oOSSearchHistory = new OSSearchHistory();
            oOSSearchHistory.EmployeeID = Session["CurrentUser"].ToString().Trim();
            oOSSearchHistory.CreatedBy = Session["CurrentUser"].ToString().Trim();
            oOSSearchHistory.CreatedOn = DateTime.Now;
            oOSSearchHistory.EmpBranchCode = Session["CurrentBranchCode"].ToString().Trim();
            oOSSearchHistory.IsActive = true;
            oOSSearchHistory.Purpose = ddlPurpose.SelectedValue; 
            oOSSearchHistory.SearchCompIP = GetIp().ToString();
            oOSSearchHistory.SearchDate = DateTime.Now;
            oOSSearchHistory.SearchDetail = strSearchDetail;
            oOSSearchHistory.SearchRefNo = srchRefNo;
            obDBCtxt.OSSearchHistories.AddObject(oOSSearchHistory);
            obDBCtxt.SaveChanges();

            LoadGrid();
            
            lblRefNo.Text = "Reference Number : "+ srchRefNo;
            lblSearchID.Text = srchRefNo;
            Session["SRCHID"] = srchRefNo;

            SetReferInfo(srchRefNo);
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowSuccessBox()", true);
        }
        else
        {

        }
    }

    private void SetReferInfo(string ReferenceNo)
    {
        List<vwReferCaseList> CaseList = null;
        btnSuspSave.Enabled = true;
        OSSearchHistory oOSSearchHistoryTemp = obDBCtxt.OSSearchHistories.FirstOrDefault(t => t.SearchRefNo == ReferenceNo);
        if (oOSSearchHistory.OSREFERINFOes.Count > 0)
        {
            OSUSER oOSUSERInitiator = new OSUSER();
            oOSUSERInitiator.EMPLOYEEID = oOSSearchHistory.EmployeeID;
            oOSUSERInitiator.DETAILINFO = oOSUSERInitiator.EMPLOYEEID + " - INITIATOR";
            CaseList = obDBCtxt.vwReferCaseLists.Where(t => t.SearchRefNo == ReferenceNo).ToList();
            obDBCtxt.Refresh(RefreshMode.StoreWins, CaseList);
            string currentUserId=Session["CurrentUser"].ToString().Trim();
            if (oOSSearchHistory.OSREFERINFOes.OrderByDescending(t => t.ID).First().REFERFROM != currentUserId && oOSSearchHistory.EmployeeID != currentUserId)
            {
                Int32 CaMelCoUserType = Convert.ToInt32(Enumaretion.SanctionUserType.DeputyCAMELCO);
                Int32 CaMelCoUserType1 = Convert.ToInt32(Enumaretion.SanctionUserType.CAMELCO);
                var oReferUserList = obDBCtxt.OSUSERs.Where(t => (t.USERTYPE == CaMelCoUserType || t.USERTYPE == CaMelCoUserType1) && t.ISACTIVE).Distinct().ToList();
                oReferUserList.Add(oOSUSERInitiator);
                UIUtility.FillCombo(ddlReferTo, "DETAILINFO", "EMPLOYEEID", oReferUserList, true, false);
                ddlRefStatus.Items.RemoveAt(1);
                ddlRefStatus.SelectedIndex = 1;//to inititate a request in value 1
                ddlRefStatus.Enabled = true;

                
                
            }
            else
            {
                Int32 ReferApprvCode = Convert.ToInt32(Enumaretion.ReferStatus.Approve);
                if (oOSSearchHistory.OSREFERINFOes.OrderByDescending(t => t.ID).First().REFERSTATUS == ReferApprvCode && oOSSearchHistory.EmployeeID==currentUserId && oOSSearchHistory.IsOpen == true)
                {
                    txtACOpen.Enabled = true;

                    btnSuspSave.Enabled = true;
                    ddlReferTo.Enabled = false;
                    ddlRefStatus.Enabled = false;
                    referStatusVal.Enabled = false;
                    referToVal.Enabled = false;
                }
                else
                {
                    txtACOpen.Enabled = false;
                    btnSuspSave.Enabled = false;
                    ddlReferTo.Enabled = false;
                    ddlRefStatus.Enabled = false;
                }
                txtACOpen.Text=(!string.IsNullOrEmpty(oOSSearchHistory.ACOpenBy)) ? oOSSearchHistory.ACOpenBy.Trim() : string.Empty;
            }
            

        }
        else
        {
            Int32 UserType = Convert.ToInt32(Enumaretion.SanctionUserType.BAMELCO);
            var oReferUserList = obDBCtxt.OSUSERs.Where(t => t.USERTYPE == UserType && t.ISACTIVE).Distinct().ToList();
            UIUtility.FillCombo(ddlReferTo, "DETAILINFO", "EMPLOYEEID", oReferUserList, true, false);
            ddlRefStatus.SelectedIndex = 1;//to inititate a request in value 1
            txtACOpen.Text = string.Empty;
            ddlRefStatus.Enabled = false;
            ddlReferTo.Enabled = true;
        }

        gvCaseInfo.DataSource = CaseList;
        gvCaseInfo.DataBind();

    }
    protected void gridSanctionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        gridSanctionList.PageIndex = e.NewPageIndex;
        ////LoadGrid();
    }
    protected void btnSuspSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblSearchID.Text))
        {
            try
            {
                string refId = lblSearchID.Text.Trim();
                string EmpId=Session["CurrentUser"].ToString().Trim();
                oOSSearchHistory = obDBCtxt.OSSearchHistories.FirstOrDefault(t => t.SearchRefNo == refId);
                if (oOSSearchHistory != null)
                {

                    
                    //oOSSearchHistory.IsSuspicious = true;
                    //oOSSearchHistory.ACOpenBy = txtACOpen.Text.Trim();
                    if (oOSSearchHistory.OSREFERINFOes.Count > 0 && oOSSearchHistory.OSREFERINFOes.OrderByDescending(t => t.ID).First().REFERSTATUS == Convert.ToInt32(Enumaretion.ReferStatus.Approve) && oOSSearchHistory.EmployeeID == EmpId)
                    {
                        oOSSearchHistory.IsOpen = false;
                        oOSSearchHistory.ACOpenBy = txtACOpen.Text.Trim();
                    }
                    else
                    {
                        OSREFERINFO oOSREFERINFO = new OSREFERINFO();
                        oOSREFERINFO.SEARCHID = oOSSearchHistory.ID;
                        oOSREFERINFO.REFERFROM = Session["CurrentUser"].ToString().Trim();
                        if (Convert.ToInt32(ddlRefStatus.SelectedValue) > Convert.ToInt32(Enumaretion.ReferStatus.Recommend))
                        {
                            oOSREFERINFO.REFERSTATUS = Convert.ToInt32(ddlRefStatus.SelectedValue.ToString());
                            ddlReferTo.SelectedIndex = ddlReferTo.Items.Count - 1;
                            oOSREFERINFO.REFERTO = ddlReferTo.SelectedValue.ToString();
                        }
                        else
                        {
                            oOSREFERINFO.REFERSTATUS = Convert.ToInt32(ddlRefStatus.SelectedValue.ToString());
                            oOSREFERINFO.REFERTO = ddlReferTo.SelectedValue.ToString();
                        }
                        oOSREFERINFO.REFERDATE = DateTime.Now;
                        
                        oOSREFERINFO.REFERREMARKS = txtReferRemark.Text.Trim();
                        oOSREFERINFO.ISOPEN = true;
                        oOSREFERINFO.CREATEDBY = Session["CurrentUser"].ToString().Trim();
                        oOSREFERINFO.CREATEDON = DateTime.Now;
                        obDBCtxt.OSREFERINFOes.AddObject(oOSREFERINFO);
                        obDBCtxt.SaveChanges();

                        oOSSearchHistory.IsOpen = true;
                        oOSSearchHistory.ReferStatus = Convert.ToInt32(ddlRefStatus.SelectedValue);
                    }
                    
                    oOSSearchHistory.ModifiedBy = Session["CurrentUser"].ToString().Trim();
                    oOSSearchHistory.ModifiedOn = DateTime.Now;
                    obDBCtxt.SaveChanges();
                    btnSuspSave.Enabled = false;
                    lblRefNo.Text = "Refer Information is saved SUCCESSFULLY ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowSuccessBox()", true);
                }
                else
                {
                    lblRefNo.Text = "DATA is not Found!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
                }
            }
            catch (Exception ex)
            {
                lblRefNo.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
            }
        }
            
        else
        {
            lblRefNo.Text = "DATA is not Found!";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scrpt1", "ShowAlertBox()", true);
        }

    }

    protected void gvCaseInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
    }


    protected void gvCaseInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            if (e.Row.Cells[5].Text.ToLower() == "1")
                e.Row.Cells[5].Text = "Requested";
            else if (e.Row.Cells[5].Text.ToLower() == "2")
                e.Row.Cells[5].Text = "Recommended";
            else if (e.Row.Cells[5].Text.ToLower() == "3")
                e.Row.Cells[5].Text = "Approved";
            else if (e.Row.Cells[5].Text.ToLower() == "4")
                e.Row.Cells[5].Text = "Refused";
        }
    }
}