<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdAdmin.ascx.cs" Inherits="Product_Database.AdAdmin" %>
<form id="adAdmin" runat="server" method="post" name="adAdmin" enctype="multipart/form-data">
Width&#160;<asp:TextBox value="300" ID="width" runat="server"></asp:TextBox><br />
Height<asp:TextBox ID="height" value ="150" runat="server"></asp:TextBox><br />
Url	&#160;&#160;&#160;&#160;<asp:TextBox ID="url" value="http://www.kinexus.ca/" runat="server"></asp:TextBox><br />
Alt Tag <asp:TextBox ID="alt" value="kinexus" runat="server"></asp:TextBox><br />
<asp:FileUpload ID="ad" runat="server" />
<br />
<input id="Submit1" type="submit" value="submit" />
</form>
<asp:Literal ID="serverRespons" runat="server"></asp:Literal>
