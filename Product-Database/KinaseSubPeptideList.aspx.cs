using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Product_Database
{
    public partial class KinaseSubPeptideList : System.Web.UI.Page
    {
        public ArrayList row = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Kinexus Protein ProductDBConnectionString"].ConnectionString);
            SqlDataReader reader = null;
            try
            {
                SqlCommand command = new SqlCommand("SELECT DISTINCT * FROM [KinaseSubPeptideList]", connection);
                //open sql connection
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ArrayList items = new ArrayList();

                    try { items.Add((string)"<a href=\"../Images/images/" + reader[4] + "\" target=\"_blank\" rel=\"shadowbox\"><span style=\"color:#999999;\">" + reader[0] + "</span></a>"); }
                    catch (System.InvalidCastException ex){ items.Add(""); }
                    try { items.Add((string)reader[1]); }
                    catch (System.InvalidCastException ex) { items.Add(""); }
                    try { items.Add((string)reader[2]); }
                    catch (System.InvalidCastException ex) { items.Add(""); }
                    try { items.Add((string)reader[3]); }
                    catch (System.InvalidCastException ex) { items.Add(""); }
                    row.Add(items);
                }
            }
            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close();
                }

                //Close the connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}