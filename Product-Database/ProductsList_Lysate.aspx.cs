using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace ProductDB
{
    public partial class ProductsListLysate : System.Web.UI.Page
    {
        int rowID; 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

  
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataView ProductTable = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            ProductTable.RowFilter = "Product_Name_Short = '" + Lysate_textbx.Text + "'"
                                + " OR Product_Name_Long = '" + Lysate_textbx.Text + "'"
                                + " OR Product_Name_Alias LIKE '%" + EscapeSQlLikeString(Lysate_textbx.Text) + "%'";
            DataRowView row = (DataRowView)ProductTable[0];

            Product myProduct = new Product();
            myProduct.Product_Name = Lysate_textbx.Text;
            myProduct.Product_Number = row["Product_Number"].ToString();

            if (Page.IsValid)
            {
                Response.Redirect("~/ProductInfo_Lysate.aspx?Product_Number=" + myProduct.Product_Number);
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
    }
}