using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CheckEndSession())
                Response.Redirect("../OBL Screening/LogIn.aspx");
            else
            {
                lblSancCustCount.Text = SanctionCustomerList(Session["CurrentBranchCode"].ToString().Trim()).ToString();
                //Label lblUName = (Label)Master.FindControl("lblUName");
                //Label lblUEID = (Label)Master.FindControl("lblEID");
                //Label lblUDesgn = (Label)Master.FindControl("lblDesgn");
                //Label lblUDept = (Label)Master.FindControl("lblDept");
                //Label lblUBranch = (Label)Master.FindControl("lblBranch");

                //lblUName.Text = Session["CurrentUserName"].ToString();
                //lblUEID.Text = string.IsNullOrEmpty(Session["CurrentUser"].ToString()) ? "" : Session["CurrentUser"].ToString();
                //lblUDesgn.Text = string.IsNullOrEmpty(Session["CurrentDesgn"].ToString()) ? "" : Session["CurrentDesgn"].ToString(); //Session["CurrentDept"].ToString();
                //lblUDept.Text = string.IsNullOrEmpty(Session["CurrentDept"].ToString()) ? "" : Session["CurrentDept"].ToString(); //Session["CurrentDept"].ToString();
                //lblUBranch.Text = string.IsNullOrEmpty(Session["CurrentBranchName"].ToString()) ? "" : Session["CurrentBranchName"].ToString();// Session["CurrentBranchName"].ToString();
            }
        }
    }
}