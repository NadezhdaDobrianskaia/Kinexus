using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Product_Database
{
    public partial class ProductInfo_BioactiveCompound : ProductInfo
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
                AddLableInofPairToColum("Scientific Background: ", GetDBValue("Scientific_Background"));
                AddLableInofPairToColum("Compound Empirical Formula: ", GetDBValue("Compound_Empirical_Formula"));
                AddLableInofPairToColum("Compound Molecular Weight: ", GetDBValue("Compound_Molecular_Weight"));
                AddLableInofPairToColum("Compound MDL Number: ", GetDBValue("Compound_MDL_Number"));
                AddLableInofPairToColum("Compound PubChem Substance ID: ", GetDBValue("Compound_PubChem_Substance_ID"));
                AddLableInofPairToColum("Compound Production Method: ", GetDBValue("Compound_Production_Method"));
                AddLableInofPairToColum("Compound Purity: ", GetDBValue("Compound_Purity"));
                AddLableInofPairToColum("Compound Appearance: ", GetDBValue("Compound_Appearance"));
                AddLableInofPairToColum("Compound Form: ", GetDBValue("Compound_Form"));
                AddLableInofPairToColum("Compound Solubility: ", GetDBValue("Compound_Solubility"));
                AddLableInofPairToColum("Storage Conditions: ", GetDBValue("Storage_Conditions"));
                AddLableInofPairToColum("Storage Stability: ", GetDBValue("Storage_Stability"));
                AddLableInofPairToColum("Compound Safety Issues: ", GetDBValue("Compound_Safety_Issues"));
                AddLableInofPairToColum("Product Use: ", GetDBValue("Product_Use"));
                AddLableInofPairToColum("Compound Target Description: ", GetDBValue("Compound_Target_Description"));
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