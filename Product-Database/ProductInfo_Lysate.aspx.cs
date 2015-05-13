using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class ProductInfo_Lysate : ProductInfo
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (loadData())
            {

                BuildCommonFirstRow();
                AddLableInofPairToColum("Full Name: ", GetDBValue("Product_Name_Long"));
                AddLableInofPairToColum("Alias: ", GetDBValue("Product_Name_Alias"));
                AddLableInofPairToColum("Product Type Specific: ", GetDBValue("Product_Type_Specific"));
                AddLableInofPairToColum("Product Description: ", GetDBValue("Product_Description"));
                AddLableInofPairToColum("Product Method: ", GetDBValue("Production_Method"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Lysate Use Description: ", GetDBValue("Lysate_Use_Description"));
                AddLableLinkPairToColum("Related Product 1: ", GetDBValue("Related_Product_1_Url"), GetDBValue("Related_Product_1"));
                AddLableLinkPairToColum("Related Product 2: ", GetDBValue("Related_Product_2_Url"), GetDBValue("Related_Product_2"));
                AddLableLinkPairToColum("Related Product 3: ", GetDBValue("Related_Product_3_Url"), GetDBValue("Related_Product_3"));
                AddLableLinkPairToColum("Related Product 4: ", GetDBValue("Related_Product_4_Url"), GetDBValue("Related_Product_4"));
                AddLableLinkPairToColum("Related Product 5: ", GetDBValue("Related_Product_5_Url"), GetDBValue("Related_Product_5"));
                AddLableLinkPairToColum("Related Product 6: ", GetDBValue("Related_Product_6_Url"), GetDBValue("Related_Product_6"));
                AddLableLinkPairToColum("Related Product 7: ", GetDBValue("Related_Product_7_Url"), GetDBValue("Related_Product_7"));
                AddLableLinkPairToColum("Related Product 8: ", GetDBValue("Related_Product_8_Url"), GetDBValue("Related_Product_8"));
                AddLableLinkPairToColum("Related Product 9: ", GetDBValue("Related_Product_9_Url"), GetDBValue("Related_Product_9"));
                AddLableLinkPairToColum("Related Product 10: ", GetDBValue("Related_Product_10_Url"), GetDBValue("Related_Product_10"));
                AddLableLinkPairToColum("Customer Information Package Download", GetDBValue("Info_Package_Url"), GetDBValue("Info_Package_Url"));
                BuildTargetLinksHTML();
                BuildFiquersHTML();
                BuildreferencesHTML();
                BuildAdColum();
                FillExtarColums();
                output.Text += outputHTML;
            }
        }
    }
}