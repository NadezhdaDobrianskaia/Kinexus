<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdAdminPage.aspx.cs" Inherits="Product_Database.Styles.AdAdminPage" %>
<%@ Register src="AdAdmin.ascx" tagname="AdAdmin" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc2:AdAdmin ID="AdAdmin1" runat="server" />
</asp:Content>
