using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OBL_Screening;
using OBLUSER;
using System.Data;
namespace OBL_Screening
{

    public partial class _Default : BasePage
    {
        #region Properties
        public OBLAPP oOBLAPP = null;
        DataTable UserDt = new DataTable();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

                if (CheckEndSession())
                    Response.Redirect("../LogIn.aspx");
                else
                {
                    Response.Redirect("~/LogIn.aspx");
                }

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
          
        }
    }
}
