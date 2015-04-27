using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProductDB
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1DataBound(object sender, EventArgs e)
        {
            DropDownList1.Items.Insert(0, new ListItem("Enter Search Item", "0"));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataView ProductTable = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            ProductTable.RowFilter = "Product_Name_Short = '" + DropDownList1.SelectedValue + "'"
                                + " OR Product_Name_Long = '" + DropDownList1.SelectedValue + "'"
                                + " OR Product_Name_Alias = '" + DropDownList1.SelectedValue + "'"
                                   ;
            DataRowView row = (DataRowView)ProductTable[0];

            Product myProduct = new Product();
            myProduct.Product_Name = DropDownList1.SelectedValue;
            myProduct.Product_Number = row["Product_Number"].ToString();

            if (Page.IsValid)
            {
                Response.Redirect("~/ProductsListDetailed_ProteinSubstrate.aspx?Product_Number=" + myProduct.Product_Number);
            }
        }
    }
}