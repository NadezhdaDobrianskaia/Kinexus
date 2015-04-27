using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class ProductInfo_ProteinEnzyme : ProductInfo
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (loadData())
            {

                BuildCommonFirstRow();
                AddLableInofPairToColum("Full Name: ", GetDBValue("Product_Name_Long"));
                AddLableInofPairToColum("Alias: ", GetDBValue("Product_Name_Alias"));
                AddLableInofPairToColum("Product Type Specific: ", GetDBValue("Product_Type_Specific"));
                AddLableLinkPairToColum("Protein UniProt: ", GetDBValue("Prot_UniProt_Url"), GetDBValue("Prot_Uniprot"));
                AddLableLinkPairToColum("Protein SigNET: ", GetDBValue("Prot_SigNET_Link"), GetDBValue("Prot_Uniprot"));
                AddLableInofPairToColum("Scientific Background: ", GetDBValue("Scientific_Background"));
                AddLableInofPairToColum("Product Use: ", GetDBValue("Product_Use"));
                AddLableInofPairToColum("Product Description: ", GetDBValue("Product_Description"));
                AddLableInofPairToColum("Product Molecular Mass Calculated: ", GetDBValue("Product_Molecular_Mass_Calculated"));
                AddLableInofPairToColum("Protein Molecular Mass Measured: ", GetDBValue("Prot_Molecular_Mass_Measured"));
                AddLableInofPairToColum("Production Method: ", GetDBValue("Production_Method"));
                AddLableInofPairToColum("Protein Species: ", GetDBValue("Prot_Species"));
                AddLableInofPairToColum("Protein Production Species: ", GetDBValue("Prot_Production_Species"));
                AddLableInofPairToColum("Protein Modifications: ", GetDBValue("Prot_Modifications"));
                AddLableInofPairToColum("Protein Concentration: ", GetDBValue("Prot_Concentration"));
                AddLableInofPairToColum("Protein Purity: ", GetDBValue("Prot_Purity"));
                AddLableInofPairToColum("Storage Buffer: ", GetDBValue("Storage_Buffer"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Storage Stability: ", GetDBValue("Storage_Stability"));
                AddLableInofPairToColum("Protein Recommended Enzyme: ", GetDBValue("Prot_Recommended_Enzyme"));
                AddLableInofPairToColum("Protein Activity: ", GetDBValue("Prot_Activity"));
                AddLableInofPairToColum("Protein Recommended Substrate: ", GetDBValue("Prot_Recommended_Substrate"));
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