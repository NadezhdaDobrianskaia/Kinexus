using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class AdUploader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpPostedFile imageFile =Request.Files["ad"];

            if (imageFile.ContentType.StartsWith("image") && (Boolean)Session["AdminUpload"])
            {
                imageFile.SaveAs("/Ads/" + imageFile.FileName);
            }
            else
            {
                output.Text += "<meta http-equiv=\"refresh\" content=\"0;url=/\">";
            }
        }
    }
}