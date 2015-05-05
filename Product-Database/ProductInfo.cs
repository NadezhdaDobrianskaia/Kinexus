using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using Webpage.Ads;
using System.IO;
using System.Collections;
using System.Collections.Specialized;


namespace Product_Database
{
    /// <summary>
    /// ProductInfo is the parent of all info page and contains HTML rendering logic  
    /// </summary>
    public class ProductInfo : System.Web.UI.Page
    {
        private SqlDataReader reader;
        public SqlDataReader Reader { get { return reader; } }
        protected SqlParameter productIDFilter;
        public enum columSize { mini, med, full }
        private int htmlBufferLines;
        public const int MAX_CAHR_PER_LINE = 40;
        public const int MAX_LINE_PER_MED_COLUM = 28;
        public const int MAX_LINE_PER_COLUM = 28;
        public const int MAX_MINI_COLUM = 3;
        public const int MAX_MED_COLUM = 3;
        private int columcount;

        public ArrayList sizes = new ArrayList();

        public SqlConnection connection;
        public const string DATA_TABLE_HTML = "<table class=\"dataTable\">";
        protected StringBuilder outputHTML;
        protected StringBuilder HTMLBuffer;
        /// <summary>
        /// Loads product data and instatist the object 
        /// </summary>
        /// <returns>True if data was load and false if any exceptions where encountered during the read </returns>
        public Boolean loadData()
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["comp4900ConnectionString"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT DISTINCT * FROM [ProductDB] WHERE ([Product_Number] = @Product_Number)", connection);
                productIDFilter = new SqlParameter();
                productIDFilter.ParameterName = "@Product_Number";
                productIDFilter.Value = Request.QueryString["Product_Number"];
                command.Parameters.Add(productIDFilter);
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                outputHTML = new StringBuilder();
                columcount = 0;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Builds Columns in the outputHTML buffer
        /// Should always call  HtmlBufferFlush() after use;
        /// </summary>
        /// <param name="infoToAdd"></param>
        /// <param name="size"></param>
        public void ColumBuilder(string infoToAdd, bool extraBlockSpace = true)
        {
            int newNumberOfLines = 0;
            int stringSize = CalcNumberOfLines(infoToAdd);
            columSize size = getNextColSize();
            if (HTMLBuffer == null)
            {
                HTMLBuffer = new StringBuilder();
                HTMLBuffer.Append(BuildOpeningColumHTML());
                htmlBufferLines = 0;

            }
            //Check if the string size is longer than maximum allowed for one column
            if (stringSize > MAX_LINE_PER_MED_COLUM || (stringSize > MAX_LINE_PER_COLUM && size == columSize.full))
            {
                //If we are adding the "Scientific Backgroud field"
                if (infoToAdd.Contains("Scientific Background:"))
                {
                    //Make the fiels appear in smaller font size
                    infoToAdd = "<span style=\"font-size:11px; color:white;\">" + infoToAdd + "</span>";
                }
                else //If we are adding other database field
                {
                    //if the column is full
                    if (size == columSize.full)
                    {
                        infoToAdd = infoToAdd.Substring(0, MAX_LINE_PER_COLUM * MAX_CAHR_PER_LINE);
                    }
                    else
                    {
                        infoToAdd = infoToAdd.Substring(0, MAX_LINE_PER_MED_COLUM * MAX_CAHR_PER_LINE);
                    }
                }
            }
            //update number of lines
            newNumberOfLines = htmlBufferLines + CalcNumberOfLines(infoToAdd);

            //if we are adding the "Scientific Background" field and it exceeds the maximum size allowed per one column 
            if (infoToAdd.Contains("Scientific Background:") && ((newNumberOfLines > MAX_LINE_PER_COLUM && size == columSize.full) || newNumberOfLines > MAX_LINE_PER_MED_COLUM))
            {
                //add data to the new column
                HTMLBuffer.Append(infoToAdd);
                //close the column
                HtmlBufferFlush();
                //update new number of lines
                newNumberOfLines = htmlBufferLines + CalcNumberOfLines(infoToAdd);
            }
            else
            {
                //if the number of lines we are adding is less or equal to maximum allowed per one column
                if (newNumberOfLines <= MAX_LINE_PER_MED_COLUM || (newNumberOfLines <= MAX_LINE_PER_COLUM && size == columSize.full))
                {
                    //add new data
                    HTMLBuffer.Append(infoToAdd);

                    //if the field that we've added is "Product Use" or "Protein SigNET", the next field to add will be the "Scientific Background", and the "Scientific Backgroud" fiels exceeds the maximum allowed size per one column
                    if ((infoToAdd.Contains("Product Use:") || infoToAdd.Contains("Protein SigNET:")) && (!String.IsNullOrWhiteSpace(GetDBValue("Scientific_Background"))))
                    {
                        //close current column
                        HtmlBufferFlush();
                    }
                    else
                    {
                        //if there is extra space in the column and the number of lines we are adding does not exceed maximum number of lines allowed
                        if (extraBlockSpace && (newNumberOfLines != MAX_LINE_PER_MED_COLUM || (newNumberOfLines != MAX_LINE_PER_COLUM && size == columSize.full)))
                        {
                            //append line breaks
                            HTMLBuffer.Append("<br /><br />");
                            //increment number of lines so it includes html line breaks
                            newNumberOfLines++;
                        }
                    }
                    htmlBufferLines = newNumberOfLines;
                }
                else
                {
                    HtmlBufferFlush();
                    ColumBuilder(infoToAdd);
                }
            }

        }
        /// <summary>
        /// Calculate the number of line a string will take to display 
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        protected int CalcNumberOfLines(string aString)
        {
            int res;
            aString = Regex.Replace(aString, @"<(.|\n)*?>", string.Empty);
            res = (int)Math.Ceiling
            ((decimal)aString.Length / (decimal)MAX_CAHR_PER_LINE);
            if (res > 1)
            {
                return res;
            }
            return 1;

        }
        /// <summary>
        /// Force the closing of a column 
        /// </summary>
        public void HtmlBufferFlush()
        {
            if (HTMLBuffer != null)
            {
                htmlBufferLines = 0;
                HTMLBuffer.Append(BuildClosingColumHTML());
                outputHTML.Append(HTMLBuffer.ToString());
                HTMLBuffer = null;
            }
        }
        /// <summary>
        /// crate the HTML for the opening of the colum
        /// </summary>
        /// <returns></returns>
        private string BuildOpeningColumHTML()
        {
            columcount++;
            columSize size = getNextColSize();
            string columTypeString;
            string columSizetring = "";
            if (size != columSize.full)
            {
                columSizetring = " " + size.ToString() + "Column ";
            }
            columTypeString = "column" + columSizetring;
            return "<div class=\"" + columTypeString + "\" >";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildClosingColumHTML()
        {
            string HTML = "</div>";
            if (columcount % 3 == 0 && columcount > 3)
            {
                HTML += "\n <div class=\"page-preak\"></div> \n";
            }
            return HTML;
        }
        /// <summary>
        /// 
        /// </summary>
        public void BuildCommonFirstRow()
        {
            StringBuilder HTML = new StringBuilder();
            HTML.Append(BuildOpeningColumHTML());
            HTML.Append("<span class=\"nameSpan\" >");
            HTML.Append(BuildBlueSpan("Product Name: "));
            HTML.Append("<span class=\"orangeLabel\" >");
            HTML.Append(GetDBValue("Product_Name_Short"));
            HTML.Append("</span>");
            HTML.Append("</span>");
            HTML.Append(BuildClosingColumHTML());

            HTML.Append(BuildOpeningColumHTML());
            HTML.Append("<span class=\"nameSpan\" >");
            HTML.Append(BuildBlueSpan("Product Number: "));
            HTML.Append("<span class=\"orangeLabel\" >");
            HTML.Append(GetDBValue("Product_Number"));
            HTML.Append("</span>");
            HTML.Append("</span>");
            HTML.Append(BuildClosingColumHTML());

            HTML.Append(BuildPriceTable());

            outputHTML.Append(HTML.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        public void BuildFiquersHTML()
        {
            //Check is there is at least one image 
            string image, txt;
            bool images = false;
            for (int i = 1; i <= 12; i++)
            {
                image = GetDBValue("Image" + i + "_File");
                txt = GetDBValue("Image" + i + "_Legend");
                if (!String.IsNullOrWhiteSpace(image) && !String.IsNullOrWhiteSpace(txt))
                {
                    images = true;
                }
            }
            if (images)
            {
                StringBuilder HTML = new StringBuilder();
                HTML.Append(BuildOpeningColumHTML());
                string img, text;
                int count = 0;
                for (int i = 1; i <= 12; i++)
                {
                    img = GetDBValue("Image" + i + "_File");
                    text = GetDBValue("Image" + i + "_Legend");
                    if (!String.IsNullOrWhiteSpace(img) && !String.IsNullOrWhiteSpace(text))
                    {

                        if (count % 2 == 0 && count != 12 && count != 0)
                        {
                            HTML.Append(BuildClosingColumHTML());
                            HTML.Append(BuildOpeningColumHTML());
                        }
                        count++;
                        HTML.Append("<a href=\"../Images/images/" + img + "\" target=\"_blank\" rel=\"shadowbox\"><img src=\"../Images/images/" + img + "\" height=\"130px\" width=\"275px\" / ></a>" + text + "<br/><br/>");

                    }


                }
                HTML.Append(BuildClosingColumHTML());
                outputHTML.Append(HTML.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BuildreferencesHTML()
        {
            string url, text;
            HtmlBufferFlush();
            for (int i = 1; i <= 12; i++)
            {
                text = GetDBValue("Product_Citation_" + i);
                url = GetDBValue("Product_Citation_" + i + "_url");

                if (!String.IsNullOrWhiteSpace(url) && !String.IsNullOrWhiteSpace(text))
                {
                    if (i == 1)
                    {
                        ColumBuilder("<span >References</span>");
                    }
                    ColumBuilder(BuildBlueSpan("[" + i + "]") + "  <a href=\"" + url + "\" target=\"_blank\">" + text + "</a>");
                }

            }
            HtmlBufferFlush();
        }
        /// <summary>
        /// Builds the HTML for the TargetLinks and adds it to the buffer
        /// </summary>
        /// <param name="buffFlushOnStrat"></param>
        public void BuildTargetLinksHTML(bool buffFlushOnStrat = false)
        {
            if (buffFlushOnStrat)
            {
                HtmlBufferFlush();
            }

            string UniProtUrl, UniProtText, url, text;
            for (int i = 1; i <= 12; i++)
            {
                UniProtText = GetDBValue("Target" + i + "_UniProt");
                UniProtUrl = GetDBValue("Target" + i + "_UniProt_url");
                text = GetDBValue("Target" + i + "_SigNET_Link");
                url = GetDBValue("Target" + i + "_SigNET_url");
                if (!String.IsNullOrWhiteSpace(UniProtText) && !String.IsNullOrWhiteSpace(UniProtUrl))
                {

                    ColumBuilder(BuildBlueSpan("Target " + i + " UniProt Link: ") + BuildLinkHtml(UniProtUrl, UniProtText));
                }

                if (!String.IsNullOrWhiteSpace(url) && !String.IsNullOrWhiteSpace(text))
                {
                    ColumBuilder(BuildBlueSpan("Target " + i + " SigNET Link: ") + BuildLinkHtml(url, text));
                }

            }
            HtmlBufferFlush();
        }
        /// <summary>
        /// Builds the price table and it's outer column HTML 
        /// </summary>
        /// <returns>The HTML</returns>
        private string BuildPriceTable()
        {
            string priceText = "Price:";
            string sizeText = "Size:";
            StringBuilder HTML = new StringBuilder();
            HTML.Append(BuildOpeningColumHTML());
            HTML.Append(DATA_TABLE_HTML);

            ArrayList size = new ArrayList();
            if (!String.IsNullOrWhiteSpace(getPackSizeInfo("Small")) && !String.IsNullOrWhiteSpace(getPackPriceInfo("Small")) && getPackSizeInfo("Small") != "N/A" && getPackSizeInfo("Small") != "n/a" && getPackPriceInfo("Small") != "N/A" && getPackPriceInfo("Small") != "n/a")
            {
                string smallPrice = String.Format("{0:0.00}", Convert.ToDouble(getPackPriceInfo("Small")));
                HTML.Append("<tr><td>" + sizeText + "</td><td ><span class=\"grayText\"> " + getPackSizeInfo("Small") + "</span></td><td>&#160;&#160;&#160;&#160;&#160;&#160;</td><td>" + priceText + "</td><td><span class=\"grayText\">" + smallPrice + "</span></td></tr>");
                sizeText = "";
                priceText = "$US";
                sizes.Add(getPackSizeInfo("Small"));
                sizes.Add("Small");
            }
            if (!String.IsNullOrWhiteSpace(getPackSizeInfo("Medium")) && !String.IsNullOrWhiteSpace(getPackPriceInfo("Medium")) && getPackSizeInfo("Medium") != "N/A" && getPackSizeInfo("Medium") != "n/a" && getPackPriceInfo("Medium") != "N/A" && getPackPriceInfo("Medium") != "n/a")
            {
                string medPrice = String.Format("{0:0.00}", Convert.ToDouble(getPackPriceInfo("Medium")));
                HTML.Append("<tr><td>" + sizeText + "</td><td><span class=\"grayText\">" + getPackSizeInfo("Medium") + "</span></td><td>&#160;&#160;&#160;&#160;&#160;&#160;</td><td>" + priceText + "</td><td ><span class=\"grayText\">" + medPrice + "</span></td></tr>");
                sizeText = "";
                priceText = "$US";
                sizes.Add(getPackSizeInfo("Medium"));
                sizes.Add("Medium");
            }
            else
            {
                priceText = "$US";
                HTML.Append("<tr><td>" + sizeText + "</td><td><span class=\"grayText\"></span></td><td>&#160;&#160;&#160;&#160;&#160;&#160;</td><td>" + priceText + "</td><td ><span class=\"grayText\"></span></td></tr>");

            }
            if (!String.IsNullOrWhiteSpace(getPackSizeInfo("Large")) && !String.IsNullOrWhiteSpace(getPackPriceInfo("Large")) && getPackSizeInfo("Large") != "N/A" && getPackSizeInfo("Large") != "n/a" && getPackPriceInfo("Large") != "N/A" && getPackPriceInfo("Large") != "n/a")
            {
                string largePrice = String.Format("{0:0.00}", Convert.ToDouble(getPackPriceInfo("Large")));
                if (priceText == "$US")
                {
                    priceText = "";
                }
                else
                {
                    priceText += " $US";
                }

                HTML.Append("<tr><td>" + sizeText + "</td><td ><span class=\"grayText\">" + getPackSizeInfo("Large") + "</span></td><td>&#160;&#160;&#160;&#160;&#160;&#160;</td><td>" + priceText + "</td><td><span class=\"grayText\">" + largePrice + "</span></td></tr>");
                sizes.Add(getPackSizeInfo("Large"));
                sizes.Add("Large");
            }
            HTML.Append("</table>");
            HTML.Append(BuildClosingColumHTML());

            return HTML.ToString();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string getPackSizeInfo(string size)
        {
            string info = GetDBValue("Pack_Size_" + size);
            if (String.IsNullOrWhiteSpace(info))
            {
                info = GetDBValue("MPack_Size_" + size);
            }
            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string getPackPriceInfo(string size)
        {
            string info = GetDBValue("Price_Size_" + size);
            if (String.IsNullOrWhiteSpace(info))
            {
                info = GetDBValue("Pack_Price_" + size);
            }
            return info;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string BuildLinkHtml(string url, string text)
        {
            return "<a  href=\"" + url + "\" target=\"_blank\" >" + text + " </a>";
        }
        protected string BuildWhiteLinkHtml(string url, string text)
        {
            return "<a class\"whieLink\" href=\"" + url + "\" target=\"_blank\">" + text + " </a>";
        }
        /// <summary>
        /// Wraps text in a span tag with a CSS class of bluelable
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        protected string BuildBlueSpan(string aString)
        {
            return "<span class=\"bluelable\" >" + aString + "</span>";
        }
        /// <summary>
        /// Builds the ad column and add the html in to the HTML Buffer
        /// </summary>
        protected void BuildAdColum()
        {
            Page pageHolder = new Page();
            //outputHTML.Append(BuildOpeningColumHTML());
            AdRotator ad = (AdRotator)LoadControl("AdRotator.ascx");
            ad.MaxWidth = 295;
            ad.MaxHeight = 100;
            pageHolder.Controls.Add(ad);
            StringWriter result = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, result, false);
            outputHTML.Append(result.ToString());
            //outputHTML.Append(BuildClosingColumHTML());


        }

        /// <summary>
        /// Creates an add to be added to a control
        /// </summary>
        /// <returns></returns>
        protected void BuildAdvertisement(Control control)
        {
            AdRotator ad = (AdRotator)LoadControl("AdRotator.ascx");
            ad.MaxWidth = 295;
            ad.MaxHeight = 100;
            control.Controls.Add(ad);
            Page pageHolder = new Page();
            pageHolder.Controls.Add(ad);
            StringWriter result = new StringWriter();
            HttpContext.Current.Server.Execute(pageHolder, result, false);
            outputHTML.Append(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        protected void FillExtarColums()
        {
            int extracols = (columcount) % 3;//add one for the add column 
            if (extracols != 0)
            {
                extracols = 3 - extracols;
                for (; extracols > 0; extracols--)
                {
                    outputHTML.Append(BuildOpeningColumHTML());
                    outputHTML.Append(BuildClosingColumHTML());
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labeText"></param>
        /// <param name="text"></param>
        public void AddLableInofPairToColum(string labeText, string text)
        {
            if (!String.IsNullOrWhiteSpace(labeText) && !String.IsNullOrWhiteSpace(text))
            {
                ColumBuilder(BuildBlueSpan(labeText) + text);
            }
        }


        public void AddLableLinkPairToColum(string labeText, string url, string text)
        {
            if (!String.IsNullOrWhiteSpace(labeText) && !String.IsNullOrWhiteSpace(text) && !String.IsNullOrWhiteSpace(url))
            {
                ColumBuilder(BuildBlueSpan(labeText) + BuildWhiteLinkHtml(url, text));
            }
        }
        /// <summary>
        /// Try to get a string value from the db 
        /// </summary>
        /// <param name="column">the DB column name</param>
        /// <param name="returnString">the string to be returned if an exception is encountered. Default value is null</param>
        /// <returns>the value if the column or returnString </returns>
        public string GetDBValue(string column, string returnString = null)
        {
            try
            {

                return Convert.ToString(reader[column]);
            }
            catch (Exception e)
            {
                return returnString;
            }
            return returnString;
        }
        /// <summary>
        /// gets the size of column being built
        /// </summary>
        /// <returns></returns>
        private columSize getNextColSize()
        {
            if (columcount <= 3)
            {
                return columSize.mini;
            }
            else if (columcount <= 6)
            {
                return columSize.med;
            }
            else
            {
                return columSize.full;
            }
        }




        /*
         * 
         * Shopping Cart
         * 
         */
        protected void addProd(object sender, EventArgs e)
        {
            ArrayList cart = new ArrayList();
            ArrayList item = this.getCart();
            ArrayList products = this.getProducts();
            string prodName = (GetDBValue("Product_Name_Short"));
            string prodID = (GetDBValue("Product_Number"));
            string size = null, price = null;
            string qty = "1";
            int index = -1;

            System.Web.UI.WebControls.LinkButton lbtn = (System.Web.UI.WebControls.LinkButton)sender;
            string id = lbtn.ID;
            if (id == "LinkButton1")
            {
                size = (getPackSizeInfo("Small"));
                price = String.Format("{0:0}", Convert.ToDouble(getPackPriceInfo("Small")));

            }

            if (id == "LinkButton2")
            {
                size = (getPackSizeInfo("Medium"));
                price = String.Format("{0:0}", Convert.ToDouble(getPackPriceInfo("Medium")));
            }

            if (id == "LinkButton3")
            {
                size = (getPackSizeInfo("Large"));
                price = String.Format("{0:0}", Convert.ToDouble(getPackPriceInfo("Large")));
            }


            if (item.Count > 0)
            {
                foreach (ArrayList product in item)
                {
                    if (product.Contains(prodID) && product.Contains(size))
                    {
                        cart = new ArrayList();
                        cart.Add(prodName);
                        cart.Add(prodID);
                        cart.Add(size);
                        cart.Add(qty);
                        cart.Add(price);
                        index = item.IndexOf(product);

                    }
                    else
                    {
                        cart.Add(prodName);
                        cart.Add(prodID);
                        cart.Add(size);
                        cart.Add(qty);
                        cart.Add(price);
                    }
                }
            }
            else
            {
                if (Session["order_form_main"] != null)
                {
                    int i = -1;
                    StringCollection form = (StringCollection)Session["order_form_main"];
                    foreach (string row in form)
                    {
                        if (row.Contains(prodID) && row.Contains(size))
                        {
                            i = form.IndexOf(row);
                        }
                    }
                    if (i >= 0)
                    {
                        form.RemoveAt(i);
                    }
                    cart.Add(prodName);
                    cart.Add(prodID);
                    cart.Add(size);
                    cart.Add(qty);
                    cart.Add(price);
                }
                else
                {
                    cart.Add(prodName);
                    cart.Add(prodID);
                    cart.Add(size);
                    cart.Add(qty);
                    cart.Add(price);
                }

            }
            if (index >= 0)
            {
                item.RemoveAt(index);
            }

            item.Add(cart);

            Boolean contains = false;
            foreach (ArrayList prod in products)
            {
                if (prod.Contains(prodID) && prod.Contains(size))
                {
                    contains = true;
                }
            }

            if (contains == false)
            {
                products.Add(cart);
            }

        }


        public ArrayList getCart()
        {
            if (Session["cart"] == null)
            {
                Session.Add("cart", new ArrayList());
            }
            return (ArrayList)Session["cart"];
        }

        public ArrayList getProducts()
        {
            if (Session["products"] == null)
            {
                Session.Add("products", new ArrayList());
            }
            return (ArrayList)Session["products"];
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/OrderForm.aspx");
        }




    }
}
