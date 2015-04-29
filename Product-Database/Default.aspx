<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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
        .productsHomepage{
            width:100%;
            margin-left:1px;
            height: auto;
            margin-right: 0px;
            margin-top: 12px;
            margin-bottom: 0px;
        }

        /**/
        .FirstLeftHomeDiv{
          float:left;
          width:32%;
          height: auto;
          text-align:left;
         padding:0px;
         margin:0px;

         }
        .FirstRightHomeDiv{
            float:right;
            width:67%;
           padding:0px;
           margin:0px;
        }

         .HomeCategoryList{
            list-style-type: none;
            padding:0px 15px 30px 15px; margin:0px;
            text-align:left;   
        }

        .HomeDiv{
            height: auto;
            border: 2px solid #496077;
            border-radius:15px;
            padding:16px 8px 8px 16px;
            height: auto;
            margin-top:10px;
        }

        .HomeSearchDiv{
            border: 2px solid #496077;
            border-radius:15px;
            margin-top:10px;
            height: auto;
            padding-left:15px;
            padding-right: 15px;
        }
        .HomeImageDiv {
            border: 2px solid #496077;
            border-radius:15px;
            padding:8px 8px 8px 8px; 
            height: auto;
            margin-top:10px;
        }

        .ProductsHomepageEnd{
          text-align:left;
          width:100%;
          margin-left:0px;
          height: auto;
          margin-right: 0px;
          margin-top:5px;
          margin-bottom: 0px;
          clear: both;
        }
        .SecondLeftHomeDiv {
          float:left;
          width:64%;
          min-height: 200px;
          border: 2px solid #496077;
          border-radius:15px;
          padding:8px 4px 8px 16px;
          margin-top:10px;
        }
        .SecondRightHomeDiv {
            float:right;
            width:30%;
            border: 2px solid #496077;
            border-radius:15px;
            margin-top:10px;
            padding:8px 4px 8px 16px;
           
        }
        .SecondRightHomeDiv p {
            clear:left;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <form runat="server" method="post" style="height: auto;">
        <div class="productsHomepage">
            <div class ="FirstLeftHomeDiv">
               <div class="HomeDiv">           
                <ul class="HomeCategoryList">
                    <li><h2>Search Category</h2></li>
                    <li><asp:HyperLink ID="HyperLink1" runat="server" Text ="Antibody" NavigateUrl="~/ProductsList_Antibody.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink2" runat="server" Text ="Protein" NavigateUrl="~/ProductsList_Protein.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink3" runat="server" Text ="Peptide" NavigateUrl="~/ProductsList_Peptide.aspx"></asp:HyperLink></li> 
                    <li><asp:HyperLink ID="HyperLink4" runat="server" Text="Cell/Tissue Lysate"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink5" runat="server" Text="Array" NavigateUrl="~/ProductsList_Microarray.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink6" runat="server" Text="Enzyme Assay"></asp:HyperLink></li>   
                    <li><asp:HyperLink ID="HyperLink7" runat="server" Text="Bioactive Compound" NavigateUrl="~/ProductsList_BioactiveCompound.aspx"></asp:HyperLink></li>                                                       
                </ul>
                </div>
               <div class="HomeDiv">
                <p>This is where the text about the company will go. aksjdflksdjfkdsjfkljsldfkjsldkf alkdjlfaksjdflksjd ;lajdlfkj lkajdslkfdjs ;lkajdsflksjd lkajds;fklj ;akljdsf;laskjf alkdfsj;ladskj kaljdladskjf ;lkajdflkdfjs ;lakjdslfkajdsf ;lkajdlfkjads ;lkajdlkjafds;lk lakjds;lfakdsjlf;l aldskja;dlkjj a;lkdjf;sldkj ;alkdsjf;ldskj ;lakdsjfladskjf ;lkajdslfkjdsl;f ;akljdslfkj </p>
                </div>
            </div>

            <div class ="FirstRightHomeDiv">
               <div class="HomeSearchDiv">
                <asp:PlaceHolder ID="search_box" runat="server"></asp:PlaceHolder>

                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionStringServer %>"
                SelectCommand="SELECT * FROM [ProductDB]"></asp:SqlDataSource>

            </div>
               <div class="HomeImageDiv">
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
            
        </div>
        <div class="ProductsHomepageEnd">
            <div class="SecondLeftHomeDiv"><p>this is where the list will go</p></div>
            <div class="SecondRightHomeDiv"><p>this is where the description will go</p></div>
        </div>
    </form>
           

</asp:Content>

