﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductsList_Lysate.aspx.cs" Inherits="ProductDB.ProductsListLysate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 110%;
            height: 65px;
        }
        
        .style3
        {
            
            width: 0px;
        }
        .style4
        {
            width: 0px;
        }

        .auto-style1 {
            width: 0px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">

            
<div class="productsListTop">     
        <div id="productListSearchBar">
                <asp:TextBox ID="Lysate_textbx" runat="server"></asp:TextBox>
            <%-- connection string for textbox --%>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString%>"
            SelectCommand="SELECT [Product_Name_Short] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Short] IS NOT NULL)
            UNION
            SELECT [Product_Name_Long] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Long] IS NOT NULL)
            UNION
            SELECT [Product_Name_Alias] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Alias] IS NOT NULL)
            " ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>">
            <SelectParameters>
            <asp:Parameter DefaultValue="Lysate" Name="Product_Type_General" />
            </SelectParameters>
            </asp:SqlDataSource>
            
            <asp:Button ID="Button1" runat="server" Text="Search"  OnClick="Button1_Click"
                    />
            <%-- connection string for datalink --%>
            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString%>"
            SelectCommand="SELECT * FROM [ProductDB]" ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>"></asp:SqlDataSource>
            
        </div>
 
        <div class="productsListColumnName">
            <span class="bold"><span class="bluelable">Product Type:</span><span class="orangeLabel">  Cell / Tissue  Lysate</span></span>

        </div>
        </div>

    <table class="style1">
        <tr>
            
        </tr>
        <tr>
            <td class="auto-style1">
                &nbsp;
            </td>
            <td class="style3" colspan="3">
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" GroupItemCount="28">
                    <ItemTemplate>
                        <tr valign="top" runat="server">
                                   <td class="listnum">
                                        <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.
                                       </td>
                                    <td class="listPnum">
                                        <asp:HyperLink  ID="HyperLink2" runat="server" Text='<%# Eval("Product_Number") %>'
                                            NavigateUrl='<%#"~/ProductInfo_Lysate.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
                                    </td>
                                    <td class="listname">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Product_Name_Short") %>'
                                            NavigateUrl='<%#"~/ProductInfo_Lysate.aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                    </td>
                                </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="">
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
                <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString %>"
                    SelectCommand="SELECT [Product_Name_Short], [Product_Number] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General) ORDER BY [Product_Name_Short], [Product_Number]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Lysate" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>