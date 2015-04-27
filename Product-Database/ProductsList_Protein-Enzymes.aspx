<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductsList_Protein-Enzymes.aspx.cs" Inherits="ProductDB.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 636px;
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
                <h2>
                    Product List - Protein Enzymes</h2>
            </td>
            <td class="style2">
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2"
                    DataTextField="Product_Name_Short" DataValueField="Product_Name_Short" Style="margin-left: 135px"
                    Width="179px" ondatabound="DropDownList1DataBound">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT [Product_Name_Short] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Short] IS NOT NULL)
UNION
SELECT [Product_Name_Long] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Long] IS NOT NULL)
UNION
SELECT [Product_Name_Alias] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General AND [Product_Name_Alias] IS NOT NULL)
">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Protein Enzyme" Name="Product_Type_General" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" Width="94px" />
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT * FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Protein Enzyme" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td class="style3" colspan="3">
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" GroupItemCount="3">
                    <ItemTemplate>
                        <td>
                            <table width="300">
                                <tr style="">
                                    <td width="10%" style="l">
                                        <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.
                                    </td>
                                    <td width="30%">
                                        <asp:HyperLink Label ID="Product_NumberLabel" runat="server" Text='<%# Eval("Product_Number") %>'
                                            NavigateUrl='<%#"~/ProductsListDetailed_ProteinEnzyme.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
                                    </td>
                                    <td width="60%">
                                        <asp:HyperLink Label ID="Product_Name_ShortLabel" runat="server" Text='<%# Eval("Product_Name_Short") %>'
                                            NavigateUrl='<%#"~/ProductsListDetailed_ProteinEnzyme.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
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
                                                Product_Name_Short
                                            </th>
                                            <th runat="server">
                                                Product_Number
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
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server">
                            </td>
                        </tr>
                    </GroupTemplate>
                </asp:ListView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT [Product_Name_Short], [Product_Number] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Protein Enzyme" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
