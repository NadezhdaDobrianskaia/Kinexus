using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProductDB;

namespace Product_Database
{
    public partial class Bioactive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            DataView ProductTable = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            ProductTable.RowFilter = "Product_Name_Short = '" + Bioactive_Compound_textbx.Text + "'"
                                + " OR Product_Name_Long = '" + Bioactive_Compound_textbx.Text + "'"
                                + " OR Product_Name_Alias = '" + Bioactive_Compound_textbx.Text + "'"
                                   ;
            DataRowView row = (DataRowView)ProductTable[0];

            Product myProduct = new Product();
            myProduct.Product_Name = Bioactive_Compound_textbx.Text;
            myProduct.Product_Number = row["Product_Number"].ToString();

            if (Page.IsValid)
            {
                Response.Redirect("~/ProductInfo_BioactiveCompound.aspx?Product_Number=" + myProduct.Product_Number);
            }
        }
    }
}