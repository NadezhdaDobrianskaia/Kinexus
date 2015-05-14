<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductInfo_Microarrays.aspx.cs" Inherits="Product_Database.ProductInfo_Microarrays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server">
    <div id="ShoppingCartMenu" class="column recommendedLink">
        <asp:ImageButton ID="ImageButton1" runat="server" CssClass="cartImg" ImageUrl="~/Images/cart2.png"
            onmouseover="mopen('DropDownMenu')" onmouseout="mclosetime()" 
            Height="23px" Width="157px" />
        <a class="checkoutLink" href="./CartSummary.aspx">View Shopping Cart</a>
        <div id="DropDownMenu" style="visibility: hidden;" onmouseover="mcancelclosetime()"
            onmouseout="mclosetime()" class="">
            <%
                int i = 0;
                foreach (string size in sizes)
                {
                    if (i % 2 == 0)
                    {
                        ViewState["size"] = size;
                        if (sizes[i + 1] == "Small")
                        { 
            %>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="ddmenuItem" OnClick="addProd"><% Response.Write(ViewState["size"]);%></asp:LinkButton><br />
            <% 
}

                        if (sizes[i + 1] == "Medium")
                        { 
            %>
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="ddmenuItem" OnClick="addProd"><% Response.Write(ViewState["size"]);%></asp:LinkButton><br />
            <% 
}
                        if (sizes[i + 1] == "Large")
                        { 
            %>
            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="ddmenuItem" OnClick="addProd"><% Response.Write(ViewState["size"]);%></asp:LinkButton><br />
            <% 
}

                    }
                    i++;
                }    
            %>
        </div>
    </div>
    </form>
<div class="productInfo">
    <asp:Literal ID="output" runat="server"></asp:Literal>
</div>
</asp:Content>
