using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace Product_Database
{
    public partial class CategoryManager : System.Web.UI.Page
    {
        /// <summary>
        /// Event fired on page load.
        /// </summary>
        /// <param name="sender">The page.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if we are not authenticated logout.
            if (!Request.IsAuthenticated)
            {
                //if not authenticated go to login page
                Response.Redirect("Login.aspx");
            }

            //the collection of post data
            NameValueCollection nvc = Request.Form;
            //initilize the stirng colleciton for the enabled groups
            StringCollection enabledGroups = new StringCollection();

            string filename = "category_data.mff";

            //there is no post data
            if (nvc.Count == 0)
            {
                //if we have enabled groups in the application state
                if (Application["enabled_groups"] != null)
                {
                    enabledGroups = (StringCollection)Application["enabled_groups"];
                }
                //there is no application data
                else
                {
                    //read the category data if it exists, or create an empty collection
                    enabledGroups = readFlatFile(filename);                    
                }
                //build a category form in the group_manager literal
                buildCategoryForm(group_manager, enabledGroups);
            }
            //the form was submitted
            else
            {
                //traverse all the items in the post collection
                foreach (string name in nvc.AllKeys)
                {
                    //get the last six characters or whole word
                    string end_tag = name.Length > 6 ? name.Substring(name.Length - 6) : name;

                    //check to see if we are dealing with a group dropdown item
                    if (end_tag.Equals("gpitem"))
                    {
                        //if dropdown box is set to enabled
                        if (nvc.Get(name).Equals("enabled"))
                        {
                            //add the group to the enabled groups colleciton
                            enabledGroups.Add(name.Substring(0, name.Length - 7));
                        }
                    }
                }

                //save the enabled groups to a file
                writeFlatFile(filename, enabledGroups);                

                //set the enabled groups application state
                Application.Add("enabled_groups", enabledGroups);

                //rebuild the group
                buildCategoryForm(group_manager, enabledGroups);
            }
        }

        /// <summary>
        /// Reads a flat file from the server creates an empty collection if nothing exists.
        /// </summary>
        /// <param name="filename">The name of the file to read.</param>
        /// <returns>A collection of values in the file.</returns>
        private StringCollection readFlatFile(string filename)
        {
            //determine the filepath on the server
            string data_path = Server.MapPath(filename);
            //initilize an empty collection
            StringCollection output = new StringCollection();
            //file exists
            if (File.Exists(data_path))
            {
                //initialize a stream reader
                StreamReader reader = new StreamReader(data_path);

                //not the end of the stream
                if (!reader.EndOfStream)
                {
                    //read the line of data
                    string data = reader.ReadLine();

                    //parse the information into a collection
                    output = parseString(data);
                }

                //close the stream
                reader.Close();
            }

            //return the collection
            return output;            
        }

        /// <summary>
        /// Write a flat file with the filename and data to the server.
        /// </summary>
        /// <param name="filename">The filename to write to the server.</param>
        /// <param name="data">The collection of data to write to the server.</param>
        private void writeFlatFile(string filename, StringCollection data)
        {
            //determine the filepath on the server
            string data_path = Server.MapPath(filename);

            //initilize an empty collection
            StringCollection output = new StringCollection();

            //open a writer stream to save the file
            StreamWriter writer = new StreamWriter(data_path);

            //write the file with the enabled groups
            writer.WriteLine(buildSaveData(data));

            //close the stream
            writer.Close();
        }

        /// <summary>
        /// Divides the target string into a collection of strings based on the delimiter.
        /// </summary>
        /// <param name="target">The string to parse</param>
        /// <param name="delim">The delimiter by which to parse the strings. Default: ';'</param>        
        /// <returns>Collection of string from the target string.</returns>
        private StringCollection parseString(string target, char delim = ';')
        {
            //initilize a string collection for the output
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
        /// Save the enabled groups to a string
        /// </summary>
        /// <param name="groups">The collection of enabled groups.</param>
        /// <returns>The string representing the enabled groups.</returns>
        private string buildSaveData(StringCollection groups)
        {
            //the output stringbuilder
            StringBuilder output = new StringBuilder();
            
            //traverse the group
            for(int i=0; i<groups.Count; i++)
            {
                //append the current group to the output string
                output.Append(groups[i]);

                //if we are not on the last group
                if (i != groups.Count - 1)
                {
                    //insert the delimiter and a space
                    output.Append("; ");
                }
            }
            
            //return the output string
            return output.ToString();
        }

        /// <summary>
        /// Build the category form based on the distinct items in the product type column.
        /// </summary>
        /// <param name="literal"></param>
        private void buildCategoryForm(Literal literal, StringCollection enabled)
        {
            //build the query string
            string queryString = "SELECT DISTINCT Product_Type_General FROM ProductDB";

            //make the connection to the database
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Kinexus Protein ProductDBConnectionString"].ConnectionString);

            //run the query
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();

            //execute the reader on the query
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                StringCollection values = new StringCollection();
                while (reader.Read())
                {
                    string group_name = reader[0].ToString();
                    values.Add(group_name);
                }

                //object[] values = new object[reader.FieldCount];
                //int fieldCount = reader.GetValues(values);

                generateCategoryHtml(literal, values, enabled);
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// Generate the html for the category manager
        /// </summary>
        /// <param name="output">The literal to output to.</param>
        /// <param name="groups">The collection of all the groups.</param>
        /// <param name="enabled">The collection of enabled groups.</param>
        private void generateCategoryHtml(Literal output, StringCollection groups, StringCollection enabled)
        {
            //clear the output
            output.Text = "";

            //create a table to contain the form
            output.Text += "<table>";

            //the variable that signifys if a category is enabled
            bool enable = false;

            //group title
            output.Text += "<tr><td><h3>Enable Groups</h3></td></tr>";

            //generate html for each grouping in the collection of groups
            foreach (object group in groups)
            {
                //enabled collection contains the value
                if(enabled.Contains(group.ToString()))
                {
                    //set it to enabled
                    enable = true;
                }
                
                //draw the group name
                output.Text += "<tr><td>" + group + "</tr></td>";
                //draw the dropdownlist for each category
                output.Text += "<tr><td>" + buildEnableCategory(group.ToString(), enable) + "</tr></td>";
                
                //disable the group, by default
                enable = false;
            }

            //generate submit button and close table
            output.Text += "<tr><td><input type=\"submit\" method=\"post\" value=\"Save\" name=\"submit_category\"></input></tr></td>" +
                           "</table>";
        }

        /// <summary>
        /// Build the html dropdown list with the group name.
        /// </summary>
        /// <param name="group">The name of the group.</param>
        /// <returns>The html dropdown list.</returns>
        private string buildEnableCategory(string group, bool enable)
        {
            //initlize a string builder
            StringBuilder sb = new StringBuilder();

            //initilize the selection buffers
            string enabled = "", disabled = "";

            //category is enabled enable it
            if (enable)
            {
                enabled = "selected=\"selected\"";
            }
            //category is disabled
            else
            {
                disabled = "selected=\"selected\"";
            }


            //define a dropdown list
            sb.Append("<select id=\"" + group + "_gpitem\" name=\"" + group + "_gpitem\">");

            //add the enabled option
            sb.Append("<option id=\"small\" value=\"enabled\" " + enabled + ">Enabled</option>");

            //add the disabled option
            sb.Append("<option id=\"medium\" value=\"disabled\" " + disabled + ">Disabled</option>");

            //close the dropdown list
            sb.Append("</select>");

            //return the html representation of the dropdown list
            return sb.ToString();
        }
    }
}