<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeBehind="Login.aspx.cs" Inherits="Product_Database.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Login_Form" runat="server">
    <div class="column medColumn">
        <table style="width: 100%;">
            <tr>
                <td>
                    <!-- The Login form-->
                    <asp:Login ID="login_control" runat="server" OnAuthenticate="Login_Authenticate">
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
    </form>
</asp:Content>
