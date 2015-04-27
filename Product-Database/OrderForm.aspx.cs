using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing.Pdf;
using System.Security.Cryptography;
using System.Drawing;
using System.Net;
using System.Xml;
using System.Collections;


namespace OrderForm
{
    public partial class OrderForm : System.Web.UI.Page
    {
        public ArrayList shoppingCart = new ArrayList();
        /// <summary>
        /// Called when the page loads
        /// </summary>
        /// <param name="sender">The page</param>
        /// <param name="e">Event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //create child controls
            base.CreateChildControls();

            //the name of the order form
            string orderFormName = "order_form_main";
            Validate("ODF");
            required_msg_hidden.Visible = true;

            //the session doesn't exist
            if (Session[orderFormName] == null && Session["cart"] == null)
            {
                //generate the order form
                generateOrderForm(product_form, orderFormName);
            }
            else if (Session[orderFormName] != null && Session["cart"] == null)
            {

                //retrieve the collection from the session
                StringCollection form = (StringCollection)Session[orderFormName];

                //clear the literal
                product_form.Text = "";

                //rebuild the order table from the collection
                foreach (string table_row in form)
                {
                    //append the table row to the literal
                    product_form.Text += table_row;

                }
            }
            else if (Session[orderFormName] == null && Session["cart"] != null)
            {
                generateOrderCart(product_form, orderFormName);
            }
            else if (Session[orderFormName] != null && Session["cart"] != null)
            {
                generateCombinedOrder(product_form, orderFormName);
            }
            //the session exists
            else
            {
                //retrieve the collection from the session
                StringCollection form = (StringCollection)Session[orderFormName];

                //clear the literal
                product_form.Text = "";

                //rebuild the order table from the collection
                foreach (string table_row in form)
                {
                    //append the table row to the literal
                    product_form.Text += table_row;
                }

            }
        }

        /// <summary>
        /// Generate pdf document and send it to the browser.
        /// </summary>
        /// <param name="name">The name of the file to send.</param>
        private void generatePdf(string name)
        {
            //define a pdf document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Order Confirmation";

            //write the form to the document
            writeFormToPdf(document);

            //create a memory stream to put the pdf document into
            MemoryStream stream = new MemoryStream();

            //save the document to the stream
            document.Save(stream, false);

            //clear the response
            Response.Clear();

            //define header information for pdf file
            Response.ContentType = "application/pdf";

            //specify the size
            Response.AddHeader("Accept-Header", stream.Length.ToString());
            Response.AddHeader("Content-Length", stream.Length.ToString());

            //specify the filename
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".pdf");

            //write the file
            Response.OutputStream.Write(stream.GetBuffer(), 0, (int)stream.Length);

            //flush and end the resposne
            Response.Flush();
            Response.End();

            //close the stream
            stream.Close();
        }

        /// <summary>
        /// Generate pdf document and send it to the browser.
        /// </summary>
        /// <param name="name">The name of the file to send.</param>
        private MemoryStream writeStreamPdf(string name)
        {
            //define a pdf document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Order Confirmation";

            //write the form to the document
            writeFormToPdf(document);

            //create a memory stream to put the pdf document into
            MemoryStream stream = new MemoryStream();

            //save the document to the stream
            document.Save(stream, false);

            //return the stream
            return stream;
        }

        /// <summary>
        /// Format the listitem value
        /// </summary>
        /// <param name="text">The text from the listitem.</param>
        /// <returns>The value without newline, return carriage, and trimmed.</returns>
        private string formatListItem(string text)
        {
            //remove the tags if they exist
            if (text.Substring(0, 3).Equals("\r\n") && text.Substring(text.Length - 4).Equals("\r\n"))
            {
                //return the string without tags
                return text.Substring(3, (text.Length - 4));
            }

            //if we for some reason fail return the string
            return text;
        }

        /// <summary>
        /// Traverse the form and writ the information to a Pdf Document.
        /// </summary>
        /// <param name="document">Pdf document to write to.</param>
        private void writeFormToPdf(PdfSharp.Pdf.PdfDocument document)
        {
            //define styles
            XFont header_style = new XFont("Arial", 15, XFontStyle.Bold);
            XFont default_style = new XFont("Arial", 11, XFontStyle.Regular);
            XFont table_header = new XFont("Arial", 12, XFontStyle.Bold);

            //header space before and after
            int header_space_before = 10;
            int header_space_after = 5;

            //initialize the text variables
            XFont font = default_style;
            double leftMargin = 40, topMargin = 20;

            //group header
            bool group_header = true;
            bool double_name = false;
            string group_name = "";

            //character size
            double text_size = 0;

            //space before or after styles
            double space_after = 0, space_before = 0;

            //text coordinates
            double x = 0, y = 0;

            //add first page to the document
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XImage header = XImage.FromFile(Server.MapPath("~/Images/KinexusOrderFormHeader.jpeg"));
            XImage footer = XImage.FromFile(Server.MapPath("~/Images/KinexusOrderFormFooter.jpeg"));

            //draw the banner
            gfx.DrawImage(header, (page.Width / 2) - (header.Size.Width / 2), 0);

            /*
            //draw the banner
            gfx.DrawImage(header, (page.Width / 2) - (footer.PixelWidth * 46 / footer.HorizontalResolution) / 2, 0,
            //size of the bannner with offset
            footer.PixelWidth * 46 / footer.HorizontalResolution, footer.PixelHeight * 46 / footer.VerticalResolution);
            */

            //increment past the banner
            y += 100;

            //traverse the form and visit all controls
            foreach (Control c in Page.Form.Controls)
            {
                //control is a literal control
                if (c is LiteralControl)
                {
                    //skip it
                    continue;
                }

                //check if the control has an id and possible style tag
                if (c.ID != null && c.ID.Length >= 6)
                {
                    //get the style tag from the control id
                    string style_tag = c.ID.Substring(c.ID.Length - 6);

                    //if we get to a new group reset header
                    if (!group_header && !group_name.Equals(style_tag))
                    {
                        group_header = true;
                    }

                    //apply styles to or disable groups
                    switch (style_tag)
                    {
                        //apply style to header group
                        case "header":
                            font = header_style;
                            space_before = header_space_before;
                            space_after = header_space_after;
                            break;

                        //apply style to alternate address group
                        case "altadd":
                            //hide the group
                            if (ci_box_same_altadd.Checked)
                            {
                                continue;
                            }
                            //style the group
                            else
                            {
                                //apply a group header
                                if (group_header)
                                {
                                    font = header_style;
                                    group_header = false;
                                    group_name = style_tag;
                                    space_before = header_space_before;
                                    space_after = header_space_after;
                                }
                                //style for rest of group
                                else
                                {
                                    font = default_style;
                                }
                            }

                            break;

                        //groups to skip
                        case "info":
                        case "survey":
                        case "hidden":
                            continue;

                        //default style
                        default:
                            font = default_style;
                            break;
                    }
                }

                //initilize the text variable
                string text = "";

                //if the control is a label
                if (c is Label)
                {
                    text = ((Label)c).Text;
                    text_size = gfx.MeasureString(text, font).Height;
                    x = 0;

                    //increment the line
                    y += text_size;

                    //apply space before style
                    y += space_before;
                    space_before = 0;

                    //if another line will overflow the page create a new one
                    if (y + (text_size * 2) >= page.Height)
                    {
                        //create a new page
                        page = document.AddPage();
                        //get a graphics refence from the apge
                        gfx = XGraphics.FromPdfPage(page);
                        //reset the y position
                        y = 0 + topMargin;
                    }
                }
                //the control is of input type
                else
                {
                    //the control is a textbox
                    if (c is TextBox)
                    {
                        text = ((TextBox)c).Text;
                    }
                    //the contorl is a dropdown list
                    else if (c is DropDownList)
                    {
                        //set the text to the selected item
                        text = formatListItem(((DropDownList)c).Items[((DropDownList)c).SelectedIndex].Text.Trim());
                    }
                    //the control is a checkbox
                    else if (c is CheckBox)
                    {
                        //if the box is checked display a message, otherwise don't
                        text = ((CheckBox)c).Checked ? "x (checked)" : "";
                    }

                    //as long as string is not null
                    if (text.Length > 0)
                    {
                        //mesure the new size
                        text_size = gfx.MeasureString(text, font).Height;
                    }

                    //adjust the x position
                    x = page.Width / 2;
                }

                //if another line will fall in the margin
                if (y + (text_size * 4) >= page.Height)
                {
                    //create a new page
                    page = document.AddPage();
                    //get a graphics refence from the apge
                    gfx = XGraphics.FromPdfPage(page);
                    //reset the y position
                    y = 0 + topMargin;
                }

                //draw the text to the pdf
                gfx.DrawString(text, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);

                //apply space after style
                y += space_after;
                space_after = 0;
            }

            //if the size of the table will overflow the page
            if (y + (text_size * 12) >= page.Height)
            {
                //create a new page
                page = document.AddPage();
                //get a graphics refence from the apge
                gfx = XGraphics.FromPdfPage(page);
                //reset the y position
                y = 0 + topMargin;
            }

            //set new margin for table
            leftMargin = 30;

            double subtotal = 0;

            //draw the requested products table from the post data
            for (int i = 1; i <= 10; i++)
            {
                //set up the header on the first run
                if (i == 1)
                {
                    //set the font to table header style
                    font = table_header;
                    //mesure the font size
                    text_size = gfx.MeasureString("Name:", font).Height;
                    //move down, leave some extra space above
                    y += 2 * text_size;
                    //reset the x position
                    x = 0;

                    //draw the name column header
                    gfx.DrawString("Name:", font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    //offset the drawing location
                    x += page.Width / 4;

                    //draw the id column header
                    gfx.DrawString("Id:", font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    //offset the drawing location
                    x += page.Width / 6;

                    //draw the amount column header
                    gfx.DrawString("Amount:", font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    //offset the drawing location
                    x += page.Width / 6;

                    //draw the price column header
                    gfx.DrawString("Price:", font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    //offset the drawing location
                    x += page.Width / 6;

                    //draw the cost column header
                    gfx.DrawString("Cost:", font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    //offset the drawing location
                    x += page.Width / 6;

                    //reset the style to the default style
                    font = default_style;
                }

                //the name field from the post data
                string name = Request.Form["prod_name" + i];
                //the id field from the post data
                string id = Request.Form["prod_id" + i];
                //the size field from the post data
                string size = Request.Form["prod_size" + i];
                //the amount field from the post data
                string amount = Request.Form["unit_number" + i];
                //the price field from the post data
                string price = Request.Form["unit_price" + i];
                //the cost field from the post data
                string cost = Request.Form["cost" + i];

                //if the cost is null or too short it invalidates the row
                if (cost == null || cost.Length < 2)
                {
                    //skip the row
                    continue;
                }

                //accumulate the total
                subtotal += Convert.ToDouble(cost.Substring(1));

                //calculate the size of the text
                text_size = gfx.MeasureString(name, font).Height;
                //reset the x position
                x = 0;
                //move the the page by the text size
                y += text_size;

                //the name needs be drawn over two lines
                if (gfx.MeasureString(name, font).Width > (page.Width / 5))         //splits up names that are greater size then page.Width / 5
                {
                    //draw the first line
                    gfx.DrawString(name.Substring(0, name.Length / 2), font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    y += text_size;

                    //draw the second line
                    gfx.DrawString(name.Substring(name.Length / 2), font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                    y -= text_size;

                    //set that we need to offset for the second line
                    double_name = true;
                }
                //the name fits on one line
                else
                {
                    gfx.DrawString(name, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                }
                //offset the drawing location
                x += page.Width / 4;

                //draw the id data
                gfx.DrawString(id, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                //offset the drawing location
                x += page.Width / 6;

                //draw the amount data
                gfx.DrawString(amount, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                //offset the drawing location
                x += page.Width / 6;

                //draw the price data
                gfx.DrawString(price, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                //offset the drawing location
                x += page.Width / 6;

                //draw the cost data
                gfx.DrawString(cost, font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);
                //offset the drawing location
                x += page.Width / 6;

                //if we have a name that is over two lines
                if (double_name)
                {
                    //account for the extra line of text
                    y += text_size;
                    //reset the flag
                    double_name = false;
                }
            }

            //adjust for the total
            y += text_size * 2;

            //set the width <-- approximate
            x = (3 * page.Width) / 4;

            //draw the total
            gfx.DrawString("Subtotal: $" + Convert.ToString(subtotal), font, XBrushes.Black, new XRect(x + leftMargin, (y - text_size) + topMargin, page.Width, page.Height), XStringFormats.TopLeft);

            text_size = gfx.MeasureString("Subtotal: $", font).Height;

            //leave some room after the subtotal line
            y += text_size * 5;

            //if only the banner will overflow we have to draw it on the next page..
            if ((y + footer.PixelHeight * 46 / footer.VerticalResolution) > page.Height)
            {
                //create a new page
                page = document.AddPage();
                //get a graphics refence from the apge
                gfx = XGraphics.FromPdfPage(page);
                //reset the y position
                y = 0 + topMargin;

                //draw the footer at the bottom of the page
                gfx.DrawImage(footer, (page.Width / 2) - (header.Size.Width / 2), page.Height - footer.Size.Height);

                /*
                //draw the footer at the bottom of the page
                gfx.DrawImage(footer, (page.Width / 2) - (footer.PixelWidth * 46 / footer.HorizontalResolution) / 2, page.Height - (footer.PixelHeight * 46 / footer.VerticalResolution),
                    //width, height with offset
                footer.PixelWidth * 46 / footer.HorizontalResolution, footer.PixelHeight * 46 / footer.VerticalResolution);
                */
            }
            //banner will fit on the page
            else
            {
                //draw the footer at the bottom of the page
                gfx.DrawImage(footer, (page.Width / 2) - (header.Size.Width / 2), y);
                /*
                //draw the footer bottem of page
                gfx.DrawImage(footer, (page.Width / 2) - (footer.PixelWidth * 46 / footer.HorizontalResolution) / 2, y,
                //width, height with offset
                footer.PixelWidth * 46 / footer.HorizontalResolution, footer.PixelHeight * 46 / footer.VerticalResolution);
                 * */
            }
        }

        /// <summary>
        /// Traverse the form and build a text representation.
        /// </summary>
        /// <returns>Text represnetation of the form.</returns>
        private string writeFormToText()
        {
            StringBuilder output = new StringBuilder();
            //initilize the text variable
            string text = "";

            //build email
            foreach (Control c in Page.Form.Controls)
            {
                text = "";
                //the control is a label
                if (c is Label)
                {
                    text = ((Label)c).Text + " ";
                }
                //the control is not a label
                else
                {
                    //the control is a textbox
                    if (c is TextBox)
                    {
                        text = ((TextBox)c).Text + "\n";
                    }
                    //the control is a dropdownlist
                    else if (c is DropDownList)
                    {
                        text = ((DropDownList)c).Items[((DropDownList)c).SelectedIndex].Text.Trim() + '\n';
                    }
                    //the control is a checkbox
                    else if (c is CheckBox)
                    {
                        //checkbox is checked
                        if (((CheckBox)c).Checked)
                        {
                            //display the checked text
                            text = "x (checked)\n";
                        }
                        //checkbox isn't checkd
                        else
                        {
                            //proceed to the next line
                            text = "\n";
                        }
                    }
                }
                //append the text
                output.Append(text);
            }

            double subtotal = 0;

            for (int i = 1; i <= 10; i++)
            {
                //if we are on the first iteration
                if (i == 1)
                {
                    //append the pseudo headers
                    text = "Name:\tId:\tSize:\tAmount:\tPrice:\tCost:\n";
                    output.Append(text);
                }

                //product name from post data
                string name = Request.Form["prod_name" + i];
                //product id from post data
                string id = Request.Form["prod_id" + i];
                //product size from post data
                string size = Request.Form["prod_size" + i];
                //amount of product form post data
                string amount = Request.Form["unit_number" + i];
                //price of product form post data
                string price = Request.Form["unit_price" + i];
                //cost from post data
                string cost = Request.Form["cost" + i];

                //if the cost is null or just a dollar sign
                if (cost == null || cost.Length < 2)
                {
                    //skip th record
                    continue;
                }

                //accumulate the total
                subtotal += Convert.ToDouble(cost.Substring(1));

                //append the row of values
                text = name + "\t" + id + "\t" + size + "\t" + amount + "\t" + price + "\t" + cost + "\n";
                output.Append(text);
            }

            //append the total
            output.Append("Subtotal: $" + subtotal);

            //return the text representation
            return output.ToString();
        }

        /// <summary>
        /// Click order button.
        /// </summary>
        /// <param name="sender">The order button.</param>
        /// <param name="e">The event arguments</param>
        protected void orderButtonClick(object sender, EventArgs e)
        {
            //initilize the subtotal
            double subtotal = 0;

            //traverse the cost fields
            for (int i = 1; i <= 10; i++)
            {
                //accumulate the costs
                string cost = Request.Form["cost" + i];
                if (!String.IsNullOrEmpty(cost) && cost != ",")
                {
                    if (!cost.Equals("0.00"))
                    {
                        subtotal += Convert.ToDouble(cost.Substring(1));
                    }
                }
            }

            //make sure the subtotal is greater than zero
            if (subtotal <= 0)
            {
                return;
            }

            //validate all server controls in group ODF
            Validate("ODF");
            required_msg_hidden.Visible = true;
            //if the page comes back valid
            if (Page.IsValid)
            {
                required_msg_hidden.Visible = false;
                //smtp information
                SmtpClient client = new SmtpClient("74.220.215.210", 25);
                //smtp credentials
                client.Credentials = new System.Net.NetworkCredential("orders@kinexus.ca", "Products*123");

                //client requested an email
                /* if (confirm_email_box_hidden.Checked)
                 {
                     //create a mail message of confirmation
                     MailMessage customer_confirm = new MailMessage("order@kinexus.ca", ci_email_box.Text, "Kinexus Completed Products Order Form", "Order summary:\n" + writeFormToText() + "!");

                     //create a pdf stream in an attachment
                     Attachment attachment = new Attachment(writeStreamPdf("order_summary"), "order_summary.pdf");
                     //attach it to the client email
                     customer_confirm.Attachments.Add(attachment);
                     try
                     {
                         //send the email off
                         client.Send(customer_confirm);
                     }
                     catch (System.Net.Mail.SmtpFailedRecipientException)
                     {

                     }
                     catch (System.Net.Sockets.SocketException)
                     {
                    
                     }

                 }*/

                //Email confirmation email with pdf attachment to the client 
                MailMessage customer_confirm = new MailMessage("orders@kinexus.ca", ci_email_box.Text, "Kinexus Completed Products Order Form", "Order summary:\n" + writeFormToText() + "!");
                //create a pdf stream in an attachment
                Attachment attachment = new Attachment(writeStreamPdf("order_summary"), "order_summary.pdf");
                //attach it to the client email
                customer_confirm.Attachments.Add(attachment);
                //send the email off
                try { client.Send(customer_confirm); }
                catch (System.Net.Mail.SmtpFailedRecipientException) { }

                //send summary email to kinexus                
                MailMessage confirm = new MailMessage("orders@kinexus.ca", "orders@kinexus.ca", "Kinexus Completed Products Order Form", "Order summary:\n" + writeFormToText() + "!");
                //attempt to send an email
                try { client.Send(confirm); }
                catch (System.Net.Sockets.SocketException) { }

                //finally generate a pdf document for the client
                finally { generatePdf("order_form"); }
            }
        }

        /// <summary>
        /// Generate the order form and persist it with a session.
        /// </summary>
        private void generateOrderForm(Literal literal, string parent_name)
        {
            //initialize a collection for persistance
            StringCollection form = new StringCollection();
            //initialize a stringbuilder
            StringBuilder form_row = new StringBuilder();

            //rebuild the header of the form
            form_row.Append("<table width=\"900\"><tr><td width=\"220\"><h2>Requested Products</h2></td><td style=\"color:#999999;\">Enter either the \"Product Name\" or \"Product ID\" code to initially complete the fields in each row and adjust as desired</td></tr></table>" +
                            "<table cellpadding=\"0px\" cellspacing=\"1px\">" +
                //table headers
                            "<tr style=\"text-align: center;\"><th>No.</th><th>Product Name</th><th>Product ID</th><th>Product Size</th><th>Number of Units</th><th>Unit Price</th><th>Cost</th></tr>");

            //append the stringbuilder to the literal
            literal.Text += form_row.ToString();

            //add the header to the collection
            form.Add(form_row.ToString());

            //flush the stringbuilder
            form_row.Clear();

            //draw ten rows
            for (int i = 1; i <= 10; i++)
            {
                //draw the table row
                form_row.Append("<tr id=\"order_row" + i + "\"><td>" + i + "</td>");
                //product name
                form_row.Append("<td><input id=\"prod_name" + i + "\" name=\"prod_name" + i + "\" type=\"text\" class=\"auto_complete\" /></td>");
                //product id
                form_row.Append("<td><input id=\"prod_id" + i + "\" name=\"prod_id" + i + "\" type=\"text\" class=\"auto_complete_num\" /></td>");
                //product size
                form_row.Append("<td><input id=\"prod_size" + i + "\" name=\"prod_size" + i + "\" type=\"text\" /></td>");
                //number of units
                form_row.Append("<td><input id=\"unit_number" + i + "\" name=\"unit_number" + i + "\" onkeyup=\"numeric(" + i + ");\" type=\"text\" /></td>");
                //price per unit
                form_row.Append("<td><input id=\"unit_price" + i + "\" name=\"unit_price" + i + "\" type=\"text\" class=\"rightAlign\" /></td>");
                //total cost
                form_row.Append("<td><input id=\"cost" + i + "\" name=\"cost" + i + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\" /></td></tr>");

                //append the row to the literal
                literal.Text += form_row.ToString();

                //add the row to the c
                form.Add(form_row.ToString());

                //flush the buffer
                form_row.Clear();
            }

            //append the end of the table
            form_row.Append("</table>");

            //add it to the litteral
            literal.Text += form_row.ToString();

            //add it to the collection
            form.Add(form_row.ToString());

            //create a session
            Session.Add(parent_name, (object)form);
        }

        private void generateCombinedOrder(Literal literal, string parent_name)
        {
            //initialize a collection for persistance
            StringCollection form = new StringCollection();
            //initialize a stringbuilder
            StringBuilder form_row = new StringBuilder();

            //rebuild the header of the form
            form_row.Append("<table width=\"900\"><tr><td width=\"220\"><h2>Requested Products</h2></td><td style=\"color:#999999;\">Enter either the \"Product Name\" or \"Product ID\" code to initially complete the fields in each row and adjust as desired</td></tr></table>" +
                            "<table cellpadding=\"0px\" cellspacing=\"1px\">" +
                //table headers
                            "<tr style=\"text-align: center;\"><th>No.</th><th>Product Name</th><th>Product ID</th><th>Product Size</th><th>Number of Units</th><th>Unit Price</th><th>Cost</th></tr>");

            //append the stringbuilder to the literal
            literal.Text += form_row.ToString();

            //add the header to the collection
            form.Add(form_row.ToString());

            //flush the stringbuilder
            form_row.Clear();

            //retrieve the collection from the session
            StringCollection form2 = (StringCollection)Session["order_form_main"];
            int count = 0;

            //retrieve non-empty rows
            foreach (string table_row in form2)
            {
                //if the row is not empty
                if (table_row.Contains("value"))
                {
                    //add the row to the form
                    form_row.Append(table_row);
                    //append the row to the literal
                    literal.Text += form_row.ToString();
                    //count the number of non-empty rows
                    count++;
                    //add the row to the form
                    form.Add(form_row.ToString());
                    form_row.Clear();
                }

            }

            //number of the next item in the table
            int i = count + 1;
            //count the number of items in the cart
            int countRows = ((ArrayList)Session["cart"]).Count;

            //add items from the cart to the table
            foreach (ArrayList item in (ArrayList)Session["cart"])
            {
                double sum = Convert.ToDouble(item[3]) * Convert.ToDouble(item[4]);

                //draw the table row
                form_row.Append("<tr id=\"order_row" + i + "\"><td>" + i + "</td>");
                //product name
                form_row.Append("<td><input id=\"prod_name" + i + "\" name=\"prod_name" + i + "\" type=\"text\" class=\"auto_complete\" value=\"" + item[0] + "\" /></td>");
                //product id
                form_row.Append("<td><input id=\"prod_id" + i + "\" name=\"prod_id" + i + "\" type=\"text\" class=\"auto_complete_num\" value=\"" + item[1] + "\"/></td>");
                //product size
                form_row.Append("<td><input id=\"prod_size" + i + "\" name=\"prod_size" + i + "\" type=\"text\"  value=\"" + item[2] + "\"/></td>");
                //number of units
                form_row.Append("<td><input id=\"unit_number" + i + "\" name=\"unit_number" + i + "\" onkeyup=\"numeric(" + i + ");\" type=\"text\"  value=\"" + item[3] + "\"/></td>");
                //price per unit
                form_row.Append("<td><input id=\"unit_price" + i + "\" name=\"unit_price" + i + "\" type=\"text\" class=\"rightAlign\"   value=\"$" + item[4] + ".00\"/></td>");
                //total cost
                form_row.Append("<td><input id=\"cost" + i + "\" name=\"cost" + i + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\"  value=\"$" + sum + ".00\"/></td></tr>");
                i++;

                //append the row to the literal
                literal.Text += form_row.ToString();

                //add the row to the c
                form.Add(form_row.ToString());

                //flush the buffer
                form_row.Clear();
            }

            //total number of rows in the table
            int totalRows = count + countRows;
            //see if there are less then 10 rows
            int num = 10 - totalRows;
            //if there are less then 10 rows, add empty rows
            if (num > 0)
            {
                for (int k = ((count + countRows) + 1); k <= 10; k++)
                {
                    //draw the table row
                    form_row.Append("<tr id=\"order_row" + k + "\"><td>" + k + "</td>");
                    //product name
                    form_row.Append("<td><input id=\"prod_name" + k + "\" name=\"prod_name" + k + "\" type=\"text\" class=\"auto_complete\"  /></td>");
                    //product id
                    form_row.Append("<td><input id=\"prod_id" + k + "\" name=\"prod_id" + k + "\" type=\"text\" class=\"auto_complete_num\" /></td>");
                    //product size
                    form_row.Append("<td><input id=\"prod_size" + k + "\" name=\"prod_size" + k + "\" type=\"text\"/></td>");
                    //number of units
                    form_row.Append("<td><input id=\"unit_number" + k + "\" name=\"unit_number" + k + "\" onkeyup=\"numeric(" + k + ");\" type=\"text\" /></td>");
                    //price per unit
                    form_row.Append("<td><input id=\"unit_price" + k + "\" name=\"unit_price" + k + "\" type=\"text\" class=\"rightAlign\"  /></td>");
                    //total cost
                    form_row.Append("<td><input id=\"cost" + k + "\" name=\"cost" + k + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\" /></td></tr>");
                    //append the row to the literal
                    literal.Text += form_row.ToString();

                    //add the row to the c
                    form.Add(form_row.ToString());

                    //flush the buffer
                    form_row.Clear();
                }
            }

            form_row.Append("</table>");
            //append the row to the literal
            literal.Text += form_row.ToString();

            //add the row to the form
            form.Add(form_row.ToString());

            //flush the buffer
            form_row.Clear();

            //add new form to the session
            Session.Add(parent_name, (object)form);
            //remove cart
            Session.Remove("cart");

        }
        private void generateOrderCart(Literal literal, string parent_name)
        {
            //initialize a collection for persistance
            StringCollection form = new StringCollection();
            //initialize a stringbuilder
            StringBuilder form_row = new StringBuilder();

            //rebuild the header of the form
            form_row.Append("<table width=\"900\"><tr><td width=\"220\"><h2>Requested Products</h2></td><td style=\"color:#999999;\">Enter either the \"Product Name\" or \"Product ID\" code to initially complete the fields in each row and adjust as desired</td></tr></table>" +
                            "<table cellpadding=\"0px\" cellspacing=\"1px\">" +
                //table headers
                            "<tr style=\"text-align: center;\"><th>No.</th><th>Product Name</th><th>Product ID</th><th>Product Size</th><th>Number of Units</th><th>Unit Price</th><th>Cost</th></tr>");

            //append the stringbuilder to the literal
            literal.Text += form_row.ToString();

            //add the header to the collection
            form.Add(form_row.ToString());

            //flush the stringbuilder
            form_row.Clear();

            //draw ten rows
            int i = 1;
            //count number of items in the cart
            int countRows = ((ArrayList)Session["cart"]).Count;
            foreach (ArrayList item in (ArrayList)Session["cart"])
            {
                double sum = Convert.ToDouble(item[3]) * Convert.ToDouble(item[4]);

                //draw the table row
                form_row.Append("<tr id=\"order_row" + i + "\"><td>" + i + "</td>");
                //product name
                form_row.Append("<td><input id=\"prod_name" + i + "\" name=\"prod_name" + i + "\" type=\"text\" class=\"auto_complete\" value=\"" + item[0] + "\" /></td>");
                //product id
                form_row.Append("<td><input id=\"prod_id" + i + "\" name=\"prod_id" + i + "\" type=\"text\" class=\"auto_complete_num\" value=\"" + item[1] + "\"/></td>");
                //product size
                form_row.Append("<td><input id=\"prod_size" + i + "\" name=\"prod_size" + i + "\" type=\"text\"  value=\"" + item[2] + "\"/></td>");
                //number of units
                form_row.Append("<td><input id=\"unit_number" + i + "\" name=\"unit_number" + i + "\" onkeyup=\"numeric(" + i + ");\" type=\"text\"  value=\"" + item[3] + "\"/></td>");
                //price per unit
                form_row.Append("<td><input id=\"unit_price" + i + "\" name=\"unit_price" + i + "\" type=\"text\" class=\"rightAlign\"  value=\"$" + item[4] + ".00\"/></td>");
                //total cost
                form_row.Append("<td><input id=\"cost" + i + "\" name=\"cost" + i + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\" value=\"$" + sum + ".00\"/></td></tr>");
                i++;

                //append the row to the literal
                literal.Text += form_row.ToString();

                //add the row to the c
                form.Add(form_row.ToString());

                //flush the buffer
                form_row.Clear();
            }

            //see if there are less than 10 rows in the table
            int num = 10 - countRows;
            if (num > 0)
            {
                for (int k = (countRows + 1); k <= 10; k++)
                {
                    //draw the table row
                    form_row.Append("<tr id=\"order_row" + k + "\"><td>" + k + "</td>");
                    //product name
                    form_row.Append("<td><input id=\"prod_name" + k + "\" name=\"prod_name" + k + "\" type=\"text\" class=\"auto_complete\"  /></td>");
                    //product id
                    form_row.Append("<td><input id=\"prod_id" + k + "\" name=\"prod_id" + k + "\" type=\"text\" class=\"auto_complete_num\" /></td>");
                    //product size
                    form_row.Append("<td><input id=\"prod_size" + k + "\" name=\"prod_size" + k + "\" type=\"text\"/></td>");
                    //number of units
                    form_row.Append("<td><input id=\"unit_number" + k + "\" name=\"unit_number" + k + "\" onkeyup=\"numeric(" + k + ");\" type=\"text\" /></td>");
                    //price per unit
                    form_row.Append("<td><input id=\"unit_price" + k + "\" name=\"unit_price" + k + "\" type=\"text\" class=\"rightAlign\"  /></td>");
                    //total cost
                    form_row.Append("<td><input id=\"cost" + k + "\" name=\"cost" + k + "\" height=\"20px\" width=\"40px\" type=\"text\" class=\"rightAlign\" /></td></tr>");
                    //append the row to the literal
                    literal.Text += form_row.ToString();

                    //add the row to the c
                    form.Add(form_row.ToString());

                    //flush the buffer
                    form_row.Clear();
                }
            }

            //append the end of the table
            form_row.Append("</table>");

            //add it to the litteral
            literal.Text += form_row.ToString();

            //add it to the collection
            form.Add(form_row.ToString());

            //add new form to the session
            Session.Add(parent_name, (object)form);

            //remove cart
            Session.Remove("cart");
        }

    }
}
