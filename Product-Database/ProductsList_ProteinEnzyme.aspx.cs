using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProductDB
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataView ProductTable = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            ProductTable.RowFilter = "Product_Name_Short = '" + Protein_Enzyme_textbx.Text + "'"
                                + " OR Product_Name_Long = '" + Protein_Enzyme_textbx.Text + "'"
                                + " OR Product_Name_Alias LIKE '%" + Protein_Enzyme_textbx.Text + "%'"
                                   ;
            DataRowView row = (DataRowView)ProductTable[0];

            Product myProduct = new Product();
            myProduct.Product_Name = Protein_Enzyme_textbx.Text;
            myProduct.Product_Number = row["Product_Number"].ToString();

            if (Page.IsValid)
            {
                Response.Redirect("~/ProductInfo_ProteinEnzyme.aspx??Product_Number=" + myProduct.Product_Number);
            }
        }
    }
}