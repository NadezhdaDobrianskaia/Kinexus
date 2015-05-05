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
        /// <summary>
        /// Event fired on page load.
        /// </summary>
        /// <param name="sender">The page.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetImageURL();
            //load the categories, with special ordering set to true
            load_searchBar(search_box, true);
            //load_categories(search_box, true);
            //load_hyperlinks(search_box, true);
            var data = File.ReadAllText(Server.MapPath("~/InfoHomepage/productsHomepageDescription.txt"));
            HiddenField1.Value = data.ToString();
            /*
            var data2 = File.ReadAllText(Server.MapPath("~/InfoHomepage/productsHomepageContact.txt"));
            HiddenField2.Value = data2.ToString();
            */
            
            string line;
            string data3 = "";
            System.IO.StreamReader file = new System.IO.StreamReader(Server.MapPath("~/InfoHomepage/productsHomepageContact.txt"));
            while ((line = file.ReadLine()) != null)
            {
            
                PlaceHolderContact.Controls.Add(new LiteralControl(line + "<br />"));          
            }

            
                        
            file.Close();




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

            output.Controls.Add(new LiteralControl("<tr>" +
    "<td></td><td colspan=\"5\"><span class=\"gray30\">Select desired category and type in at least 2 letters of the product name or view a complete list<span> <br /><br /></td></tr>"));

            output.Controls.Add(new LiteralControl("</table>"));
        }


        private void load_searchBar(PlaceHolder output, bool special_order)
        {

            output.Controls.Add(new LiteralControl("<table id=\"searchHome\" ><tr><td></td><td>" +
                                "<h2>Search Menu</h2></td></tr><td>"));
            
            //instantiate a textbox for the query string
            TextBox box = new TextBox();

            //instantiate buttons for search and for product list
            Button search_button = new Button(), list_button = new Button();

            string group = "Peptide"; // this had been the problem changing code compared to old code
                                       //need a drop down selection list to help choose and add the id to the dropbox

            //define textbox
            box.ID = group.Replace(" ", "_") + "_textbx";
            box.Attributes.Add("class", "searchBox");
            box.Text = "Enter Search Term";
            box.Attributes.Add("onfocus", "rmText($(this))");
            box.Attributes.Add("onblur", "rpText($(this))");
            //add the control to the panel
            output.Controls.Add(new LiteralControl("</td><td>"));
            output.Controls.Add(box);

            Console.Write(box.ID);

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


            output.Controls.Add(new LiteralControl("<tr>" +
                "<td></td><td colspan=\"5\"><span class=\"gray30\">Select desired category and type in at least 2 letters of the product name or view a complete list<span> <br /><br /></td></tr>"));

            //close the table
            output.Controls.Add(new LiteralControl("</table>"));
        }
        /*
        private void load_hyperlinks(PlaceHolder output, bool special_order)
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



            output.Controls.Add(new LiteralControl("<table id=\"searchTable\" ><tr><td colspan=\"4\">" +
                               "<h2>Search Menu</h2><br /><br /></td></tr>"));
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
            for (int i = 0; i < 6; i++)
            {
                //add the groups in client's order
                if (enabled.Contains(groupOrder[i]))
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

                //define the label
                label.Text = group;
                label.ID = group.Replace(" ", "_") + "_slabel";

                //add the control to the panel
                output.Controls.Add(new LiteralControl("<tr><td>"));
                output.Controls.Add(label);
                
            }

            //close the table
            output.Controls.Add(new LiteralControl("</table>"));
        }
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
            groupOrder.Add("Microarray");

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
            //cast the sender as a button
            Button button_sender = (Button)sender;

            //grab the group tag on the button
            string button_group = button_sender.ID.Substring(0, button_sender.ID.Length - 7);

            //get the value in the associated textbox from the post data and strip the tags
            string box_value = Server.HtmlEncode(Request.Form["ctl00$MainContent$" + button_group + "_textbx"]);

            //if the query value box is empty do nothing
            if (box_value.Trim().Length == 0 || box_value.Trim() == "Enter Search Term")
            {
                //do nothing
                return;
            }

            //select the product table from the database
            DataView productTable = (DataView)SqlDataSource7.Select(DataSourceSelectArguments.Empty);

            //filter data from the selected database table
            productTable.RowFilter = "Product_Name_Short = '" + box_value + "' " +
                                     "OR Product_Name_Long = '" + box_value + "' " +
                                     "OR Product_Name_Alias LIKE '%" + EscapeSQlLikeString(box_value) + "%'";
         
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
                //redirect to the associated product detail page
                Response.Redirect("~/ProductInfo_" + button_group.Replace("_", string.Empty) + ".aspx?Product_Number=" + product.Product_Number);
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

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            SetImageURL();
        }

        private void SetImageURL()
        {
            Random rand = new Random();
            int i = rand.Next(1, 5);
            Image1.ImageUrl = "~/InfoHomepage/" + i.ToString() + ".jpg";
        }
    }
}
