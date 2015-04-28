﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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

        .leftHomeDiv{
          float:left;
          border: 2px solid #8AC007;
          width: auto;
          height: auto;
         }

        .rightHomeDiv{
        float:right;
        border: 2px solid #8AC007;
        width:auto;
        
         }

    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
        <form runat="server" method="post" style="height: 456px">
            <div class ="leftHomeDiv">
               <div>    
                <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server">HyperLink</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server">HyperLink</asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" runat="server">HyperLink</asp:HyperLink>
                <asp:HyperLink ID="HyperLink5" runat="server">HyperLink</asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" runat="server">HyperLink</asp:HyperLink>
                </div>
               <div>
                <p>This is where the text about the company will go</p>
                </div>
            </div>

            <div class ="rightHomeDiv">
               <div>
                <asp:PlaceHolder ID="search_box" runat="server"></asp:PlaceHolder>

                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                SelectCommand="SELECT * FROM [ProductDB]"></asp:SqlDataSource>

            </div>
               <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Image ID="Image1" Height="240px" Width="320px" runat="server" />    
    
                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>    
            </div>
            </div>
        </form>
   

</asp:Content>

