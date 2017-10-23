using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBLSCREENINGModel;
using System.Data.Objects;
using System.Data;

public partial class SanctionDetails : BasePage
{
    #region Properties
    public List<vwADDRESSList> ovwADDRESSList = null;
    public List<vwAKAList> ovwAKAList = null;

    public List<vwCitizenList> ovwCitizenList = null;
    public List<vwIDList> ovwIDList = null;

    public List<vwNationalityList> ovwNationalityList = null;
    public List<vwProgramList> ovwProgramList = null;

    public SDN_DETAIL_INFO oSDN_DETAIL_INFO = null;
    public string pageMode = string.Empty;
    
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CheckEndSession())
                Response.Redirect("~/LogIn.aspx");
            else
            {
                if (Request.QueryString["Id"] != null)
                {
                    Int64 lngId = Convert.ToInt64(Request.QueryString["Id"].ToString().Trim());
                    string ListType = Request.QueryString["Type"].ToString().Trim();
                    string SdnType = Request.QueryString["SType"].ToString().Trim();
                    //var refreshableObjects = (from entry in obDBCtxt.ObjectStateManager.GetObjectStateEntries(
                    //                            EntityState.Added
                    //                           | EntityState.Deleted
                    //                           | EntityState.Modified
                    //                           | EntityState.Unchanged)
                    //                          where entry.EntityKey != null
                    //                          select entry.Entity);

                    //obDBCtxt.Refresh(RefreshMode.StoreWins, refreshableObjects);

                    GetSanctionDetail(lngId,ListType,SdnType);
                    GetAddressList(lngId, ListType, SdnType);
                    GetAKAList(lngId, ListType, SdnType);
                    GetIDlist(lngId, ListType, SdnType);
                    GetCitizenList(lngId, ListType, SdnType);
                    if (Request.QueryString["MOD"] != null && Request.QueryString["MOD"].ToString().Trim().ToUpper() == "CTPC")
                    {
                        pageMode = "CTPC";
                    }
                }
            }
        }
    }

    private void GetSanctionDetail(Int64 SdnID, string ListType,string SdnType)
    {
        oSDN_DETAIL_INFO = obDBCtxt.SDN_DETAIL_INFO.FirstOrDefault(p=>p.SDN_ID == SdnID && p.LISTED_BY == ListType && p.SDNTYPE == SdnType);
        if (oSDN_DETAIL_INFO != null)
        {
            lblType.Text = oSDN_DETAIL_INFO.SDNTYPE;
            lblList.Text = "";
            lblC3Row1.Text = (string.IsNullOrEmpty(oSDN_DETAIL_INFO.COMMENTS.Trim())) ? "" : "Comments: ";
            lblComment.Text = oSDN_DETAIL_INFO.COMMENTS;
            if (oSDN_DETAIL_INFO.SDNTYPE.ToLower() == "individual")
            {
                //label text entry
                lblC1Row1.Text = "Last Name:";
                lblC1Row2.Text = "First Name:";
                lblC1Row3.Text = "Title:";
                lblC1Row4.Text = "Date of Birth:";
                lblC1Row5.Text = "Place of Birth";

                lblC2Row1.Text = "Program:";
                lblC2Row2.Text = "Nationality:";
                lblC2Row3.Text = "Citizenship:";
                lblC2Row4.Text = "Remarks:";
                lblC2Row5.Text = "";

                //data text
                lblLastName.Text = oSDN_DETAIL_INFO.LASTNAME;
                lblFirstName.Text = oSDN_DETAIL_INFO.FIRSTNAME;
                lblTitle.Text = oSDN_DETAIL_INFO.TITLE;
                lblDOB.Text = (!string.IsNullOrEmpty(oSDN_DETAIL_INFO.DOB)) ? oSDN_DETAIL_INFO.DOB.ToString() : "";
                lblPOB.Text = oSDN_DETAIL_INFO.POB;

                lblProg.Text = oSDN_DETAIL_INFO.PROGRAM;
                lblNation.Text = oSDN_DETAIL_INFO.NATIONALITY;
                lblCitShip.Text = oSDN_DETAIL_INFO.CITIZEN;
                lblRemark.Text = "";
                lblVOwner.Text = "";
            }
            else if (oSDN_DETAIL_INFO.SDNTYPE.ToLower() == "entity")
            {
                //label text entry
                lblC1Row1.Text = "Entity Name:";
                lblC1Row2.Text = "";
                lblC1Row3.Text = "";
                lblC1Row4.Text = "";
                lblC1Row5.Text = "";

                lblC2Row1.Text = "Program:";
                lblC2Row2.Text = "";
                lblC2Row3.Text = "";
                lblC2Row4.Text = "Remarks:";
                lblC2Row5.Text = "";
                

                //data text
                lblLastName.Text = oSDN_DETAIL_INFO.FIRSTNAME;
                lblFirstName.Text = "";
                lblTitle.Text = "";
                lblDOB.Text = "";
                lblPOB.Text = "";

                lblProg.Text = oSDN_DETAIL_INFO.PROGRAM;                
                lblNation.Text = "";
                lblCitShip.Text = ""; 
                lblRemark.Text = "";
                lblVOwner.Text = "";
            }
            else if (oSDN_DETAIL_INFO.SDNTYPE.ToLower() == "vessel")
            {
                //label text entry
                lblC1Row1.Text = "Vessel Name:";
                lblC1Row2.Text = "Call Sign:";
                lblC1Row3.Text = "Tonnage:";
                lblC1Row4.Text = "Gross Registered Tonnage:";
                lblC1Row5.Text = "";

                lblC2Row1.Text = "Program:";
                lblC2Row2.Text = "Vessel Flag:";
                lblC2Row3.Text = "Vessel Type:";
                lblC2Row4.Text = "Remarks:";
                lblC2Row5.Text = "Vessel Owner:";


                //data text
                lblLastName.Text = oSDN_DETAIL_INFO.LASTNAME;
                lblFirstName.Text = "";
                lblTitle.Text = "";
                lblDOB.Text = "";
                lblPOB.Text = "";

                lblProg.Text = oSDN_DETAIL_INFO.PROGRAM;
                lblNation.Text = "";
                lblCitShip.Text = "";
                lblRemark.Text = "";
            }
            else if (oSDN_DETAIL_INFO.SDNTYPE.ToLower() == "aircraft")
            {
                //label text entry
                lblC1Row1.Text = "Aircraft Name:";
                lblC1Row2.Text = "";
                lblC1Row3.Text = "";
                lblC1Row4.Text = "";
                lblC1Row5.Text = "";

                lblC2Row1.Text = "Program:";
                lblC2Row2.Text = "";
                lblC2Row3.Text = "";
                lblC2Row4.Text = "Remarks:";
                lblC2Row5.Text = "";


                //data text
                lblLastName.Text = oSDN_DETAIL_INFO.FIRSTNAME;
                lblFirstName.Text = "";
                lblTitle.Text = "";
                lblDOB.Text = "";
                lblPOB.Text = "";

                lblProg.Text = oSDN_DETAIL_INFO.PROGRAM;
                lblNation.Text = "";
                lblCitShip.Text = "";
                lblRemark.Text = "";
                lblVOwner.Text = ""; ;
            }
            

            
        }


    }

    private void GetAKAList(Int64 SdnID, string ListType,string SdnType)
    {
        ovwAKAList = obDBCtxt.vwAKALists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY.Trim() == ListType && t.SDNTYPE.Trim() == SdnType).Distinct().ToList();
        //obDBCtxt.Refresh(RefreshMode.StoreWins, ovwAKAList);
        if (ovwAKAList != null)
        {
            grvAKA.DataSource = ovwAKAList;
            grvAKA.DataBind();
        }
    }

    private void GetAddressList(Int64 SdnID, string ListType,string SdnType)
    {
        
        ovwADDRESSList = obDBCtxt.vwADDRESSLists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY == ListType && t.SDNTYPE == SdnType).Distinct().ToList();        
        if (ovwADDRESSList != null)
        {
            grvAddress.DataSource = ovwADDRESSList;
            grvAddress.DataBind();
        }
    }

    private void GetIDlist(Int64 SdnID, string ListType,string SdnType)
    {
        ovwIDList = obDBCtxt.vwIDLists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY == ListType && t.SDNTYPE == SdnType).Distinct().ToList();
        //obDBCtxt.Refresh(RefreshMode.StoreWins, ovwIDList);
        if (ovwIDList != null)
        {
            grvID.DataSource = ovwIDList;
            grvID.DataBind();
        }
    }

    private void GetCitizenList(Int64 SdnID, string ListType,string SdnType)
    {
        ovwCitizenList = obDBCtxt.vwCitizenLists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY == ListType && t.SDNTYPE == SdnType).Distinct().ToList();
        if (ovwCitizenList != null)
        {
            grvCitizen.DataSource = ovwCitizenList;
            grvCitizen.DataBind();
        }
    }

    private void GetProgList(Int64 SdnID, string ListType,string SdnType)
    {
        ovwProgramList = obDBCtxt.vwProgramLists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY == ListType && t.SDNTYPE == SdnType).Distinct().ToList();
        //obDBCtxt.Refresh(RefreshMode.StoreWins, ovwProgramList);
        if (ovwProgramList != null)
        {
            grvProg.DataSource = ovwProgramList;
            grvProg.DataBind();
        }
    }

    private void GetNationalityList(Int64 SdnID, string ListType,string SdnType)
    {
        ovwNationalityList = obDBCtxt.vwNationalityLists.Where(t => t.sdnEntry_Id == SdnID && t.LISTED_BY == ListType && t.SDNTYPE == SdnType).Distinct().ToList();
        //obDBCtxt.Refresh(RefreshMode.StoreWins, ovwNationalityList);
        if (ovwNationalityList != null)
        {
            grvNationality.DataSource = ovwNationalityList;
            grvNationality.DataBind();
        }
    }
    protected void grvAKA_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Alias Details";
            HeaderCell2.ColumnSpan = 3;
            HeaderRow.Cells.Add(HeaderCell2);
            grvAKA.Controls[0].Controls.AddAt(0, HeaderRow);
        }

    }
    protected void grvAddress_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Text = "Address Details";
            HeaderCell2.ColumnSpan = 5;
            HeaderRow.Cells.Add(HeaderCell2);
            grvAddress.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void grvID_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Text = "ID Details";
            HeaderCell2.ColumnSpan = 5;
            HeaderRow.Cells.Add(HeaderCell2);
            grvID.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    protected void grvCitizen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Text = "Citizen Details";
            HeaderCell2.ColumnSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);
            grvCitizen.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }

    protected void grvNationality_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Text = "Natioanlity Details";
            HeaderCell2.ColumnSpan = 2;
            HeaderRow.Cells.Add(HeaderCell2);
            grvNationality.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }

    protected void grvProg_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell2 = new TableCell();

            HeaderCell2.Text = "Program Details";
            HeaderCell2.ColumnSpan = 1;
            HeaderRow.Cells.Add(HeaderCell2);
            grvProg.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
}