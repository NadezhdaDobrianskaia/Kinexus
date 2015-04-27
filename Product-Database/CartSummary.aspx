<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartSummary.aspx.cs" Inherits="Product_Database.CartSummary"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css"> 

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <div id="OrderSummary" class="column summary" runat="server" style="width:900px;">
        <asp:Label ID="CartMessage" runat="server" Text=""></asp:Label>
    </div>
    </form>
</asp:Content>