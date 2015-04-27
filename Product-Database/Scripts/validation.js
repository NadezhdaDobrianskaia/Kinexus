$(document).ready(function () {
    if ($('#ctl00_MainContent_ci_box_same_altadd').is(':checked')) {
        alternate();
    }
    purchaseOrder($('#ctl00_MainContent_ci_purchaseorder_box'));
    creditOrder($('#ctl00_MainContent_ci_creditcard_box'));
    setupAutoComplete();
    setupAutoCompleteNum();
});

function alternate() {
    $('#alt_address_table').toggle();
}

//update the 
function updateSize(element, index) {
    var data = element.val();
    var price = parseFloat($("#prod_size" + index + " option:selected").attr("price"));
    $('#unit_price' + index).val('$' + price.toFixed(2));
    calculateCost(index);
}

//adjust the required fields depending on the checkbox
function purchaseOrder(element) {
    if (element.is(':checked')) {
        $('#ctl00_MainContent_ci_ordernumber_box').keyup(function () { orderNumber($('#ctl00_MainContent_ci_ordernumber_box')) });  //.attr("onblur", "required($(this))");        
        orderNumber($('#ctl00_MainContent_ci_ordernumber_box'));        
        //required($('#ctl00_MainContent_ci_ordernumber_box'));
    } else {
        $('#ctl00_MainContent_ci_ordernumber_box').removeClass("invalid_field");
        $('#ctl00_MainContent_ci_ordernumber_box').unbind("keyup");
        $('#ctl00_MainContent_ci_order_req_hidden').attr("style", "color: black");
        //required($('#ctl00_MainContent_ci_creditcard_box'));
    }
}

function creditOrder(element) {
    if (element.is(':checked')) {
        $('#ctl00_MainContent_ci_cardnumber_box').keyup(function () { creditOrder($('#ctl00_MainContent_ci_cardnumber_box')) }); //.blur(required($(this)));  //attr("onblur", "required($(this))");
        credit($('#ctl00_MainContent_ci_ci_cardnumber_box'));
        //required($('#ctl00_MainContent_ci_ordernumber_box'));
    } else {
        $('#ctl00_MainContent_ci_cardnumber_box').removeClass("invalid_field");
        $('#ctl00_MainContent_ci_cardnumber_box').unbind("keyup");
        $('#ctl00_MainContent_ci_credit_req_hidden').attr("style", "color: black");
        //required($('#ctl00_MainContent_ci_creditcard_box'));
    }
}

//add comas to integral values for currency
function dollarFormat(amount) {
    var output = "";
    var count = 0;

    for (var i = amount.length - 4; i >= 0; i--) {
        var oneChar = amount.charAt(i);
        if (count == 3) {
            output = "," + output;
            output = oneChar + output;
            count = 1;
        }
        else {
            output = oneChar + output;
            count++;
        }
    }
    return output + ".00";
}

//retrieve product information
function getProductData(index, name, type) {
    $.post("OrderFormScript.aspx", { name: name, index: index, type: type },
    function (data) {
        if (data.length >= 1) {
            $('#order_row' + index).html(data);
            calculateCost(index);
            setupAutoComplete();
            setupAutoCompleteNum();
        }
    });
}

//required field
function required(element) {
    var data = element.val();
    if (data.toString().length > 0) {
		element.removeClass('invalid_field');
	} else {
		element.addClass('invalid_field');
	}
    submit();
}

//email field
function email(element) {
    var data = element.val();
    var pattern = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (data.toString().match(pattern)) {
		element.removeClass('invalid_field');
	} else {
		element.addClass('invalid_field');
	}
    submit();
}

//update cost
function calculateCost(index) {
    var quantity = parseFloat($('#unit_number' + index).val());
    var price = parseFloat(removeDollar($('#unit_price' + index).val()));
    if (!isNaN(quantity) && !isNaN(price)) {
        $('#cost' + index).val('$' + dollarFormat(parseFloat(quantity * price).toFixed(2)));
    }
    $('#subtotal').html('$' + dollarFormat(calculateTotal().toFixed(2)));
}

//only positive values
function numeric(index) {
    var value = new String($('#unit_number' + index).val());
    $('#unit_number' + index).val(value.replace(/[^0-9]/g, ''));
    calculateCost(index);    
}

//update total cost
function calculateTotal() {
    var total = 0;
    for (var i = 1; i <= 10; i++) {
        var num = new String(removeDollar($('#cost' + i).val()));
        var value = new String(num.substring(0, num.length - 3));
        var cost = parseFloat(value.replace(/,/, ''));
        if (!isNaN(cost)) {
            total += cost;
        }
    }
    return total;
}

//remove $
function removeDollar(field) {
    if (field.charAt(0) == '$')
        return field.substring(1, field.length);
    return field;
}

//creditcard field
function credit(element) {
    var data = element.val();
    //if (data.toString().match('^(4[0-9]{12}(?:[0-9]{3})?)|(5[1-5][0-9]{14})$')) {
    if (data != null && data.toString().length == 16) {
        element.removeClass('invalid_field');
        $('#ctl00_MainContent_ci_credit_req_hidden').attr("style", "color: black");
	} else {
        element.addClass('invalid_field');
        $('#ctl00_MainContent_ci_credit_req_hidden').attr("style", "color: red");
	}
    submit();
}

//submit the page
function submit() {
    if ($('.invalid_field').size() > 0) {
        $('#data-error').html('you must fix all errors');
        $('#ctl00_MainContent_ci_submit').attr('disabled', 'disabled');
    } else {
        $('#data-error').html('');
        $('#ctl00_MainContent_ci_submit').removeAttr('disabled');
    }
}

//purchase order number field
function orderNumber(element) {
    var data = element.val();
    //if (data.toString().match('^(4[0-9]{12}(?:[0-9]{3})?)|(5[1-5][0-9]{14})$')) {
    if (data.toString().length > 0) {
        element.removeClass('invalid_field');
        $('#ctl00_MainContent_ci_order_req_hidden').attr("style", "color: black");
    } else {
        element.addClass('invalid_field');
        $('#ctl00_MainContent_ci_order_req_hidden').attr("style", "color: red");
    }
    submit();
}

function setupAutoComplete() {
    $(".auto_complete").autocomplete({
        source: "ProductDataList.aspx",
        minLength: 1,
        select: function (event, ui) {
            var id = this.id.toString();
            id = id.charAt(id.length - 1);
            if (parseInt(id) == 0) {
                id = 10;
            }
            getProductData(id, ui.item.value, "_name");
        }
    });
}

function setupAutoCompleteNum() {
    $(".auto_complete_num").autocomplete({
        source: "ProductDataList.aspx?mode=num",
        minLength: 1,
        select: function (event, ui) {
            var id = this.id.toString();
            id = id.charAt(id.length - 1);
            if (parseInt(id) == 0) {
                id = 10;
            }
            getProductData(id, ui.item.value, "_id");
        }
    });
}