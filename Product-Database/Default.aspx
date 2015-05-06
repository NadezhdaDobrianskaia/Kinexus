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
            margin-right: 0px;
            margin-top: 12px;
            margin-bottom: 0px;
        }

        .productsHomepageTopDiv{
            
        }

        /*-----------left column of the top part of the products homepage -------------*/
        .FirstLeftHomeDiv{
          float:left;
          width:32%;
          text-align:left;
          padding:0px;
          margin:0px;
         }
        

        /* category type list items*/
        .HomeCategoryList{
            list-style-type: none;
            padding:0px 15px 30px 15px; margin:0px;
            text-align:left;   
        }
        .CategoryDiv{
            min-height: 170px;
            border: 2px solid #496077;
            border-radius:15px;
            padding:5px 8px 8px 16px;
            margin-top:10px;
        }
        /*Homepage description*/
        .DescriptionHomePage{
            min-height: 360px;
            border: 2px solid #496077;
            border-radius:15px;
            padding:5px 8px 8px 16px;
            margin-top:10px;
        }
        /*----------- end of left column of the top part of the products homepage -------------*/



        /*-----------right column of the top part of the products homepage -------------*/
        .FirstRightHomeDiv{
            float:right;
            width:67%;
            padding:0px;
            margin:0px;
        }
        
        .HomeSearchDiv{
            border: 2px solid #496077;
            border-radius:15px;
            margin-top:10px;
            min-height: 110px;
            padding:5px 16px 8px 16px;
            
        }
        #HomeImageDiv {
            min-height: 420px;
            border: 2px solid #496077;
            border-radius:15px;
            padding:8px 8px 8px 8px; 
            margin-top:10px;
        }
        /*-----end of right column of the top part of the products homepage -------------*/
        
        /*---------bottom part of products homepage */
        .ProductsHomepageEnd{
          text-align:left;
          width:100%;
          margin-left:0px;
          margin-right: 0px;
          margin-top:5px;
          margin-bottom: 0px;
          clear: both;
        }
        .SecondLeftHomeDiv {
          float:left;
          width:64%;
          min-height: 290px;
          border: 2px solid #496077;
          border-radius:15px;
          padding:8px 4px 8px 16px;
          margin-top:10px;
        }
        .SecondRightHomeDiv {
            float:right;
            width:30%;
            height:290px;
            border: 2px solid #496077;
            border-radius:15px;
            margin-top:10px;
            padding:8px 4px 8px 16px;
           
        }
        .SecondRightHomeDiv p {
            clear:left;
        }
        .ProductsContact{
        }

        .newPdtNumbers{
            color: #99C7FF;
        }


        .newPdtHyperlink a:link{
            color:#e1e1e1;
            font-size:12px;
            margin-right: 5px;
        }

        .newPdtHyperlink a:hover{
            color: #99C7FF;
            margin-right: 5px;
        }

        .newPdtHyperlink a:visited {
            color: #e1e1e1;
                        margin-right: 5px;
        }

        .newPdtLeftColumn{
            float: left;
        }
        .newPdtRightColumn {
            float: right;
        }

       /*---------end of bottom part of products homepage */


    </style>
    
    <!-- script responsible to help populate the products homepage description from a "~/InfoHomepage/productsHomepageDescription.txt file-->
    <script type="text/javascript" language="javascript">
      $(document).ready(function () {
          var data = $("#<%=HiddenField1.ClientID %>").val();
          $('#pTextData').text(data);
      });
    </script>


    <!-- starting and stopping the timer-->
    <script type="text/javascript" language="javascript">
        function stopTimer(x) {
            
            var timer = $find("<%=Timer1.ClientID%>")
            timer._stopTimer();
           
        }
        function Start_Timer() {
            var timer = $find("<%=Timer1.ClientID%>")
            timer._startTimer();
        }
    </script>

    

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <form runat="server" method="post" style="height: auto;">
    <div  class ="productsHompepage">
        <div class="productsHomepageTopDiv">
            <div class ="FirstLeftHomeDiv">
                <!-- category list -->
               <div class="CategoryDiv">           
                <ul class="HomeCategoryList">
                    <li><h2>Search Category List</h2></li>
                    <li><asp:HyperLink ID="HyperLink1" runat="server" Text ="Antibody" NavigateUrl="~/ProductsList_Antibody.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink2" runat="server" Text ="Peptide" NavigateUrl="~/ProductsList_Peptide.aspx"></asp:HyperLink></li> 
                    <li><asp:HyperLink ID="HyperLink3" runat="server" Text ="Protein Enzyme" NavigateUrl="~/ProductsList_ProteinEnzyme.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink4" runat="server" Text ="Protein Substrate" NavigateUrl="~/ProductsList_ProteinSubstrate.aspx"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink5" runat="server" Text="Cell/Tissue Lysate"></asp:HyperLink></li>
                    <li><asp:HyperLink ID="HyperLink6" runat="server" Text="Array" NavigateUrl="~/ProductsList_Microarray.aspx"></asp:HyperLink></li>
                   <!-- <li><asp:HyperLink ID="HyperLink7" runat="server" Text="Enzyme Assay"></asp:HyperLink></li>   
                    <li><asp:HyperLink ID="HyperLink8" runat="server" Text="Bioactive Compound" NavigateUrl="~/ProductsList_BioactiveCompound.aspx"></asp:HyperLink></li>
                       -->                                                       
                </ul>
                </div>
                <!--Description of Kinexus products homepage-->
               <div class="DescriptionHomePage">
                   <asp:HiddenField ID="HiddenField1" runat="server" />
                   <p id="pTextData">
                    </p>
                </div>
            </div>

            <div class ="FirstRightHomeDiv">
               <!--Search of Kinexus products homepage-->
               <div class="HomeSearchDiv">
                <asp:PlaceHolder ID="search_box" runat="server"></asp:PlaceHolder>
                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString %>"
                SelectCommand="SELECT * FROM [ProductDB]" ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>"></asp:SqlDataSource>
                </div>
              <!--Pictures of Kinexus products homepage-->
               <div id="HomeImageDiv" >
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <!--<asp:HyperLink  ID="HyperLinkImage" runat="server" Text ="" NavigateUrl="http://google.com">   </asp:HyperLink>-->
                        <asp:ImageButton onmouseover="stopTimer(this)" onmouseout="Start_Timer()" ID="ImageButton1" runat="server" Height="410px" Width="600px" AlternateText="Promotional images" OnClick="ImageButton1_Click"/>
                        
                        

                        <!--<asp:Image ID="Image1" Height="410px" Width="600px" runat="server" />-->
                        <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick"></asp:Timer>
                     </ContentTemplate>
 
                </asp:UpdatePanel>   
                  
                </div>
            </div>
            
        </div>
        <div class="ProductsHomepageEnd">
           <!--New Products of Kinexus products homepage-->
            <div class="SecondLeftHomeDiv">
                <h2>New Products</h2>
                <!--left column connection to database and pull out the first 10 items-->
                <div class ="newPdtLeftColumn">
                    <asp:PlaceHolder ID="PlaceHolderNewPdtleft" runat="server">
                        <asp:SqlDataSource ID="SqlDataSourceNewPdtLeft" runat="server"
                            ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString %>"
                            SelectCommand="SELECT [New_Product_Order], [Product_Number],[Product_Type_General], [Brief_Description]
                             FROM [ProductDB] WHERE New_Product_Order > 0 AND New_Product_Order <11 ORDER BY New_Product_Order" 
                            ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>">                    <SelectParameters>
                                <asp:Parameter DefaultValue="Antibody" Name="Product_Type_General" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:PlaceHolder>

                    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSourceNewPdtLeft">                    
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td class="newPdtNumbers"><%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.</td>
                                    <td class="newPdtHyperlink"><asp:HyperLink ID="NewPdtHyperLink" runat="server" Text='<%# Eval("Brief_Description") %>'
                                                NavigateUrl='<%#"~/ProductInfo_" + Eval("Product_Type_General")+".aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:ListView>
                    </div>

                <!--connection to database and pull out the last 10 items-->
                    <div class ="newPdtRightColumn">
                    <asp:PlaceHolder ID="PlaceHolderNewPdtRight" runat="server">
                        <asp:SqlDataSource ID="SqlDataSourceNewPdtRight" runat="server"
                            ConnectionString="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString %>"
                            SelectCommand="SELECT [New_Product_Order], [Product_Number],[Product_Type_General], [Brief_Description]
                             FROM [ProductDB] WHERE New_Product_Order > 10 AND New_Product_Order <21 ORDER BY New_Product_Order" 
                            ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>">
                                                <SelectParameters>
                                <asp:Parameter DefaultValue="Antibody" Name="Product_Type_General" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:PlaceHolder>

                    <asp:ListView ID="ListView2" runat="server" DataSourceID="SqlDataSourceNewPdtRight">                    
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td class="newPdtNumbers"><%# Eval("New_Product_Order") %>.</td>
                                    <td class="newPdtHyperlink"><asp:HyperLink ID="NewPdtHyperLink" runat="server" Text='<%# Eval("Brief_Description") %>'
                                                NavigateUrl='<%#"~/ProductInfo_" + Eval("Product_Type_General")+".aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:ListView>
                    </div>
            </div>
            <div class="SecondRightHomeDiv">
                <a href="http://www.kinexus.ca/contact/contact.html"><h2>Contact</h2></a>
                <p class="ProductsContact">
                    <asp:PlaceHolder ID="PlaceHolderContact" runat="server">
                       


                    </asp:PlaceHolder>
                </p>
            </div>
        </div>
    </div>
    </form>
           

</asp:Content>
