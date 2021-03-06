﻿<%-- ProductsList_BioactiveCompound.aspx
     @author Lili Hao --%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductsList_Protein-Substrates.aspx.cs" Inherits="Product_Database.Bioactive
" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
              .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
        }
        .style4
        {
            width: 2897px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style4">
             <div class="column nameColumn">
                    <span class="bold"><span class="bluelable">Product Type:</span><span class=orangeLabel> Bioactive Compounds</sapn></sapn>
                </div>
            </td>
            <td class="style2">
            <%-- connection string for textbox --%>
               <asp:TextBox id="Bioactive_Compound_textbx" runat="server"></asp:TextBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT [Product_Name_Short] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Short] IS NOT NULL)
UNION
SELECT [Product_Name_Long] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Long] IS NOT NULL)
UNION
SELECT [Product_Name_Alias] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Alias] IS NOT NULL)
">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Bioactive Compound" Name="Product_Type_General" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <%-- connection string for datalink --%>
                <asp:Button ID="Button1" runat="server" Text="Search" 
                    onclick="Button1_Click" />
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>" 
                    SelectCommand="SELECT * FROM [ProductDB]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="style3" colspan="3">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource2" GroupItemCount="15">
                     <ItemTemplate>
                       <tr id="Tr3"  runat="server">
                                   <td class="listnum">
                                        <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.
                                       </td>
                                    <td class="listPnum">
                                        <asp:HyperLink  ID="HyperLink2" runat="server" Text='<%# Eval("Product_Number") %>'
                                            NavigateUrl='<%#"~/ProductInfo_BioactiveCompound.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
                                    </td>
                                    <td class=listname>
                                        <asp:HyperLink  ID="HyperLink1" runat="server" Text='<%# Eval("Product_Name_Short") %>'
                                            NavigateUrl='<%#"~/ProductInfo_BioactiveCompound.aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                    </td>
                                </tr>
                            
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table1" runat="server">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                 
                                </td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td2" runat="server" style="">
                                </td>
                            </tr>
                        </table>
                        <div id="groupPlaceholder" runat="server" />
                    </LayoutTemplate>
                    <GroupTemplate>
                     <div class="column list">
                            <table id="itemPlaceholderContainer" runat="server" />
                            <table >
                            <tr  id="itemPlaceholder" runat="server">
                            </tr>
                            </table>
                        </div>
                    </GroupTemplate>
                </asp:ListView>
                <%-- connection string for productlist --%>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT [Product_Number], [Product_Name_Short] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General) ORDER BY [Product_Name_Short], [Product_Number]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Bioactive Compound" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
