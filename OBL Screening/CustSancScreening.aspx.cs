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

public partial class CustSancScreening : BasePage
{
    public List<EXISTINGSANCCUSTINFO> oEXISTINGSANCCUSTINFO = null;
    
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

    private void LoadGridData(string CustNo, string CustName)
    {
        string BranchCode = Session["CurrentBranchCode"].ToString();
        oEXISTINGSANCCUSTINFO = obDBCtxt.EXISTINGSANCCUSTINFOes.Where(t => t.LOCAL_BRANCH_CODE == BranchCode && ((string.IsNullOrEmpty(CustName)) || t.CUSTOMER_NAME1.Contains(CustName)) && ((string.IsNullOrEmpty(CustNo)) || t.CUSTOMER_NO == CustNo)).ToList();
        grvSancCustInfo.DataSource = oEXISTINGSANCCUSTINFO.OrderBy(t=>t.ID).ToList();
        grvSancCustInfo.DataBind();
        lblResultCount.Text = oEXISTINGSANCCUSTINFO.Count.ToString();
    }
    private void ClearGridData()
    {
        oEXISTINGSANCCUSTINFO = null;
        grvSancCustInfo.DataSource = oEXISTINGSANCCUSTINFO;
        grvSancCustInfo.DataBind();
    }

    

    protected void grvSancCustInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        e.Cancel = true;
        grvSancCustInfo.PageIndex = e.NewPageIndex;
        this.LoadGridData(txtSrchCustNo.Text.Trim(), txtSrchName.Text.Trim());
    }








    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridData(txtSrchCustNo.Text.Trim(), txtSrchName.Text.Trim());
    }
}