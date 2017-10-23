using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string EmpId = string.Empty;
    public string ReferUserType = string.Empty;
    public bool IsCTPCUser = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        BasePage oBasePage = new BasePage();
        if (oBasePage.CheckEndSession())
            Response.Redirect("~/LogIn.aspx");
        else
        {
            EmpId = Session["CurrentUser"].ToString();
            lblUser.Text = Session["CurrentUserName"].ToString();
            lblUName.Text = Session["CurrentUser"].ToString();
            lblEID.Text = string.IsNullOrEmpty(Session["CurrentUserName"].ToString()) ? "" : Session["CurrentUserName"].ToString();
            lblDesgn.Text = string.IsNullOrEmpty(Session["CurrentDesgn"].ToString()) ? "" : Session["CurrentDesgn"].ToString(); //Session["CurrentDept"].ToString();
            lblDept.Text = string.IsNullOrEmpty(Session["CurrentDept"].ToString()) ? "" : Session["CurrentDept"].ToString(); //Session["CurrentDept"].ToString();
            lblBranch.Text = string.IsNullOrEmpty(Session["CurrentBranchName"].ToString()) ? "" : Session["CurrentBranchName"].ToString();// Session["CurrentBranchName"].ToString();
            lblSancCustCount.Text = oBasePage.SanctionCustomerList(Session["CurrentBranchCode"].ToString()).ToString();
            //lblPendReferCount.Text = oBasePage.PendingReferList(EmpId).ToString();
            ReferUserType = oBasePage.GetReferUserType(EmpId);
            if (Session["CurrentDept"].ToString().Trim().Contains("ctpc") || Session["CurrentUser"].ToString() == "013030502575")
                IsCTPCUser = true;
            else
                IsCTPCUser = false;

            
        }

    }
}
