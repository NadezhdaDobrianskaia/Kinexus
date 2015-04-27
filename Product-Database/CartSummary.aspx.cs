using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace Product_Database
{
    public partial class CartSummary : System.Web.UI.Page
    {
        public ArrayList items;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["products"] != null)
            {
                Table tbl = new Table();
                tbl.ID = "OrderSummaryTbl";
                tbl.CssClass = "tblOrder";
                tbl.Width = 700;
                TableHeaderRow tblHeader = new TableHeaderRow();
                TableHeaderCell tblHeaderCell = new TableHeaderCell();
                tblHeaderCell.Text = "Shopping Cart Summary: ";
                tblHeaderCell.ColumnSpan = 5;
                tblHeader.Cells.Add(tblHeaderCell);
                tbl.Rows.Add(tblHeader);
                TableRow row = new TableRow();
                TableRow rowTotal = new TableRow();

                TableCell header1 = new TableCell();
                TableCell header2 = new TableCell();
                TableCell header3 = new TableCell();
                TableCell header4 = new TableCell();
                TableCell header6 = new TableCell();

                TableCell totalPrice = new TableCell();
                double sumTotal = 0;

                header1.Text = "<span class=\"tableHeader\">Name</span>";
                header2.Text = "<span class=\"tableHeader\">ID</span>";
                header3.Text = "<span class=\"tableHeader\">Size</span>";
                header4.Text = "<span class=\"tableHeader\">Price</span>";
                header6.Text = " ";
                row.Cells.Add(header1);
                row.Cells.Add(header2);
                row.Cells.Add(header3);
                row.Cells.Add(header4);
                row.Cells.Add(header6);
                tbl.Rows.Add(row);

                int i = 0;
                items = (ArrayList)Session["products"];
                
                foreach (ArrayList item in items)
                {
                    LinkButton link = new LinkButton();
                    link.Attributes.Add("runat", "server");
                    link.Text = "Delete";
                    link.ID = i.ToString();
                    CommandEventArgs comm = new CommandEventArgs("num", item[1]);
                    CommandEventArgs comm2 = new CommandEventArgs("num", item[2]);


                    link.Click += delegate { LinkButton1_Click(sender, comm, comm2); };
                    //link.CommandArgument = i.ToString();
                    i++;

                    TableCell cell1 = new TableCell();
                    TableCell cell2 = new TableCell();
                    TableCell cell3 = new TableCell();
                    TableCell cell4 = new TableCell();
                    TableCell cell6 = new TableCell();
                    TableRow row2 = new TableRow();

                    string id = (string)item[1];
                    string size = (string)item[2];
                    double sum = Convert.ToDouble(item[3]) * Convert.ToDouble(item[4]);
                    string price = String.Format("{0:0.00}", Convert.ToDouble(item[4]));
                    string total = String.Format("{0:0.00}", sum);

                    cell1.Text = "<a href=\"ProductInfo_Antibody.aspx?Product_Number=" + item[1].ToString() + " \" >" + item[0].ToString() + "</a>";
                    cell2.Text = item[1].ToString();
                    cell3.Text = item[2].ToString();
                    cell4.Text = "$" + price.ToString();
                    cell6.Controls.Add(link);

                    row2.Cells.Add(cell1);
                    row2.Cells.Add(cell2);
                    row2.Cells.Add(cell3);
                    row2.Cells.Add(cell4);
                    row2.Cells.Add(cell6);

                    sumTotal = sumTotal + Convert.ToDouble(item[4]);
                    tbl.Rows.Add(row2);
                }
                TableCell c1 = new TableCell();
                TableCell c2 = new TableCell();
                TableCell c3 = new TableCell();
                TableCell c4 = new TableCell();

                totalPrice.Text = "<span>Total:</span> $" + sumTotal.ToString() + ".00";
                totalPrice.ColumnSpan = 2;
                rowTotal.Cells.Add(c1);
                rowTotal.Cells.Add(c2);
                rowTotal.Cells.Add(c3);
                rowTotal.Cells.Add(totalPrice);

                HyperLink checkout = new HyperLink();
                checkout.Text = "Checkout";
                checkout.NavigateUrl = "/OrderForm.aspx";
                checkout.CssClass = "checkoutButton";
                tbl.CellSpacing = 5;
                tbl.Rows.Add(rowTotal);
                OrderSummary.Controls.Add(tbl);
                OrderSummary.Controls.Add(checkout);
            }
            else
            {
                CartMessage.Text = "Your Shopping Cart is Empty";
            }

        }

        protected void LinkButton1_Click(object sender, CommandEventArgs e, CommandEventArgs e2)
        {

            StringCollection form = (StringCollection)Session["order_form_main"];
            ArrayList cart = (ArrayList)Session["cart"];
            int removeAt = -1;
            int formRemoveAt = -1;
            int cartRemoveAt = -1;

            foreach (ArrayList item in items)
            {
                if (item.Contains(e.CommandArgument.ToString()) && item.Contains(e2.CommandArgument.ToString()))
                {
                    removeAt = items.IndexOf(item);
                }
            }

            if (removeAt >= 0)
            {
                items.RemoveAt(removeAt);
            }

            if (form != null)
            {
                foreach (string row in form)
                {
                    if (row.Contains(e.CommandArgument.ToString()) && row.Contains(e2.CommandArgument.ToString()))
                    {
                        formRemoveAt = form.IndexOf(row);
                    }
                }

                if (formRemoveAt >= 0)
                {
                    form.RemoveAt(formRemoveAt);

                    for (int i = formRemoveAt; i < form.Count; i++)
                    {
                        form[i] = form[i].Replace("<td>" + (i + 1) + "</td>", "<td>" + (i) + "</td>");
                    }

                }

                if (form.Count < 12)
                {

                    StringCollection form2 = new StringCollection();

                    foreach (string table_row in form)
                    {
                        //if the row is not empty
                        if (table_row.Contains("value"))
                        {
                            form2.Add(table_row);
                        }

                    }

                    int size = form.Count;
                    for (int k = size - 1; k < 11; k++)
                    {
                        form.Add("<table cellpadding=\"0px\" cellspacing=\"1px\"><tr id=\"order_row" + k + "\"><td style=\"padding-right: 9px;\">" + k + "</td><td><input id=\"prod_name" + k + "\" name=\"prod_name" + k + "\" type=\"text\" class=\"auto_complete\" /></td>" +
    "<td><input id=\"prod_id" + k + "\" name=\"prod_id" + k + "\" type=\"text\" class=\"auto_complete_num\" /></td>" +
    "<td><input id=\"prod_size" + k + "\" name=\"prod_size" + k + "\" type=\"text\" /></td>" +
"<td><input id=\"unit_number" + k + "\" name=\"unit_number" + k + "\" onkeyup=\"numeric(" + k + ");\" type=\"text\" /></td>" +
"<td><input id=\"unit_price" + k + "\" name=\"unit_price" + k + "\" type=\"text\" class=\"rightAlign\" /></td>" +
"<td><input id=\"cost" + k + "\" name=\"cost" + k + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\" /></td></tr></table>");
                    }

                    form = form2;
                }




                if (form.Count == 0)
                {
                    Session.Remove("order_form_main");
                }
            }

            if (cart != null)
            {
                foreach (ArrayList prod in cart)
                {
                    if (prod.Contains(e.CommandArgument.ToString()) && prod.Contains(e2.CommandArgument.ToString()))
                    {
                        cartRemoveAt = cart.IndexOf(prod);
                    }
                }

                if (cartRemoveAt >= 0)
                {
                    cart.RemoveAt(cartRemoveAt);
                }
            }

            if (items.Count == 0)
            {
                Session.Remove("products");
            }

            Response.Redirect("/CartSummary.aspx");
        }


    }
}