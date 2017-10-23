using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBLUSER;
using System.Data;

public partial class LogIn : System.Web.UI.Page
{
    #region Properties
    public OBLAPP oOBLAPP = null;
    DataTable UserDt = new DataTable();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
          oOBLAPP = new OBLAPP();
            try
            {
                               
                string Status = oOBLAPP.GetByUserIDCheck(this.txtEmpId.Text, this.txtPassword.Text);

                if (Status == "NotFound")
                {
                    lblMsg.Text = "Employee ID is invalid.";
                }
                else if (Status == "Invalid")
                {

                    lblMsg.Text = "Password is invalid.";

                }
                else if (Status == "Valid")
                {
                    UserDt = oOBLAPP.GetByUserID(txtEmpId.Text); 
                    Session["CurrentUser"] = this.txtEmpId.Text;
                    Session["CurrentUserId"] = UserDt.Rows[0]["UserID"].ToString();                    
                    Session["CurrentBranchCode"] = UserDt.Rows[0]["BranchCode"].ToString();
                    Session["CurrentUserName"] = UserDt.Rows[0]["Name"].ToString();
                    Session["CurrentBranchName"] = UserDt.Rows[0]["BranchName"].ToString();
                    Session["CurrentDept"] = UserDt.Rows[0]["PreDeptName"].ToString();
                    Session["CurrentDesgn"] = UserDt.Rows[0]["PreDesgName"].ToString();
                    Response.Redirect("~/Dashboard.aspx");
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
  
    }
}