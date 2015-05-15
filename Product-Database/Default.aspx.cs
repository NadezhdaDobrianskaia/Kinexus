using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Data.SqlClient;
namespace ProductDB
{

    public partial class _Default : System.Web.UI.Page
    {
        public Boolean search_checked = false;
        public TextBox search_TextBox;

        public Boolean getChecked()
        {
            return search_checked;
        }


        protected void checkbox_checked(object sender, EventArgs e)
        {
            if (ViewState["CheckedStatus"] == null && ViewState["checkboxId"] == null)
            {
                ViewState["CheckedStatus"] = false;
                ViewState["checkboxId"] = "unifiedSearchBar";
            }
            else
            {
                bool isChecked = (bool)ViewState["CheckedStatus"];
                
                if (!isChecked)
                {
                    ViewState["CheckedStatus"] = true;
                    search_TextBox.ID = "unifiedSearchBar" + "_checked" + "_textbx";
                }
                else
                {
                    search_TextBox.ID = "unifiedSearchBar" + "_textbx";
                    ViewState["CheckedStatus"] = false;
                }
            }
            /*
            if (!search_checked)
            {
                search_checked = true;
                search_TextBox.ID = "unifiedSearchBar" + "_checked" + "_textbx";
                //Response.Write(search_TextBox.ID);
            }
            else
            {
                search_TextBox.ID = "unifiedSearchBar" + "_textbx";
                search_checked = false;
                //Response.Write(search_TextBox.ID);
            }*/
        }
        /// <summary>
        /// Event fired on page load.
        /// </summary>
        /// <param name="sender">The page.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //load the image as the first load
            if (!IsPostBack) { 
                SetImageURL();
                checkbox_checked(sender,e);
            }

            //load the search bar
            load_searchBar(search_box, true); //load_categories(search_box, true);

            
            //To read the description of the company, contacts and hyperlinks
            if (!IsPostBack) { 
                products_page_content();
            }

            loadContacts();
        }
        /// <summary>
        /// Reading in all data in order to display the products homepage
        /// </summary>
        private void products_page_content()
        {
            
            System.IO.StreamReader myFile = new System.IO.StreamReader(Server.MapPath("~/InfoHomepage/productsHomepageDescription.txt"));
            string myString = myFile.ReadToEnd();
            HiddenField1.Value = myString;
            myFile.Close();
            
            if (Application["ContactsInfo"] == null)
            {
                System.IO.StreamReader fileContact = new System.IO.StreamReader(Server.MapPath("~/InfoHomepage/productsHomepageContact.txt"));
                StringCollection ContactInfoText = new StringCollection();
                string line;
                while ((line = fileContact.ReadLine()) != null)
                {
                    ContactInfoText.Add(line);
                }

                fileContact.Close();
                Application.Add("ContactsInfo", ContactInfoText);//crate new cach
            }


            if (Application["HyperLinkList"] == null)//is the list of hyperlinks cahsed in mem
            {

                StringCollection LinkEntries = new StringCollection();  //AN array of hyperlinks in the InfoHomepage folder under ImagesLinks.txt
                //To read the links in the controls
                System.IO.StreamReader file2 = new System.IO.StreamReader(Server.MapPath("~/InfoHomepage/ImagesLinks.txt"));
                string line2;
                while ((line2 = file2.ReadLine()) != null)
                {
                    LinkEntries.Add(line2);
                }
                file2.Close();
                Application.Add("HyperLinkList", LinkEntries);//crate new cach
            }

        }


        /// <summary>
        /// loads the contacts information on to the page is a post back is called
        /// </summary>
        private void loadContacts()
        {
            if (Application["ContactsInfo"] == null){
                products_page_content();
            }
            else { 
                StringCollection Contact = (StringCollection)Application["ContactsInfo"];
            
                foreach (string line in Contact)
                {
                    PlaceHolderContact.Controls.Add(new LiteralControl(line + "<br />"));
                }
            }
        }
        /// <summary>
        /// Divides the target string into a collection of strings based on the delimiter.
        /// </summary>
        /// <param name="target">The string to parse</param>
        /// <param name="delim">The delimiter by which to parse the strings. Default: ';'</param>        
        /// <returns>Collection of string from the target string.</returns>
        private StringCollection parseString(string target, char delim = ';')
        {
            //initialize a string collection for the output
            StringCollection output = new StringCollection();

            //intilize a string builder to parse the string
            StringBuilder sb = new StringBuilder();

            //traverse the string
            for (int i = 0; i <= target.Length; i++)
            {
                //we have not reached the end of the string
                if (i < target.Length)
                {
                    //icurrent character is the delimtier
                    if (target[i] == delim)
                    {
                        //add the stringbuilder to the output
                        output.Add(sb.ToString());

                        //flush the stringbuilder
                        sb.Clear();

                        //safe guard incase client breaks his convention (forgets a ' ' after ';')
                        if ((i + 1) < target.Length && target[i + 1] == ' ')
                        {
                            //increment past the whitespace
                            i++;
                        }
                    }
                    //current character is not a delimiter
                    else
                    {
                        //add the current character to the stringbuilder
                        sb.Append(target[i]);
                    }
                }
                //special case, flush the string builder
                else
                {
                    //add the stringbuilder to the collection
                    output.Add(sb.ToString());
                }
            }

            //return the collection
            return output;
        }

        /// <summary>
        /// @Author Calvin Truong
        /// This function loads a search that combines the searches the database by name instead of category
        /// </summary>
        /// <param name="output">Placeholder object that will contain a textbox object and translated into html</param>
        private void load_unifiedSearchBar(PlaceHolder output)
        {
            output.Controls.Add(new LiteralControl("<table id=\"SearchHome\"><tr><td></td><td>" +
                    "<h2>Search Menu</h2></td></tr><td>"));

            //instantiate buttons for search and for product list
            Button search_button = new Button(), list_button = new Button();

            //Creates a textbox that will take in a query string from the user
            TextBox box = new TextBox();

            box.ID = "unifiedSearchBar_textbx";
            box.Attributes.Add("class", "searchBox");
            box.Text = "Enter Search Term";
            box.Attributes.Add("onfocus", "rmText($(this))");
            box.Attributes.Add("onblur", "rpText($(this))");

            // Add the control to the panel
            output.Controls.Add(new LiteralControl("</td><td>"));
            output.Controls.Add(box);

            //Response.Write(box.ID);

            
            //define the search button
            search_button.Text = "Search";
            search_button.ID = "unifiedSearchBar_button";

            //attach the click event
            search_button.Click += new EventHandler(search_click);

            //attach the form attributes
            search_button.Attributes.Add("method", "post");
            search_button.Attributes.Add("type", "submit");

            //add the control to the panel
            output.Controls.Add(new LiteralControl("</td><td>"));
            output.Controls.Add(search_button);
            output.Controls.Add(new LiteralControl("</td><td></td></tr>"));


            output.Controls.Add(new LiteralControl("</table>"));
        }


        private void load_searchBar(PlaceHolder output, bool special_order)
        {

            output.Controls.Add(new LiteralControl("<table id=\"searchHome\" ><tr><td></td><td>" +
                                "<h2>Search Menu</h2></td></tr><td>"));
            
            //instantiate a textbox for the query string
            search_TextBox = new TextBox();

            //instantiate buttons for search and for product list
            Button search_button = new Button(), list_button = new Button();
            CheckBox search_checkbox = new CheckBox();
            search_checkbox.Attributes.Add("class", "searchCheckBox");
            string group = (string)ViewState["checkboxId"]; // this had been the problem changing code compared to old code
                                       //need a drop down selection list to help choose and add the id to the dropbox

            //define textbox
            search_TextBox.ID = group.Replace(" ", "_") + "_textbx";
            search_TextBox.Attributes.Add("class", "searchBox");
            search_TextBox.Text = "Enter Search Term";
            search_TextBox.Attributes.Add("onfocus", "rmText($(this))");
            search_TextBox.Attributes.Add("onblur", "rpText($(this))");
            //add the control to the panel
            output.Controls.Add(new LiteralControl("</td><td>"));
            output.Controls.Add(search_TextBox);

            //Response.Write(search_TextBox.ID);

            //define the search button
            search_button.Text = "Search";
            search_button.ID = group.Replace(" ", "_") + "_button";

            //attach the click event
            search_button.Click += new EventHandler(search_click);

            //attach the form attributes
            search_button.Attributes.Add("method", "post");
            search_button.Attributes.Add("type", "submit");

            //add the control to the panel
            output.Controls.Add(new LiteralControl("</td><td>"));
            output.Controls.Add(search_button);

            output.Controls.Add(new LiteralControl("</td><td></td></tr>"));

            
           
            output.Controls.Add(new LiteralControl("<tr><td></td><td>"));
            search_checkbox.AutoPostBack = true;
            search_checkbox.CheckedChanged += new EventHandler(checkbox_checked);
            output.Controls.Add(search_checkbox);
            output.Controls.Add(new LiteralControl("<span class=\"gray30\">Check to search from anywhere in search string, uncheck to search from beginning only.<span></td></tr>"));

            /*
            if (search_checkbox.Checked)
            {
                search_checked = true;
                box.ID = "unifiedSearchBar" + "_checked" + "_textbx";
            }
            else
            {
                box.ID = "unifiedSearchBar" + "_textbx";
                search_checked = false;
            }
             */

            //close the table
            output.Controls.Add(new LiteralControl("</table>"));
        }
        /*
        /// <summary>
        /// Loads the categories based on the enabled groupings.
        /// </summary>
        /// <param name="output">The placeholder to draw to.</param>
        /// 
        
        private void load_categories(PlaceHolder output, bool special_order)
        {
            //instantiate a collection for enabled groups
            StringCollection enabled = new StringCollection();

            //determine if there is an application state with enabled groups
            if (Application["main_order_form"] != null)
            {
                //if a state exists grab the data from the state
                enabled = (StringCollection)Application["main_order_form"];
            }
            //no application state exists
            else
            {
                //define the path to the category data file
                string data_path = Server.MapPath("category_data.mff");

                //the file exists
                if (File.Exists(data_path))
                {
                    StreamReader reader = new StreamReader(data_path);
                    if (!reader.EndOfStream)
                    {
                        string data = reader.ReadLine();
                        enabled = parseString(data);
                    }
                    
                    //close the reader
                    reader.Close();
                }
                //the file doesn't exist
                else
                {
                    //redirect to the home page
                    Response.Redirect("http://kinexus.ca");
                }
            }

            //add the table tag to the control 
            
            output.Controls.Add(new LiteralControl("<table id=\"searchTable\" ><tr><td colspan=\"4\">" +
                                "<h2>Search Menu</h2><span class=\"gray30\">Select desired category and type in at least 2 letters of the product name or view a complete list<span> <br /><br /></td></tr>"));

            //initialize a collection for the group order
            List<string> groupOrder = new List<string>();

            StringCollection orderedGroups = new StringCollection();
            
            //arrange the order of the items
            groupOrder.Add("Antibody");
            groupOrder.Add("Protein Enzyme");
            groupOrder.Add("Peptide");
            groupOrder.Add("Protein Substrate");
            groupOrder.Add("Bioactive Compound");
            groupOrder.Add("Array");

            //enable the groups in the order collection
            for(int i=0; i<6; i++)
            {
                //add the groups in client's order
                if(enabled.Contains(groupOrder[i]))
                {
                    //add the group to the collection
                    orderedGroups.Add(groupOrder[i]);
                }
            }

            //if the special order flag is set to true then use the orderd group collection otherwise use the enabled colletion
            StringCollection groups = special_order ? orderedGroups : enabled;

            //traverse all the groups in the enabled collection
            foreach (string group in groups)
            {
                //instantiate a label for the group name
                Label label = new Label();

                //instantiate a textbox for the query string
                TextBox box = new TextBox();

                //instantiate buttons for search and for product list
                Button search_button = new Button(), list_button = new Button();

                //define the label
                label.Text = group;
                label.ID = group.Replace(" ", "_") + "_slabel";

                //add the control to the panel
                output.Controls.Add(new LiteralControl("<tr><td>"));
                output.Controls.Add(label);

                //define textbox
                box.ID = group.Replace(" ", "_") + "_textbx";
                box.Attributes.Add("class", "searchBox");
                box.Text = "Enter Search Term";
                box.Attributes.Add("onfocus", "rmText($(this))");
                box.Attributes.Add("onblur", "rpText($(this))");
                //add the control to the panel
                output.Controls.Add(new LiteralControl("</td><td>"));
                output.Controls.Add(box);

                //define the search button
                search_button.Text = "Search";
                search_button.ID = group.Replace(" ", "_") + "_button";
                
                //attach the click event
                search_button.Click += new EventHandler(search_click);

                //attach the form attributes
                search_button.Attributes.Add("method", "post");
                search_button.Attributes.Add("type", "submit");

                //add the control to the panel
                output.Controls.Add(new LiteralControl("</td><td>"));
                output.Controls.Add(search_button);
                output.Controls.Add(new LiteralControl("</td><td>"));
                //define the product list button
                list_button.Text = "Product List";
                list_button.ID = group.Replace(" ", "_") + "_pdlist";

                //attach the click event
                list_button.Click += new EventHandler(list_click);   

                //attach the form attributes
                list_button.Attributes.Add("method", "post");
                list_button.Attributes.Add("type", "submit");

                //add the control to the panel
                output.Controls.Add(list_button);
                output.Controls.Add(new LiteralControl("</td></tr>"));                
            }

            //close the table
            output.Controls.Add(new LiteralControl("</table>"));
        }
        
        */
        /// <summary>
        /// Product list button clicked.
        /// </summary>
        /// <param name="sender">The product list button.</param>
        /// <param name="e">The event arguments.</param>
        protected void list_click(object sender, EventArgs e)
        {
            Button button_sender = (Button)sender;
            string button_group = button_sender.ID.Substring(0, button_sender.ID.Length - 7).Replace("_", "");
            Response.Redirect("~/ProductsList_" + button_group + ".aspx");
        }

        /// <summary>
        /// Search button clicked.
        /// </summary>
        /// <param name="sender">The search button.</param>
        /// <param name="e">The event arguments.</param>
        protected void search_click(object sender, EventArgs e)
        {
            string product_group = "";
            string product_name = "";
            string product_num = "";
            //cast the sender as a button
            Button button_sender = (Button)sender;

            //grab the group tag on the button
            string group_id = button_sender.ID.Substring(0, button_sender.ID.Length - 7); //unifiedsearch etc.

            //get the value in the associated textbox from the post data and strip the tags
            string box_value = Server.HtmlEncode(Request.Form["ctl00$MainContent$" + group_id + "_textbx"]);

            for (int i = 0; i < box_value.Length; i++)
            {
                if (box_value[i] == ' ' && box_value[i + 1] == '-' && box_value[i + 2] == ' ')
                {
                    product_num = box_value.Substring(0, i);
                    ++i;
                    ++i;

                }

            }

            for (int i = 0; i < product_num.Length; i++)
            {
                if (product_num[i] == 'A' && product_num[i + 1] == 'B')
                {
                    product_group = "Antibody";
                }
                else if (product_num[i] == 'S' && product_num[i + 1] == 'P')
                {
                    product_group = "Peptide";
                }
                else if (product_num[i] == 'B' && product_num[i + 1] == 'C')
                {
                    product_group = "BioactiveCompound";
                }
                else if (product_num[i] == 'P' && product_num[i + 1] == 'E')
                {
                    product_group = "ProteinEnzyme";
                }
                else if (product_num[i] == 'M' && product_num[i + 1] == 'A')
                {
                    product_group = "Array";
                }
                else if (product_num[i] == 'P' && product_num[i + 1] == 'S')
                {
                    product_group = "ProteinSubstrate";
                }
                else if (product_num[i] == 'L' && product_num[i + 1] == 'C')
                {
                    product_group = "Lysate";
                }
            }

            //if the query value box is empty do nothing
            if (box_value.Trim().Length == 0 || box_value.Trim() == "Enter Search Term")
            {
                //do nothing
                return;
            }

            //select the product table from the database
            DataView productTable = (DataView)SqlDataSource7.Select(DataSourceSelectArguments.Empty);

            if (group_id.Equals("unifiedSearchBar"))
            {
                //filter data from the selected database table
                productTable.RowFilter = "Product_Number = '" + product_num + "' ";
                /*
                                          "OR Product_Name_Long = '" + box_value + "' " +
                                         "OR Product_Name_Alias LIKE '%" + EscapeSQlLikeString(box_value) + "%'" +
                                         "OR Pep_Sequence LIKE '%" + EscapeSQlLikeString(box_value) + "%'";*/

            }
         
            //create a dataview row
            DataRowView row = (DataRowView)productTable[0];

            //create a product object
            Product product = new Product();

            //set the product values
            product.Product_Name = box_value;
            product.Product_Number = row["Product_Number"].ToString();
            
            //if the page is valid
            if (Page.IsValid)
            {
                //redirect to the associated product detail page .Replace("_", string.Empty) 
                Response.Redirect("~/ProductInfo_" + product_group.Replace("_", string.Empty)  + ".aspx?Product_Number=" + product.Product_Number);
            }
        }
        /// <summary>
        /// Preparers user input for a like query by escaping special characters
        /// </summary>
        /// <param name="term">the string to be escaped</param>
        /// <returns>the escaped string</returns>
        private string EscapeSQlLikeString(String term)
        {
            term = term.Replace("_", "[_]");
            term = term.Replace("[", "[[]");
            return term.Replace("%", "[[%]");
        }

        /// <summary>
        /// Responsible for switching images every few seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            SetImageURL();
        }

       
        /// <summary>
        /// Responsible for slide image on the homepage
        /// </summary>
        private void SetImageURL()
        {
           
            if (ViewState["ImageDisplayed"] == null)
            {
                ImageButton1.ImageUrl = "~/InfoHomepage/1.jpg";              
               // ImageButton1.ToolTip = "1.jpg" ;
                /*Image1.ImageUrl = "~/InfoHomepage/1.jpg";*/
                ViewState["ImageDisplayed"] = 1;
                
            }
            else{
                int i = (int)ViewState["ImageDisplayed"]; 
                if (i < 5)
                {
                    i++;
                    /*Image1.ImageUrl = "~/InfoHomepage/" + i.ToString() + ".jpg";*/
                    ImageButton1.ImageUrl = "~/InfoHomepage/" + i.ToString() + ".jpg";
                   // ImageButton1.ToolTip = i.ToString() + ".jpg";
                    ViewState["ImageDisplayed"] = i;
                    
                }else {

                    ImageButton1.ImageUrl = "~/InfoHomepage/1.jpg";
                    //ImageButton1.ToolTip = i.ToString();
                    /*Image1.ImageUrl = "~/InfoHomepage/1.jpg";*/
                    ViewState["ImageDisplayed"] = 1;
                    
                }
            }
                /* the below code if you want to display images at random
            Random rand = new Random();
            int i = rand.Next(1, 5);
            Image1.ImageUrl = "~/InfoHomepage/" + i.ToString() + ".jpg";
                 */
        }


        
        /// <summary>
        /// when the image button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            Timer1.Enabled = false;
            //Response.Redirect("http://google.com", true);

            StringCollection Hyper;
            //hard coded defaults, this is a fail safe.
            //if thire is not infoefile use the following link 
            string url = "http://www.kinexus.ca/ourServices/ourServices.html";
            string alt = "AD";

            if (ViewState["ImageDisplayed"] == null)
            {
                Hyper = (StringCollection)Application["HyperLinkList"];//read the adlist cash
                    url = Hyper[0];
                    Response.Redirect(url, true);
                    Timer1.Enabled = true;
            }
            
            else { 
            
                int i = (int)ViewState["ImageDisplayed"];
                Hyper = (StringCollection)Application["HyperLinkList"];//read the adlist cash
                url = Hyper[(i - 1)];
                Response.Redirect(url, true);

            }
            Timer1.Enabled = true;
        }
    }
}
