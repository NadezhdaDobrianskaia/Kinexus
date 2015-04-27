using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database.Styles
{
    public partial class AdAdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if we are not authenticated logout.
            if (!Request.IsAuthenticated)
            {
                //if not authenticated go to login page
                Response.Redirect("Login.aspx");
            }
        }
    }
}