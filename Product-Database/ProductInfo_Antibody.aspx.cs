using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;

namespace Product_Database
{
    public partial class ProductInfo_Antibody : ProductInfo
    {
        
         protected void Page_Load(object sender, EventArgs e)
        {
            if (loadData())
            {
                BuildCommonFirstRow();
                AddLableInofPairToColum("Target Full Name: ", GetDBValue("Product_Name_Long"));
                AddLableInofPairToColum("Target Alias: ", GetDBValue("Product_Name_Alias"));
                AddLableInofPairToColum("Product Type Specific: ", GetDBValue("Product_Type_Specific"));
                AddLableInofPairToColum("Antibody Code: ", GetDBValue("Product_Type_Specific"));
                AddLableInofPairToColum("Antibody Target Type: ", GetDBValue("Ab_Target_Type"));
                AddLableInofPairToColum("Antibody Phosphosite:  ", GetDBValue("Ab_Phosphosite"));
                AddLableLinkPairToColum("Protein UniProt: ", GetDBValue("Prot_UniProt_Url"), GetDBValue("Prot_Uniprot"));
                AddLableLinkPairToColum("Protein SigNET: ", GetDBValue("Prot_SigNET_Link"), GetDBValue("Prot_Uniprot"));
                AddLableInofPairToColum("Scientific Background: ", GetDBValue("Scientific_Background"));
                AddLableInofPairToColum("Antibody Type: ", GetDBValue("Ab_Type"));
                AddLableInofPairToColum("Antibody Host Species: ", GetDBValue("Ab_Host_Species"));
                AddLableInofPairToColum("Antibody Ig Isotype Clone: ", GetDBValue("Ab_Ig_Isotype_Clone_Lot"));
                AddLableInofPairToColum("Antibody Immunogen Source: ", GetDBValue("Ab_Immunogen_Source"));
                AddLableInofPairToColum("Antibody Immunogen Sequence: ", GetDBValue("Ab_Immunogen_Sequence"));
                AddLableInofPairToColum("Antibody Immunogen Description: ", GetDBValue("Ab_Immunogen_Description"));
                AddLableInofPairToColum("Production Method: ", GetDBValue("Production_Method"));
                AddLableInofPairToColum("Antibody Modification: ", GetDBValue("Ab_Modification"));
                AddLableInofPairToColum("Antibody Concentration: ", GetDBValue("Ab_Concentration"));
                AddLableInofPairToColum("Storage Buffer: ", GetDBValue("Storage_Buffer"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Storage Stability: ", GetDBValue("Storage_Stability"));
                AddLableInofPairToColum("Product Use: ", GetDBValue("Product_Use"));
                AddLableInofPairToColum("Antibody Dilution Recommended: ", GetDBValue("Ab_Dilution_Recommended"));
                AddLableInofPairToColum("Antibody Potency: ", GetDBValue("Ab_Potency"));
                AddLableInofPairToColum("Antibody Species Reactivity: ", GetDBValue("Ab_Species_Reactivity"));
                AddLableInofPairToColum("Antibody Positive Control:  ", GetDBValue("Ab_Pos_Control"));
                AddLableInofPairToColum("Antibody Specificity: ", GetDBValue("Ab_Specificity"));
                AddLableInofPairToColum("Antibody Cross Reactivity: ", GetDBValue("Ab_Cross_Reactivity"));
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
                HtmlBufferFlush();
                BuildFiquersHTML();
                BuildreferencesHTML();
                BuildAdColum();
                //FillExtarColums();
                output.Text += outputHTML;
            }
            
        }
    }
}