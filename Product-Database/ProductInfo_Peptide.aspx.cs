using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class ProductInfo_Peptides : ProductInfo
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (loadData())
            {

                BuildCommonFirstRow();
                AddLableInofPairToColum("Peptide Name: ", GetDBValue("Pep_Name"));
                AddLableInofPairToColum("Alias: ", GetDBValue("Product_Name_Alias"));
                AddLableInofPairToColum("Product Use: ", GetDBValue("Product_Use"));
                AddLableInofPairToColum("Scientific Background: ", GetDBValue("Scientific_Background"));
                AddLableInofPairToColum("Peptide Production Method: ", GetDBValue("Pep_Production_Method"));
                AddLableInofPairToColum("Peptide Origin: ", GetDBValue("Pep_Origin"));
                AddLableInofPairToColum("Peptide Sequence: ", GetDBValue("Pep_Sequence"));
                AddLableInofPairToColum("Peptide Modifications N Terminus: ", GetDBValue("Pep_Modifications_N_Terminus"));
                AddLableInofPairToColum("Peptide Modifications C Terminus ", GetDBValue("Pep_Modifications_C_Terminus"));
                AddLableInofPairToColum("Peptide Modifications Other: ", GetDBValue("Pep_Modifications_Other"));
                AddLableInofPairToColum("Peptide Molecular Mass Calculated : ", GetDBValue("Product_Molecular_Mass_Calculated"));
                AddLableInofPairToColum("Peptide Purity: ", GetDBValue("Pep_Purity"));
                AddLableInofPairToColum("Peptide Appearance: ", GetDBValue("Pep_Appearance"));
                AddLableInofPairToColum("Peptide Form: ", GetDBValue("Pep_Form"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Storage Stability: ", GetDBValue("Storage_Stability"));
                AddLableInofPairToColum("Peptide Recommended Enzyme:", GetDBValue("Pep_Recommended_Enzyme"));
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