using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Product_Database
{
    public partial class AdAdmin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //was an image sleected for upload
            if (ad.HasFile)
            {
                try
                {
                    if (ad.PostedFile.ContentType.StartsWith("image/") || ad.PostedFile.ContentType == "application/x-shockwave-flash")//makes only images and flash ad can be uploaded 
                    {
                        if (ad.PostedFile.ContentLength < 2097152)//set a file limt of 2mb 
                        {
                            string filename = Path.GetFileName(ad.FileName);//get the file name 
                            ad.SaveAs(Server.MapPath("~/Ads/") + filename);//save the new image to the Ads folder
                            serverRespons.Text = "Upload status: File uploaded!";//set the satus text 

                            TextWriter tw = new StreamWriter(Server.MapPath("~/Ads/" + ad.FileName + ".txt"));//creat the info file 
                            tw.WriteLine(url.Text);
                            tw.WriteLine(alt.Text);
                            tw.WriteLine(width.Text);
                            tw.WriteLine(height.Text);
                            tw.Close();
                        }
                        else
                            serverRespons.Text = "Upload status: The file has to be less than 2 Mb!";
                    }
                    else
                        serverRespons.Text = "Upload status: Only Image files are accepted!";
                }
                catch (Exception ex)
                {
                    serverRespons.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

        }
    }
}