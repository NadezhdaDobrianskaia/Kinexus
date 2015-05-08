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
            padding:5px 8px 8px 16px;
            height: auto;
            margin-top:10px;
        }
        .HomeSearchDiv{
            border: 2px solid #496077;
            border-radius:15px;
            margin-top:10px;
            height: auto;
            padding:5px 16px 8px 16px;
            
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
        .ProductsContact{
        font-size:13px;
        }
    </style>
    
    <!-- script responsible to help populate the products homepage description from a "~/InfoHomepage/productsHomepageDescription.txt file-->
    <script type="text/javascript" language="javascript">
      $(document).ready(function () {
          var data = $("#<%=HiddenField1.ClientID %>").val();
          $('#pTextData').text(data);
      });
    </script>



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
                   <asp:HiddenField ID="HiddenField1" runat="server" />
                   <p id="pTextData">
                    </p>
                  
                </div>
            </div>

            <div class ="FirstRightHomeDiv">
               <div class="HomeSearchDiv">
                <asp:PlaceHolder ID="search_box" runat="server"></asp:PlaceHolder>

                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:comp4900ConnectionString2 %>"
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
            <div class="SecondLeftHomeDiv">
                <h2>New Products</h2>
                <asp:PlaceHolder ID="PlaceHolderNewPdt" runat="server">

                
                <asp:SqlDataSource ID="SqlDataSourceNewPdt" runat="server"
                     ConnectionString="<%$ ConnectionStrings:comp4900ConnectionString2 %>"
                    SelectCommand="SELECT [New_Product_Order], [Product_Number],[Product_Type_General], [Brief_Description] FROM [ProductDB] WHERE New_Product_Order !=0" ProviderName="<%$ ConnectionStrings:Kinexus Protein ProductDBConnectionString.ProviderName %>">                    <SelectParameters>
                        <asp:Parameter DefaultValue="Antibody" Name="Product_Type_General" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
                </asp:PlaceHolder>

                <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSourceNewPdt">
                    
                    <ItemTemplate>


                        <table>
                            
                            <tr>
                                <td><%# (((ListViewDataItem)Container).DisplayIndex + 1) %>.</td>
                                <td><asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Brief_Description") %>'
                                            NavigateUrl='<%#"~/ProductInfo_" + Eval("Product_Type_General")+".aspx?Product_Number=" + Eval("Product_Number")%>'></asp:HyperLink>
                                </td>
                            
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div class="SecondRightHomeDiv">
                <p class="ProductsContact">
                    <asp:PlaceHolder ID="PlaceHolderContact" runat="server">
                       


                    </asp:PlaceHolder>
                </p>
            </div>
        </div>
    </form>
           

</asp:Content>
