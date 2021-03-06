﻿$(document).ready(function () {
    $("#ctl00_MainContent_Antibody_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Antibody",
        minLength: 1
    });
    $("#ctl00_MainContent_Protein_Enzyme_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Protein Enzyme",
        minLength: 1
    });
    $("#ctl00_MainContent_Protein_Substrate_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Protein Substrate ",
        minLength: 1
    });
    $("#ctl00_MainContent_Microarray_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Microarray",
        minLength: 1
    });
    $("#ctl00_MainContent_Bioactive_Compound_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Bioactive Compound",
        minLength: 1
    });
    $("#ctl00_MainContent_Peptide_textbx").autocomplete({
        source: "ProductDataList.aspx?cat=Peptide",
        minLength: 1
    });
    
    //adds extra mark 
    $(".column").each(function (i) {
        var colname = "colum-hight";

        if ($(this).hasClass("miniColumn")) {
            colname = "miniColumn";
        }
        if ($(this).hasClass("medColumn")) {
            colname = "medColumn";
        }
        if ($(this).hasClass("nameColumn")) {
            colname = "nameColumn";
        }
        $(this).wrapInner('<div class="' + colname + ' col-inner" />')
        $(this).prepend('<div class="colUE"><div class="colULC"></div><div class="colURC"></div></div> <div class="' + colname + ' colLE"></div><div class="' + colname + '  colRE"></div>')
    });
    $('.col-inner').after(' <div class="colBE"><div class="colLLC"></div><div class="colLRC"></div></div>')
});
function rmText(elm) {
    if ($(elm).val() == "Enter Search Term") {
        $(elm).val("");
    }
}
function rpText(elm) {
    if ($(elm).val() == "") {
        $(elm).val("Enter Search Term");
    }
}