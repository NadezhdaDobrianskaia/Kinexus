<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ProductDB._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 509px;
        }
        .style3
        {
            width: 272px;
        }
        .style4
        {
            width: 428px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form runat="server" method="post" style="height: 205px">
        <asp:PlaceHolder ID="search_box" runat="server"></asp:PlaceHolder>

        <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
        SelectCommand="SELECT * FROM [ProductDB]"></asp:SqlDataSource>

    </form>
    



</asp:Content>

