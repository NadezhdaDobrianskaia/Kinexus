using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class ProductInfo_Microarray : ProductInfo
    {
        /*
         * 
         * 
         * Full Name:	Product_Name_Long
Alias:	Product_Name_Alias
Product Type Specific:	Product_Type_Specific
Product Description:	Product_Description
Scientific Background:	Scientific_Background
Production Method:	Production_Method
Storage Conditions:	Storage_Conditions
Storage Stability:	Storage_Stability
Microarray Use Description: 	Array_Use_Description
Microarray Probe Description: 	Array_Probe_Description
Microarray Slide Type: 	Array_Slide_Type
Microarray Slide Methodology: 	Array_Slide_Methodology

         * */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (loadData())
            {

                BuildCommonFirstRow();
                AddLableInofPairToColum("Full Name: ", GetDBValue("Product_Name_Long"));
                AddLableInofPairToColum("Alias: ", GetDBValue("Product_Name_Alias"));
                AddLableInofPairToColum("Product Type Specific: ", GetDBValue("Product_Type_Specific"));
                AddLableInofPairToColum("Scientific Background: ", GetDBValue("Scientific_Background"));
                AddLableInofPairToColum("Production Method: ", GetDBValue("Production_Method"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Storage Stability: ", GetDBValue("Storage_Stability"));
                AddLableInofPairToColum("Microarray Use Description:  ", GetDBValue("Array_Use_Description"));
                AddLableInofPairToColum("Microarray Probe Description: ", GetDBValue("Array_Probe_Description"));
                AddLableInofPairToColum("Microarray Slide Type: ", GetDBValue("Array_Slide_Type"));
                AddLableInofPairToColum("Microarray Slide Methodology: ", GetDBValue("Array_Slide_Methodology"));
                HtmlBufferFlush();
                BuildFiquersHTML();
                BuildreferencesHTML();
                BuildAdColum();
                FillExtarColums();
                output.Text += outputHTML;
            }
        }
    }
}