<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsList_BioactiveCompounds.aspx.cs" Inherits="Product_Database.Bioactive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 763px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style2">
                    <h2>Product List - Bioactive Compounds</h2></td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2"
                    DataTextField="Product_Name_Short" DataValueField="Product_Name_Short" Style="margin-bottom: 0px"
                    Width="168px" ondatabound="DropDownList1DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>" 
                SelectCommand="SELECT [Product_Name_Short] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Short] IS NOT NULL)
UNION
SELECT [Product_Name_Long] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Long] IS NOT NULL)
UNION
SELECT [Product_Name_Alias] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Alias] IS NOT NULL)
">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Bioactive Compound" Name="Product_Type_General" 
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" 
                    Width="127px" />
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>" 
                    SelectCommand="SELECT * FROM [ProductDB]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" GroupItemCount="3">
                    <ItemTemplate>
                        <td>
                            <table width="300">
                                <tr style="">
                                    <td width="10%" style="">
                                        <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.
                                    </td>
                                     <td width="30%">
                                        <asp:HyperLink Label ID="Product_NumberLabel" runat="server" Text='<%# Eval("Product_Number") %>' 
                                            NavigateUrl='<%#"~/ProductsListDetailed_BioactiveCompound.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
                                    </td>
                                    <td width="60%">
                                        <asp:HyperLink Label ID="Product_Name_ShortLabel" runat="server" Text='<%# Eval("Product_Name_Short") %>' 
                                            NavigateUrl='<%#"~/ProductsListDetailed_BioactiveCompound.aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="groupPlaceholderContainer" runat="server" border="0" style="">
                                        <tr runat="server" style="">
                                            <th runat="server">
                                                Product_Number
                                            </th>
                                            <th runat="server">
                                                Product_Name_Short
                                            </th>
                                        </tr>
                                        <tr id="groupPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="">
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server" style="text-align: left">
                            <td id="itemPlaceholder" runat="server">
                            </td>
                        </tr>
                    </GroupTemplate>
                </asp:ListView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
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
