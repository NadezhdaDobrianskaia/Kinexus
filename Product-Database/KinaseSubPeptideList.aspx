<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KinaseSubPeptideList.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="Product_Database.KinaseSubPeptideList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="kinaseSub">
        <div id="kinaseSubHeader" class="column kinaseHeader" style="height: 30px; text-align:center; font-size:18px;">
           <span class="bluelable">Recommended Substrate Peptides <span style="color:#999999;">(click on kinase short names to generate tables)</span></span>
            <div class="column">
                <table cellpadding="5px;">
                    <tr>
                        <th width="160px">
                            <span class="bluelable">Kinase Short Name</span>
                        </th>
                        <th>
                            <span class="bluelable">Long Name</span>
                        </th>
                        <th>
                            <span class="bluelable">Alias</span>
                        </th>
                        <th width="80px">
                            <span class="bluelable">UniProt ID</span>
                        </th>
                    </tr>
                    <%foreach (ArrayList item in row)
                      {
                          Response.Write("<tr>");
                          foreach (string product in item)
                          {
                              Response.Write("<td valign=\"top\">" + product + "</td>");
                          }
                          Response.Write("</tr>");
                      } %>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
