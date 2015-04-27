<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderForm.aspx.cs"
    Inherits="OrderForm.OrderForm" %>
<asp:Content ID="order_header" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/validation.js"></script>
    <title>Order Form</title>
</asp:Content>
<asp:Content ID="order_body" ContentPlaceHolderID="MainContent" runat="server">

    <!-- header table -->
    <table>
        <tr>
            <td colspan="2">
                <h1>Product Order Form</h1>
            </td>
        </tr>
        <tr>
            <td>
                <h2 class="noBold">How to Use This Form</h2>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            Please type the required information in each field and then activate the “PRINT” button to obtain 
            a pdf version. Customers should print and sign the completed form, and then return it to Kinexus 
            Bioinformatics Corporation by facsimile transmission to 1-604-323-2548 or by e-mail to info@kinexus.ca. 
            Should you have any questions, please contact our Technical Services representatives at 1-866-Kinexus 
            (in North America) or 1-604-323-2547 Ext. 1. Once you have placed a product order with Kinexus, we will 
            endeavor to ship the product within 2 business days or you will be contacted if there is a delay.
            </td>
        </tr>
    </table>
    <form id="order_form_main" runat="server" method="post" action="OrderForm.aspx">
    <div>
        <!-- customer information -->
        <table>
            <tr>
                <td colspan="3">
                    <h2 class="noBold"><asp:Label ID="ci_header" runat="server" CssClass="orangeLabel">Customer Information</asp:Label></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_sal" runat="server" CssClass="bluelabel">Salutation:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ci_titles" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="True">
                            Dr.
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Mr.
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Ms.
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label id="required_msg_hidden" runat="server" class="redval">* Completion Required</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_last" runat="server" CssClass="bluelabel" ValidationGroup="MKE">Customer Surname:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_last_box_val" runat="server"
                        ControlToValidate="ci_last_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_last_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_first" runat="server" CssClass="bluelabel">Customer First Name:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_first_box_val" runat="server"
                        ControlToValidate="ci_first_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_first_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_title" runat="server" CssClass="bluelabel">Title/Position:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_title_val" runat="server"
                        ControlToValidate="ci_title_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_title_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_org" runat="server" CssClass="bluelabel">Institution or Company Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_org_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_depatment" runat="server" CssClass="bluelabel">Department Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_department_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_suite" runat="server" CssClass="bluelabel">Unit/Room Number:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_suite_box_val" runat="server"
                        ControlToValidate="ci_suite_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_suite_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_address" runat="server" CssClass="bluelabel">Street Address:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_address_box_val" runat="server"
                        ControlToValidate="ci_address_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_address_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_city" runat="server" CssClass="bluelabel">City:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_box_city_val" runat="server"
                        ControlToValidate="ci_city_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_city_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_province" runat="server" CssClass="bluelabel">State/Province:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_province_box_val" runat="server"
                        ControlToValidate="ci_province_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_province_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_country" runat="server" CssClass="bluelabel">Country:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_country_box_val" runat="server"
                        ControlToValidate="ci_country_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_country_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_zip" runat="server" CssClass="bluelabel">Zip/Postal Code:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_zip_box_val" runat="server"
                        ControlToValidate="ci_zip_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_zip_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_email" runat="server" CssClass="bluelabel">E-mail Address:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_email_box_val" runat="server"
                        ControlToValidate="ci_email_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_email_box" runat="server" value="" onblur="email($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_phone" runat="server" CssClass="bluelabel">Phone Number:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_phone_box_val" runat="server"
                        ControlToValidate="ci_phone_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_phone_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_facs" runat="server" CssClass="bluelabel">Facsimile Number:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_facs_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_altname" runat="server" CssClass="bluelabel">Alternative Contact Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_altname_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_altemail" runat="server" CssClass="bluelabel">Alternative Contact Email:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_altemail_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_altphone" runat="server" CssClass="bluelabel">Alternative Contact Phone:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_altphone_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_repeat" runat="server" CssClass="bluelabel">Repeat Customer?</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_repeat_box" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
        </table>
        <!-- same address? -->
        <table>
            <tr>
                <td colspan="2">
                    <h2><asp:Label ID="ci_shipping_addresss_altadd" runat="server" 
                            CssClass="orangeLabel">Shipping Address</asp:Label></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_same_altadd" runat="server" CssClass="bluelabel">Same as above?</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_box_same_altadd" runat="server" onchange="alternate()"></asp:CheckBox>
                </td>
            </tr>
        </table>
        <!-- alternate address -->
        <table id="alt_address_table">
            <tr>
                <td colspan="2">
                    <asp:Label ID="ci_info_altadd" runat="server" CssClass="graytext">If not, please enter shipping info below</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_suite_altadd" runat="server" CssClass="bluelabel">Unit/Room Number:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_suite_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_address_altadd" runat="server" CssClass="bluelabel">Street Address:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_address_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>            
            <tr>
                <td>
                    <asp:Label ID="ci_city_altadd" runat="server" CssClass="bluelabel">City:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_city_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_state_altadd" runat="server" CssClass="bluelabel">State/Province:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_state_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_country_altadd" runat="server" CssClass="bluelabel">Country:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_country_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_zip_altadd" runat="server" CssClass="bluelabel">Zip/Postal Code:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_zip_box_altadd" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
        </table>
        <!-- payment method -->
        <table>
            <tr>
                <td colspan="2">
                    <h2><asp:Label ID="ci_payment_header" runat="server" CssClass="orangeLabel">Payment Method</asp:Label></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_purchaseorder" runat="server" CssClass="bluelabel">Purchase Order:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_purchaseorder_box" runat="server" OnClick="purchaseOrder($(this))"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="ci_info2" runat="server" CssClass="graytext">Accepted from institutions and companies with approved credit</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_ordernumber" runat="server" CssClass="bluelabel">Purchase Order Number:</asp:Label>
                    <asp:Label ID="ci_order_req_hidden" runat="server" CssClass="redval" Visible="false">*</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_ordernumber_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_creditcard" runat="server" CssClass="bluelabel">Credit Card:</asp:Label>
                    
                </td>
                <td>
                    <asp:CheckBox ID="ci_creditcard_box" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_cardtype" runat="server" CssClass="bluelabel">Card Type:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ci_cardtypes" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="True">
                        Visa
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                        MasterCard
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_cardname" runat="server" CssClass="bluelabel">Card Holder Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_cardname_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_cardnumber" runat="server" CssClass="bluelabel">Card Number:</asp:Label>
                    <asp:Label ID="ci_credit_req_hidden" runat="server" CssClass="redval" Visible="false">*</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_cardnumber_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_cardexpiry" runat="server" CssClass="bluelabel">Expiry Date:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_cardexpiry_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
        </table>
        <!-- billing information -->
        <table>
            <tr>
                <td colspan="2">
                    <h2><asp:Label ID="ci_billing_header" runat="server" CssClass="orangeLabel">Billing Information</asp:Label></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_invoicing" runat="server" CssClass="bluelabel">Invoicing:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_invoicing_box_val" runat="server"
                        ControlToValidate="ci_invoicing_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:DropDownList ID="ci_invoicing_box" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="False">
                            Customer Address
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Accounts Payable Address
                        </asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_payablecontact_bill" runat="server" CssClass="whitetext">Accounts Payable Contact</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_salutation_bill" runat="server" CssClass="bluelabel">Salutation:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ci_salutation_bill_box" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="True">
                            Dr.
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Mr.
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Ms.
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_lastname_bill" runat="server" CssClass="bluelabel">Contact - Surname:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_lastname_bill_box_val" runat="server"
                        ControlToValidate="ci_lastname_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_lastname_bill_box" runat="server" value="" onblur="required($this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_firstname_bill" runat="server" CssClass="bluelabel">Contact - First Name:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_firstname_bill_box_val" runat="server"
                        ControlToValidate="ci_firstname_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_firstname_bill_box" runat="server" value="" onblur="required($this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_title_bill" runat="server" CssClass="bluelabel">Title/Position:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_title_bill_box_val" runat="server"
                        ControlToValidate="ci_title_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_title_bill_box" runat="server" value="" onblur="required($this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_org_bill" runat="server" CssClass="bluelabel">Institution or Company Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_org_bill_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_department_bill" runat="server" CssClass="bluelabel">Department Name:</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_department_bill_box" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_suite_bill" runat="server" CssClass="bluelabel">Unit/Room Number:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_suite_bill_box_val" runat="server"
                        ControlToValidate="ci_suite_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_suite_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_address_bill" runat="server" CssClass="bluelabel">Street Address:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_address_bill_box_val" runat="server"
                        ControlToValidate="ci_address_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_address_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_city_bill" runat="server" CssClass="bluelabel">City:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_city_bill_box_val" runat="server"
                        ControlToValidate="ci_city_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_city_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_state_bill" runat="server" CssClass="bluelabel">State/Province:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_state_bill_box_val" runat="server"
                        ControlToValidate="ci_state_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_state_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_country_bill" runat="server" CssClass="bluelabel">Country:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_country_bill_box_val" runat="server"
                        ControlToValidate="ci_country_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_country_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_zip_bill" runat="server" CssClass="bluelabel">Zip/Postal Code:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_zip_bill_box_val" runat="server"
                        ControlToValidate="ci_zip_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_zip_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_email_bill" runat="server" CssClass="bluelabel">E-mail Address:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_email_bill_box_val" runat="server"
                        ControlToValidate="ci_email_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_email_bill_box" runat="server" value="" onblur="email($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_phone_bill" runat="server" CssClass="bluelabel">Phone Number:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_phone_bill_box_val" runat="server"
                        ControlToValidate="ci_phone_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_phone_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_facs_bill" runat="server" CssClass="bluelabel">Facsimile Number:</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="ci_facs_bill_box_val" runat="server"
                        ControlToValidate="ci_facs_bill_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="ci_facs_bill_box" runat="server" value="" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
        </table>
        <!-- short survey -->
        <table>
            <tr>
                <td colspan="2">
                    <h2>Short Survey</h2>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="ci_info_survey" runat="server" CssClass="graytext">How did you learn about Kinexus products?</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_mail_survey" runat="server" CssClass="bluelabel">Direct Mail:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_mail_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_email_survey" runat="server" CssClass="bluelabel">E-mail:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_email_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_internet_survey" runat="server" CssClass="bluelabel">Internet Search:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_internet_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_advertisment_survey" runat="server" CssClass="bluelabel">Advertisement:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_advertisement_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_referral_survey" runat="server" CssClass="bluelabel">Referral:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_referal_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_conference_survey" runat="server" CssClass="bluelabel">Scientific Conference:</asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ci_conference_box_survey" runat="server" value=""></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_other_survey" runat="server" CssClass="bluelabel">Other</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ci_other_box_survey" runat="server" value=""></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="ci_info2_survey" runat="server" CssClass="graytext">If you are a previous Kinexus Customer please tell us how we did</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_service_survey" runat="server" CssClass="bluelabel">Services:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ci_services_survey" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="True">
                            Excellent
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Good
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Fair
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Dissatisfied
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="ci_product_survey" runat="server" CssClass="bluelabel">Products:</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ci_products_survey" runat="server" CssClass="bluelabel">
                        <asp:ListItem Value="value" Selected="True">
                            Excellent
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Good
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Fair
                        </asp:ListItem>
                        <asp:ListItem Value="value" Selected="False">
                            Dissatisfied
                        </asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <!-- product pricing -->
        <table>
            <tr>
                <td>
                    <h2><asp:Label ID="discount_header" runat="server" CssClass="orangeLabel">Product and Pricing Information</asp:Label></h2>
                </td>
            </tr>
            <tr>
                <td>
                    <h3><asp:Label ID="discount_bulk" runat="server" CssClass="orangeLabel">Promotion and Bulk Discounts</asp:Label></h3>
                </td>
            </tr>
            <tr>
                <td width="180" valign="top">
                    <asp:Label ID="order_ref" runat="server" CssClass="bluelabel">Please Provide Quotation or Reference No.</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="order_ref_box" TextMode="MultiLine" Width="180" Columns="1" Rows="3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="180" valign="top">
                    <asp:Label ID="tax_rate" runat="server" CssClass="bluelabel">Applicable Tax Rate (where required)</asp:Label>
                    <!-- validator -->
                    <asp:RequiredFieldValidator 
                        id="tax_rate_box_val" runat="server"
                        ControlToValidate="tax_rate_box"
                        ErrorMessage="*"
                        ValidationGroup="ODF"
                        CssClass="redval">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="tax_rate_box" TextMode="MultiLine" Width="180" Columns="1" Rows="3" runat="server" onblur="required($(this))"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Shipping and handling costs will be applied as most appropriate to the product.</td>                
            </tr>
        </table>
        <!-- order form -->
        <table>
            <tr>
                <td>
                    <asp:Literal ID="product_form" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td id="subtotal" style="text-align: right" height="20px">
                    <!-- <asp:TextBox ID="subtotal" runat="server" value=""></asp:TextBox> -->
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label id="confirm_email_hidden" runat="server">Send confimation email with pdf form?</asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox id="confirm_email_box_hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Button ID="ci_submit" runat="server" method="post" Text="Print Order Form" CausesValidation="true" OnClick="orderButtonClick"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form> 
    </form>
</asp:Content>
 