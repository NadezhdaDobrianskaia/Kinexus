<%-- ProductsList_BioactiveCompound.aspx
     @author Lili Hao --%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductsList_ProteinEnzyme.aspx.cs" Inherits="ProductDB.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style4
        {
            width: 732px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="Form1" runat="server">
    <table class="style1">
        <tr>
            <td class="style4">
            <div class="column nameColumn">
                    <span class="bold"><span class="bluelable">Product Type:</span><span class=orangeLabel> Protein Enzymes</sapn></span>
                </div>
            </td>
            <td class="style2">
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="Protein_Enzyme_textbx" runat="server"></asp:TextBox>
                  <%-- connection string for textbox --%>
                
                </td>
                <td>
                  <%-- connection string for datalink --%>
                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
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
                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" GroupItemCount="15">
                    <ItemTemplate>
                       <tr VALIGN="top" id="Tr3"  runat="server">
                                   <td class="listnum">
                                        <%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.
                                       </td>
                                    <td class="listPnum">
                                       &#160; <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("Product_Number") %>'
                                            NavigateUrl='<%#"~/ProductInfo_ProteinEnzyme.aspx?Product_Number=" + Eval("Product_Number") %>'></asp:HyperLink>
                                    </td>
                                    <td class="listname">
                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Product_Name_Short") %>'
                                            NavigateUrl='<%#"~/ProductInfo_ProteinEnzyme.aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                    SelectCommand="SELECT [Product_Name_Short], [Product_Number] FROM [ProductDB] WHERE ([Product_Type_General] = @Product_Type_General) ORDER BY [Product_Name_Short], [Product_Number]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="Protein Enzyme" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
