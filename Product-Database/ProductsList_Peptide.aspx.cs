using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ProductDB
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            string product_group = "";
            string product_name = "";
            string product_num = "";
            //cast the sender as a button
            Button button_sender = (Button)sender;


            string myText = Peptide_textbx.Text;


            string[] strArr = myText.Split(':');
            product_name = strArr[1];
            product_num = strArr[0];
            product_group = strArr[2];

            //grab the group tag on the button
            string group_id = myText.Substring(0, button_sender.ID.Length - 7); //unifiedsearch etc.

            //get the value in the associated textbox from the post data and strip the tags
            string box_value = Server.HtmlEncode(Request.Form["ctl00$MainContent$" + "Peptide_textbx"]);


            /*for (int i = 0; i < product_num.Length; i++)
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
            }*/
            //create a product object
            Product product = new Product();

            //set the product values
            product.Product_Name = product_name;
            product.Product_Number = product_num;

            //if the page is valid
            if (Page.IsValid)
            {
                //redirect to the associated product detail page .Replace("_", string.Empty) 
                Response.Redirect("~/ProductInfo_" + product_group.Replace("_", string.Empty) + ".aspx?Product_Number=" + product_num);
            }



            /*

            DataView ProductTable = (DataView)SqlDataSource3.Select(DataSourceSelectArguments.Empty);
            ProductTable.RowFilter = "Product_Name_Short = '" + Antibody_textbx.Text + "'"
                                + " OR Product_Name_Long = '" + Antibody_textbx.Text + "'"
                                + " OR Product_Name_Alias LIKE '%" + EscapeSQlLikeString(Antibody_textbx.Text) + "%'";
            DataRowView row = (DataRowView)ProductTable[0];

            Product myProduct = new Product();
            myProduct.Product_Name = Antibody_textbx.Text;
            myProduct.Product_Number = row["Product_Number"].ToString();

            if (Page.IsValid)
            {
                Response.Redirect("~/ProductInfo_Antibody.aspx?Product_Number=" + myProduct.Product_Number);
            }*/
        }
    }
}