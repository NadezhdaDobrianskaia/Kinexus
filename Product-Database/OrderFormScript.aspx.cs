using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Text;

namespace ProductDB
{
    /// <summary>
    /// Order form script used by order form.
    /// </summary>
    public partial class OrderFormScript : System.Web.UI.Page
    {
        /// <summary>
        /// Event fired on page load.
        /// </summary>
        /// <param name="sender">The page.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //post information from the page
            var c = HttpContext.Current;
            //product name or id from post
            var prod = c.Request.Form["name"];
            //flag the determine the prior variable
            var type = c.Request.Form["type"];
            //the index of the row being filled
            var i = c.Request.Form["index"];

            //if we got here without a function redirect go back to home
            if (type == null || type.Length == 0)
                output.Text += "<meta http-equiv=\"refresh\" content=\"0;url=/\">";

            //make sure the query doesnt contain nulls or empty string
            if (prod != null)
            {
                if (prod.Length != 0)
                {
                    //remove any potential nested sql from the queries
                    prod = Server.HtmlEncode(prod);
                    type = Server.HtmlEncode(type);
                    i = Server.HtmlEncode(i);

                    //product index from post
                    string index = i.ToString();
                    //product name from post
                    string name = prod.ToString();

                    //build the query string
                    string queryType = type.Equals("_name") ? "Product_Number" : "Product_Name_Short";
                    string queryKey = type.Equals("_name") ? "Product_Name_Short = '" + prod.ToString() + "' OR Product_Name_Long = '" + prod.ToString() + "' OR Product_Name_Alias like '" + prod.ToString() + "'" : "Product_Number = '" + prod.ToString() + "'";                    
                    string queryString = "SELECT " + queryType + ", Price_Size_Small, Price_Size_Medium, Price_Size_Large, Pack_Size_Small, Pack_Size_Medium, Pack_Size_Large FROM ProductDB WHERE " + queryKey;

                    //make the connection to the database
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Kinexus Protein ProductDBConnectionString"].ConnectionString);

                    //run the query
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    //execute the reader on the query
                    SqlDataReader reader = command.ExecuteReader();

                    //read the data output from the query
                    reader.Read();

                    //try to parse information read from the database
                    try
                    {
                        //form from session data
                        StringCollection form = Session["order_form_main"] != null ? (StringCollection)Session["order_form_main"] : null;

                        //if session is null for some reason here we navigated here from an invalid page?
                        if (form == null)
                        {
                            output.Text += "<meta http-equiv=\"refresh\" content=\"0;url=/\">";
                            return;
                        }
                        
                        //product number from database    
                        string prod_number = reader[0].ToString();
                        //small price from database
                        string price_small = reader[1].ToString();
                        //medium price from database
                        string price_medium = reader[2].ToString();
                        //large price from database
                        string price_large = reader[3].ToString();
                        //small size from database
                        string pack_small = reader[4].ToString();
                        //medium size from database
                        string pack_medium = reader[5].ToString();
                        //large size from database
                        string pack_large = reader[6].ToString();

                        //format as currency
                        string currency_format = String.Format("{0:C}", price_small) + ".00";


                        //swap product name and number if we are doing a reverse retrieval
                        if (type.Equals("_id"))
                        {
                            string temp = name;
                            name = prod_number;
                            prod_number = temp;
                        }
                        
                        //rebuild the row into the stringcollection 
                        form[Convert.ToInt32(index)] = "<td>" + index + "</td>" +
                                        "<td><input id=\"prod_name" + index + "\" name=\"prod_name" + index + "\" value=\"" + name + "\" type=\"text\" class=\"auto_complete\" /></td>" +
                                        "<td><input id=\"prod_id" + index + "\" name=\"prod_id" + index + "\" value=\"" + prod_number + "\" type=\"text\" class=\"auto_complete_num\" /></td>" +
                                        "<td>" + build_dropdown(pack_small.Trim(), price_small, pack_medium.Trim(), price_medium, pack_large.Trim(), price_large, index) + "</td>" +
                                        "<td><input id=\"unit_number" + index + "\" name=\"unit_number" + index + "\" onkeyup=\"numeric(" + index + ")\" value=\"1\" type=\"text\" /></td>" +
                                        "<td><input id=\"unit_price" + index + "\" name=\"unit_price" + index + "\" readonly=\"\" value=\"$" + currency_format + "\" type=\"text\" class=\"rightAlign\" /></td>" +
                                        "<td><input id=\"cost" + index + "\" name=\"cost" + index + "\" readonly=\"\" value=\"$" + currency_format + "\" type=\"text\" class=\"rightAlign\" /></td></tr>";
                        output.Text += form[Convert.ToInt32(index)];
                        form[Convert.ToInt32(index)] = "<tr id=\"order_row" + index + "\">" + form[Convert.ToInt32(index)] + "</tr>";
                        Session["order_form_main"] = (object)form;
                    }
                    //if we have a problem retrieving information from the database
                    catch (InvalidOperationException)
                    {
                        //build an an empty table with N/A
                        output.Text += "<td>" + index + "</td>" +
                                        "<td><input id=\"prod_name" + index + "\" name=\"prod_name" + index + "\" value=\"" + name + "\" type=\"text\" class=\"auto_complete\" /></td>" +
                                        "<td><input id=\"prod_id" + index + "\" name=\"prod_id" + index + "\" value=\"\" type=\"text\" class=\"auto_complete_num\" /></td>" +
                                        "<td><input id=\"prod_size " + index + "\" name=\"prod_name" + index + "\" value=\"\" readonly=\"\" type=\"text\" /></td>" +
                                        "<td><input id=\"unit_number" + index + "\" name=\"unit_number" + index + "\" onkeyup=\"numeric(" + index + ")\" value=\"\" type=\"text\" /></td>" +
                                        "<td><input id=\"unit_price" + index + "\" name=\"unit_price" + index + "\" value=\"\" type=\"text\" class=\"rightAlign\" /></td>" +
                                        "<td><input id=\"cost" + index + "\" name=\"cost" + index + "\" readonly=\"\" value=\"\" type=\"text\" class=\"rightAlign\" /></td></tr>";
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Generate a package size dropdown list with prices associated to each field.
        /// </summary>
        /// <param name="small">The value that represents the small amount.</param>
        /// <param name="small_price">The price for the small amount.</param>
        /// <param name="medium">The value that represents the medium amount.</param>
        /// <param name="medium_price">The price for the medium amount.</param>
        /// <param name="large">The value that represents the large amount.</param>
        /// <param name="large_price">The price for the large amount.</param>
        /// <param name="index">The index of the row being manipulated.</param>
        /// <returns>Html representation of the dropdown list.</returns>
        private string build_dropdown(string small, string small_price,
                                      string medium, string medium_price,
                                      string large, string large_price, string index)
        {
            //initlize a string builder
            StringBuilder sb = new StringBuilder();

            //if there none of the sizes have a representation create a N/A textbox
            if (small.Length == 0 && medium.Length == 0 && large.Length == 0)
            {
                return "<input id=\"prod_size " + index + "\" name=\"prod_name" + index + "\" value=\"N/A\" readonly=\"\" type=\"text\" />";
            }
            //generate a dropdown list with the package values and prices
            else
            {
                //define a dropdown list
                sb.Append("<select id=\"prod_size" + index + "\" width=100% height=100% onchange=\"updateSize($(this), " + index + ")\">");

                //if a small package exists add it to the list
                if (small.Length != 0)
                    sb.Append("<option id=\"small\" price=\"" + small_price + "\" value=\"" + small + "\" selected=\"selected\">" + small + "</option>");

                //if a medium package exists add it to the list
                if (medium.Length != 0)
                    sb.Append("<option id=\"medium\" price=\"" + medium_price + "\" value=\"" + medium + "\">" + medium + "</option>");
                
                //if a large package exists add it to the list
                if (large.Length != 0)
                    sb.Append("<option id=\"large\" price=\"" + large_price + "\" value=\"" + large + "\">" + large + "</option>");

                //close the dropdown list
                sb.Append("</select>");
            }

            //return the html representation of the dropdown list
            return sb.ToString();
        }
    }
}