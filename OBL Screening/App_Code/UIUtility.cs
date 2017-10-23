

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

using OBL_Screening;
public class UIUtility
{
    public static string sSessionOutURL = "Default.aspx";
    private long id;
    //public static long CurrentUserId = 0; // client id
    public static Int32 CurrentAdminUserId = 0; // admin id
    public static string CurrentAdminUser = "";

    public static int oCurrentYear;
    public static string CurrentClientUserId = "CurrentClientUserId";
    public static string CurrentClientId = "CurrentClientId";
    public static string CurrentBranch = "CurrentBranch";
    public static bool IsNew = false;
    public static string PhotoPath = "http://www.oblintranet.com/IntraImg/";

    #region Product Image Path

    public static string ProdImage_Small = "../UploadFiles/SmallImage/";
    public static string ProdImage_Large = "../UploadFiles/LargeImage/";
    public static string ProdImage_Thumbnail = "../UploadFiles/ThumbnailImage/";
    public static string ProdImage_NotFoundImage = "UploadFiles/ImageNotFound.gif";
    public static string ProdDetailImage_NotFoundImage = "../UploadFiles/ImageNotFound.gif";
    public static string ProdCatImage_NotFoundImage = "../UploadFiles/ImageNotFound.gif";
    public static string BrandImage_NotFoundImage = "../UploadFiles/ImageNotFound.gif";
    public static string VideoFile_Returned = "../UploadFiles/VideoFile/";
    public static string BrandImage_Returned = "../Images/Brand/";
    public static string CatImage_Returned = "../images/Category/";
    public static string ProdImage_Returned = "../UploadFiles/ReturnedImage/";
    public static string AucProdImage_Small = "../Images/Auction/";
    public static string AucProdImage_Large = "../Images/Auction/";
    public static string AucProdImage_Thumbnail = "../Images/Auction/";
    public static string AucProdImage_NotFoundImage = "../Images/Auction/ImageNotFound.gif";
    #endregion

    public UIUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void ShowUIMessage(Label lblMsg, Image imgMsg, Panel pnlMsg, string msg, int type)
    {
        switch (type)
        {
            case 1:
                lblMsg.Text = "Error: " + msg;
                imgMsg.ImageUrl = "~/Images/error.png";
                pnlMsg.CssClass = "lErrorMsg";
                break;
            case 2:
                lblMsg.Text = "Warning: " + msg;
                imgMsg.ImageUrl = "~/Images/warning.png";
                pnlMsg.CssClass = "lWarningMsg";
                break;
            case 3:
                lblMsg.Text = msg;
                imgMsg.ImageUrl = "~/Images/info.gif";
                pnlMsg.CssClass = "lInfoMsg";
                break;
            case 4:
                lblMsg.Text = msg;
                imgMsg.ImageUrl = "";
                pnlMsg.CssClass = "lNoMsg";
                break;
        }
    }

    public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, ICollection iCollection, bool bHasBlank, bool bMandatory)
    {
        dropDownList.DataTextField = dataTextField;
        dropDownList.DataValueField = dataValueField;
        dropDownList.DataSource = iCollection;
        dropDownList.DataBind();

        if (bHasBlank)
        {
            ListItem oItem = new ListItem();
            if (!bMandatory)
            {
                oItem.Value = "0";
                oItem.Text = "Select";
            }
            dropDownList.Items.Insert(0, oItem);
        }
    }

    public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, ICollection iCollection, bool bHasBlank, bool bMandatory,string zeroIndexString)
    {
        dropDownList.DataTextField = dataTextField;
        dropDownList.DataValueField = dataValueField;
        dropDownList.DataSource = iCollection;
        dropDownList.DataBind();

        if (bHasBlank)
        {
            ListItem oItem = new ListItem();
            if (!bMandatory)
            {
                oItem.Value = "0";
                oItem.Text = zeroIndexString;
            }
            dropDownList.Items.Insert(0, oItem);
        }
    }

    public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, DataTable dataTbl, bool bHasBlank, bool bMandatory)
    {
        dropDownList.DataTextField = dataTextField;
        dropDownList.DataValueField = dataValueField;
        dropDownList.DataSource = dataTbl;
        dropDownList.DataBind();

        if (bHasBlank)
        {
            ListItem oItem = new ListItem();
            if (!bMandatory)
            {
                oItem.Value = "0";
                oItem.Text = "Select";
            }
            dropDownList.Items.Insert(0, oItem);
        }
    }

    public static void FillCombo(DropDownList dropDownList, string[] sArray, bool bHasBlank, bool bMandatory)
    {
        dropDownList.Items.Clear();
        ListItem oItem = new ListItem();

        if (bHasBlank)
        {
            if (!bMandatory)
            {
                oItem.Value = "0";
                oItem.Text = "ALL";
            }
            dropDownList.Items.Add(oItem);
        }

        for (int i = 0; i <= sArray.GetUpperBound(0); i++)
        {
            oItem = new ListItem();
            oItem.Text = sArray[i];
            oItem.Value = Convert.ToString(i + 1);
            dropDownList.Items.Add(oItem);
        }
    }


    public static Int64 GetComboValue64(DropDownList dropDownList)
    {
        if (dropDownList.SelectedIndex == -1 || dropDownList.SelectedItem.Value == null || dropDownList.SelectedItem.Value == "0" || dropDownList.SelectedItem.Value == string.Empty)
            return 0;
        else
            return Convert.ToInt64(dropDownList.SelectedValue);
    }

    public static Int32 GetComboValue32(DropDownList dropDownList)
    {
        if (dropDownList.SelectedIndex == -1 || dropDownList.SelectedItem.Value == null || dropDownList.SelectedItem.Value == string.Empty)
            return 0;
        else
            return Convert.ToInt32(dropDownList.SelectedItem.Value);
    }

    public static string GetComboText(DropDownList dropDownList)
    {
        if (dropDownList.SelectedIndex == -1 || dropDownList.SelectedItem.Text == null || dropDownList.SelectedItem.Text == string.Empty)
            return "";
        else
            return dropDownList.SelectedItem.Value;
    }

    public static void AssignRowsBasicData(ref object obj, bool bIsNew)
    {
        //        if(obj is 
        //obj.ModifiedBy = UIUtility.GetCurrentUser(User.Identity.Name);
        //obj.ModifiedOn = System.DateTime.Now;

        //if (bIsNew)
        //{
        //    obj.CreatedOn = System.DateTime.Now;
        //    obj.CreatedBy = UIUtility.GetCurrentUser(User.Identity.Name);
        //}
    }

    //Returns the id who has already loged in to the admin
    //public static Int64 GetCurrentUser()
    //{
    //    return CurrentAdminUserId;                
    //}

    //Returns the id who has already loged in client site
    //public static Int64 GetCurrentSiteUser()
    //{
    //    return CurrentUserId;
    //}


    public static DateTime GetCurrentDateTime()
    {
        return System.DateTime.Now;
    }

    public static string GetUploadedFileName(string file)
    {
        if (file == null || file == "")
            return "";

        if (file.LastIndexOf("\\") + 1 < file.Length)
        {
            return file.Substring(file.LastIndexOf("\\") + 1);
        }

        return "";
    }

    public static string FormatDate(object dt)
    {
        if (dt == null)
            return "";

        DateTime dtn = Convert.ToDateTime(dt);

        return dtn.ToString("dd MMM yyyy");
    }

    public static string FormatDecimal(object d)
    {
        if (d == null)
            return "";

        Decimal dn = Convert.ToDecimal(d);

        return dn.ToString("f2");
    }

    public static string FormatInteger(object i)
    {
        if (i == null)
            return "";

        Int32 iNew = Convert.ToInt32(i);

        return iNew.ToString();
    }

    public static string FormatLong(object l)
    {
        if (l == null)
            return "";

        Int64 lNew = Convert.ToInt64(l);

        return lNew.ToString();
    }

    public static string FormatString(object s)
    {
        if (s == null)
            return "";

        return s.ToString();
    }

    //// We are actually using this SetToolbar methods from different pages
    //public static void SetToolbar(int oType, long oId, int pType, long pId, Menu mnu, string menuType)
    //{
    //    DataTable dt = null;
    //    MenuItem pmi = null;

    //    string genericToolbar = "ObjectType=0 AND MenuType='" + menuType + "'";
    //    string speceficToolbar = "ObjectType=" + oType.ToString() + " AND MenuType='" + menuType + "'";

    //    dt = DataRepository.SysmenuProvider.Find(speceficToolbar).ToDataSet(false).Tables[0];

    //    if (dt.Rows.Count == 0)
    //    {
    //        dt = DataRepository.SysmenuProvider.Find(genericToolbar).ToDataSet(false).Tables[0];
    //    }

    //    if (mnu.Items.Count > 0)
    //    {
    //        mnu.Items.Clear();
    //    }

    //    // add the first menu seperator image by default
    //    pmi = new MenuItem("", "", "~/Images/MenuBullet.gif", "");
    //    pmi.Selectable = false;
    //    mnu.Items.Add(pmi);

    //    // add menu items
    //    DataView dvParent = dt.DefaultView;
    //    foreach (DataRowView pdrv in dvParent)
    //    {
    //        pmi = new MenuItem(pdrv["Text"].ToString(), pdrv["Value"].ToString(), pdrv["ImageUrl"].ToString(), pdrv["NavigateUrl"].ToString());
    //        pmi.SeparatorImageUrl = pdrv["SeperatorImageUrl"].ToString();
    //        pmi.ToolTip = pdrv["ToolTip"].ToString();
    //        mnu.Items.Add(pmi);
    //    }
    //}

    //// We are actually using this message methods from different pages
    //public static void ShowUIMessage(Label lblMsg, Image imgMsg, Panel pnlMsg, string msg, int type)
    //{
    //    switch (type)
    //    {
    //        case 1:
    //            lblMsg.Text = "Error: " + msg;
    //            imgMsg.ImageUrl = "~/Images/error.gif";
    //            pnlMsg.CssClass = "lErrorMsg";
    //            break;
    //        case 2:
    //            lblMsg.Text = "Warning: " + msg;
    //            imgMsg.ImageUrl = "~/Images/warning.gif";
    //            pnlMsg.CssClass = "lWarningMsg";
    //            break;
    //        case 3:
    //            lblMsg.Text = msg;
    //            imgMsg.ImageUrl = "~/Images/info.gif";
    //            pnlMsg.CssClass = "lInfoMsg";
    //            break;
    //        case 4:
    //            lblMsg.Text = msg;
    //            imgMsg.ImageUrl = "";
    //            pnlMsg.CssClass = "lNoMsg";
    //            break;
    //    }
    //}

    //public static void ShowUIMessage(Label lblMsg, Image imgMsg, Panel pnlMsg, string msg, int type, Exception ex)
    //{
    //    if (ex is SqlException)
    //    {
    //        switch (((SqlException)ex).Number)
    //        {
    //            case 547:
    //                // ForeignKey Violation
    //                msg = "You can not delete this record since there exits one or more dependent record(s).";
    //                break;
    //            case 2627:
    //                // Unique Index/Constriant Violation
    //                msg = "A record with the same key already exists.";
    //                break;
    //            case 2601:
    //                // Unique Index/Constriant Violation
    //                msg = "A record with the same key already exists.";
    //                break;
    //        }
    //    }

    //    ShowUIMessage(lblMsg, imgMsg, pnlMsg, msg, type);
    //}

    public static string GetSQLDate(string sText, bool bCol)
    {
        if (bCol)
            return "Convert(datetime,Convert(char(12)," + sText + ",107))";
        else
            return "Convert(datetime,Convert(char(12),'" + Convert.ToDateTime(sText).ToString("dd MMM yyyy") + "',107))";
    }

    public static string EncryptPassword(string password)
    {
        UTF8Encoding UT = new UTF8Encoding();
        MD5 md = MD5.Create();

        byte[] clearPassword = UT.GetBytes(password);
        byte[] encryptedPassword = md.ComputeHash(clearPassword);

        return UT.GetString(encryptedPassword);
    }

    //public static string[] GetPreferenceEmails()
    //{
    //    string[] strAddres = new string[5];
    //    try
    //    {
    //        PreferenceEmail email = new PreferenceEmail();
    //        email = DataRepository.PreferenceEmailProvider.GetById(1);
    //        if (email == null)
    //        {
    //            strAddres[0] = "mail.fountaincosmetics.com.au"; // 0 for Mail server IP.
    //            strAddres[1] = "info@fountaincosmetics.com.au"; // 1 for Mail from or SMTPFrom

    //        }
    //        else
    //        {

    //            strAddres[0] = email.MailServerIp; // 0 for Mail server IP.
    //            strAddres[1] = email.MailFrom; // 1 for Mail from or SMTPFrom
    //            strAddres[2] = email.MailTo; // 2 for Mail to 
    //            strAddres[3] = email.Cc; // 3 for CC
    //            strAddres[4] = email.Bcc; // 4 for BCC
    //        }
    //    }
    //    catch (Exception ex) { throw ex; }

    //    return strAddres;
    //}

    /// <summary>
    /// Load datatable with year
    /// </summary>
    /// <param name="ddlYear"> </param>
    /// <param name="intCounter"></param>    
    /// <returns></returns>

    public static void LoadYearCombo(DropDownList ddlYear, int intCounter, string strFirstSelectedText, bool isFirstValueNull)
    {
        try
        {
            DataTable dtYear = new DataTable();
            DataRow drValue = null;
            int intIndex = 1;

            dtYear.Columns.Add(new DataColumn("value"));
            dtYear.Columns.Add(new DataColumn("data"));


            for (int i = 0; i < intCounter + 2; i++)
            {



                if (i == 0)
                {
                    if (strFirstSelectedText.Length > 0)
                    {
                        drValue = dtYear.NewRow();
                        drValue["value"] = "-1";
                        drValue["data"] = strFirstSelectedText;
                        dtYear.Rows.Add(drValue);
                    }
                    if (isFirstValueNull)
                    {
                        drValue = dtYear.NewRow();
                        drValue["value"] = "-1";
                        drValue["data"] = "";
                        dtYear.Rows.Add(drValue);
                    }

                }
                else if (i == 1)
                {
                    drValue = dtYear.NewRow();
                    string strYear = DateTime.Now.ToString("yyyy");
                    drValue["value"] = strYear.Substring(strYear.Length - 2).ToString();
                    drValue["data"] = strYear;
                    dtYear.Rows.Add(drValue);
                }
                else
                {
                    drValue = dtYear.NewRow();
                    string strYear = DateTime.Now.AddYears(intIndex).ToString("yyyy");
                    drValue["value"] = strYear.Substring(strYear.Length - 2).ToString();
                    drValue["data"] = strYear;
                    dtYear.Rows.Add(drValue);
                    intIndex++;
                }

            }
            intIndex = 1;

            ddlYear.DataSource = dtYear;
            ddlYear.DataTextField = "data";
            ddlYear.DataValueField = "value";
            ddlYear.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public static void DisplayCartData(DataTable ShoppingCart, Label lblQty)
    {
        decimal nItem = 0;
        decimal nTotAmount = 0;
        for (int i = 0; i < ShoppingCart.Rows.Count; i++)
        {
            nItem = nItem + Convert.ToDecimal(ShoppingCart.Rows[i]["Qty"]);
            nTotAmount = nTotAmount + Convert.ToDecimal(ShoppingCart.Rows[i]["Qty"]) * Convert.ToDecimal(ShoppingCart.Rows[i]["Price"]);
        }
        lblQty.Text = nItem.ToString();
        //lblAmount.Text = nTotAmount.ToString();
    }
}
