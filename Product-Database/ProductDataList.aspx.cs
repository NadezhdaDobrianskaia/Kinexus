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

    public partial class ProductDataList : System.Web.UI.Page
    {
        
        private const char Alias_Delim = ';';
        private static string[] StanderdDBColumns = { "Product_Number", "Product_Name_Short", "Product_Name_Long", "Product_Name_Alias" };
        /// <summary>
        /// A data class to represent the autocompleet data for JSON sterilization 
        /// </summary>
        private class AutocompleteInfo
        {
            public string value;
        }
        /// <summary>
        /// BuildSQL builds the query fro the autocompleet of product information as defined by the DBColumns array.
        /// </summary>
        /// <param name="term">the Search term</param>
        /// <param name="cat">the Category of the product tto be listed</param>
        /// <param name="DBColumns"></param>>
        /// <returns>An Sql query string </returns>
        private string BuildSQL(string term, string cat, string[] DBColumns)
        {
            if (DBColumns == null)
            {
                DBColumns = StanderdDBColumns;

            }
            string sql = "";//initialize  to and empty string ;
            string SeachSql = " LIKE'" + EscapeSQlLikeString(term) + "%'";//build the like string
            //lop thor all the clumn to concat i= 
            for (int i = 0; i < DBColumns.Length; i++)
            {
                string colum = DBColumns[i];
                sql += "SELECT Product_Number, Product_Name_Short, Product_Type_General" + " FROM ProductDB WHERE( ";
                if (cat != null)
                {
                    sql += "Product_Type_General ='" + cat + "' AND ";
                }
                sql += colum + " IS NOT NULL AND " + colum + SeachSql + ")";
                if (i < DBColumns.Length - 1)
                {
                    sql += " UNION ";
                }
            }
            return sql + ";";

        }
        private void LoadDataList()
        {
            //get HTTP Request vars 
            String term = Request["term"];
            string cat = Request.QueryString["cat"];
            string[] mode = null;
            if (Request.QueryString["mode"] != null)
            {
                mode = new string[1];
                mode[0] = "Product_Number";
            }

            if (term != null)//
            {
                //URl encode to protect against SQL injection attracts 
                term = Server.HtmlEncode(term);
                cat = Server.HtmlEncode(cat);
                SqlDataReader reader = null;
                List<AutocompleteInfo> data = new List<AutocompleteInfo>();
                try
                {
                    //establish an connection to the SQL server 
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["comp4900ConnectionString"].ConnectionString);

                    SqlCommand command = new SqlCommand(BuildSQL(term, cat, mode), connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        AutocompleteInfo info = new AutocompleteInfo();
                        string value = reader["Product_Number"].ToString() + "      " + reader["Product_Name_Short"]
                            + "      " + reader["Product_Type_General"];
                        //string lable = reader[1].ToString();
                        if (value.Contains(Alias_Delim + " "))
                        {
                            StringCollection aliasCollection = tokenizeStringToCollection(Alias_Delim, value);
                            foreach (string alias in aliasCollection)
                            {
                                if (alias.StartsWith(term))
                                {
                                    info.value = alias;
                                    data.Add(info);
                                }
                            }
                        }
                        else
                        {
                            info.value = value;
                            data.Add(info);
                        }
                    }
                }
                 catch(Exception e ){
                     //do noting 
                 }
                finally
                {
                    //Close when done reading.
                    reader.Close();
                }


                JavaScriptSerializer serializer = new JavaScriptSerializer();
                output.Text += serializer.Serialize(data);


            }
            else
            {
                //force the use to the home page 
                output.Text += "<meta http-equiv=\"refresh\" content=\"0;url=/\">";
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDataList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="delim"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        static StringCollection tokenizeStringToCollection(char delim, string target)
        {
            StringCollection output = new StringCollection();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= target.Length; i++)
            {
                if (i < target.Length)
                {
                    if (target[i] == delim)
                    {
                        output.Add(sb.ToString());
                        sb.Clear();
                        i++;
                    }
                    else
                    {
                        sb.Append(target[i]);
                    }
                }
                else
                {
                    output.Add(sb.ToString());
                }
            }
            return output;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        private string EscapeSQlLikeString(String term)
        {
            term = term.Replace("_", "[_]");
            term = term.Replace("[", "[[]");
            return term.Replace("%", "[[%]");
        }

    }
}