using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Collections.Specialized;

namespace Webpage.Ads
{
    public partial class AdRotator : System.Web.UI.UserControl
    {
        private StringCollection fileEntries;//AN array of files in the Ads dir
        public int MaxWidth//max width of an ad to be server //will resize the width to this size 
        {
            get
            {
                return (int)ViewState["MaxWidth"];
            }
            set
            {
                ViewState["MaxWidth"] = value;
            }
        }

        public int MaxHeight//max Height of an ad to be server //will resize the height to this size 
        {
            get
            {
                return (int)ViewState["MaxHeight"];
            }
            set
            {
                ViewState["MaxHeight"] = value;
            }
        }
        public  AdRotator(){
            ViewState.Add("MaxHeight" ,  300);
            ViewState.Add("MaxWhidth", 150);
    
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //hard coded defaults this is a fail safe if thire is not infoefile and now max width or hight theas vause are used 
            string url = "http://www.kinexus.ca/ourServices/ourServices.html";
            string alt = "AD";
            int width  =  300;
            int height =  150;

            int adId = -1;//Var to hold the id of the ad to be server // -1 is an invaid sate 
            if (Application["AdList"] == null)//is the list of ad cahsed in mem
            {
                StringCollection Adlist = new StringCollection();
                String[] list = Directory.GetFiles(this.Request.PhysicalApplicationPath + "\\Ads\\");
                for(int i=0 ; i < list.Length ; i++ ){
                    FileInfo info  = new FileInfo(list[i]);
                    if(info.Extension != ".txt"){
                        Adlist.Add(list[i]);
                    }
                }
                Application.Add("AdList", Adlist);//crate new cach
                
                
            }
            if (Session["adID"] == null)//dose the current user have a adID session
            {
                Session.Add("adID", -1);//crate a new addID session for the crent user 
            }
            fileEntries = (StringCollection)Application["AdList"];//read the adlist cash
            adId =(int)Session["AdID"] ;//get the adId of the next ad to server 
            adId++;
            if (adId == -1)//is the ad invalid
            {
                return;//no ad can be printed
            }
            if (adId == fileEntries.Count)//if thi add is out if the vaid range
            {
                adId = 0;
            }
          
            FileInfo fileInfo  = new FileInfo(fileEntries[adId]);//get the file with out a path
 
            String infoFileName = fileInfo.FullName + ".txt";//get the information file name
            if (File.Exists(infoFileName))//make sure the info file exists 
                    {
                        StreamReader read = new StreamReader(infoFileName);//read info file 
                         url =  read.ReadLine();//read the first line containg the url 
                         alt = read.ReadLine();//read the second line containg the alt tag
                         width =  Convert.ToInt32(read.ReadLine());//read the third line conating 
                         height = Convert.ToInt32(read.ReadLine());//read the last line containg the height
                        
                    }
            if(width > MaxWidth){
                width =MaxWidth;
            }
            if(height > MaxHeight){
                height  =MaxHeight;
            }
            //is the add an image 
            if (fileInfo.Extension != ".swf")
            {
                if (fileInfo.Extension != ".txt")//ingnoe the info file 
                {
                    adHTML.Text += "<div id=\"ad\">\n";
                    adHTML.Text += "<a href=\"" + url + "\" >";
                    adHTML.Text += "<img alt=\"" + alt + "\" width=\"" + width + "\" height=\"" + height + "\" src=\"/Ads/" + fileInfo.Name + "\" />";
                    adHTML.Text += "</a></div>";
                }
            }else{//must be a flash add
                if (fileInfo.Extension != ".txt")//ingnoe the info file 
                {
                        adHTML.Text += "<a href=\"" + url + "\" >";
                        adHTML.Text += "<div id=\"ad\" >\n";
                        adHTML.Text += "</div></a>";
                        adHTML.Text += " <script type=\"text/javascript\">\n";
                        adHTML.Text += "swfobject.embedSWF(\"/Ads/" + fileInfo.Name + "\", \"ad\", \""+width+"\", \""+height+"\", \"9.0.0\")\n";
                        adHTML.Text += "</script>\n";
                }
            }
            Session["AdID"] = adId;//save the id of the ad just served in the session 
        }
    }

}