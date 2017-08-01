
function ajaxcalling(clickeditem) {
    var cid = document.URL.split('=')[1].split('#')[0];
    $.ajax({
        url: '/Products/Getproducts',
        type: 'POST',
        data: JSON.stringify({ sub_category: clickeditem }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else {
                var url1 = 'Products/Getproductsbysubcategory?sub_category=' + data;
                $('#allproducts').empty.load(url1);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}


function getproducts(value) {
    var cid = document.URL.split('=')[1].split('#')[0];
    $.ajax({
        url: '/Products/Getproducts',
        type: 'POST',
        data: JSON.stringify({ sub_category: value }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else {
                var url = 'Products/Getproductsbysubcategory?sub_category=' + data;
                $('#allproducts').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}



//removing from cart
function removecart(cartid) {
    var cid = location.search.split('cid=')[1];

    $.ajax({
        url: '/Products/Removefromcart?cart_id=' + cartid,
        type: 'POST',
        data: JSON.stringify({}),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else {
                successmsg("Removed from Cart Successfully .");
                var url = 'Products/Addtocartpartial?cid=' + cid;
                $('#cartrecords').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });

}

//for genarate pos

function genaratepo() {
    var customerid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var cname = document.URL.split('&&')[1].split('=')[1];
    location.href = '/Products/GenaratePOs?cid=' + customerid + '&cname=' + cname;
}

$('#plus').click(function () {
    var qty = this.prev('input[type=text]').val();
    
});

//updatecart
function updatecart(cartid, quantity, costprice) {
    cid = location.search.split('cid=')[1];
    if (quantity > 0) {
        $.ajax({
            url: '/Products/UpdateCart',
            type: 'POST',
            data: JSON.stringify({ cart_id: cartid, Quantity: quantity, total_price: costprice }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    errormsg("Not Inserted");
                }
                else {
                    successmsg("Successfully Updated Cart.");
                    var url = 'Products/Addtocartpartial?cid=' + cid;
                    $('#cartrecords').load(url);

                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
    else {
        errormsg("Invalid Quantity");
        quantity = inputqty;
    }
}
function updatecart1(cartid, quantity, costprice) {

    //var inputqty = $('#qtyinput').val();

    if (quantity > 0) {
        cid = location.search.split('cid=')[1];
        $.ajax({
            url: '/Products/UpdateCart',
            type: 'POST',
            data: JSON.stringify({ cart_id: cartid, Quantity: quantity, total_price: costprice }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    errormsg("Not Inserted");
                }
                else {
                    successmsg("Successfully Updated Cart.");
                    var url = 'Products/Addtocartpartial?cid=' + cid;
                    $('#cartrecords').load(url);
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
    else {
        errormsg("Invalid Quantity");

    }
}

//for grand total

function calculate(totalprice) {
    var vat = $('#vat').val();
    var discount = $('#discount').val();
    if (parseInt(vat) < 100 && parseInt(discount) < 100) {
        var total = (parseInt(totalprice) + (parseInt(totalprice) * (parseInt(vat) / 100)) - (parseInt(discount) / 100));
        $('#grandtotal').text(total.toFixed(2));
    }
}

//for inserting purchseorder

function insertpo(totalamount) {
    $(".overlay").show();
    var cid = location.search.split('&')[0].split('cid=')[1];
    var cname = location.search.split('cname=')[1];
    var shipping_terms = $('#shipping_terms').val();
    var shipping_date = $('#shipping_date').val();
    var payment_terms = $('#payment_terms').val();
    var payment_date = $('#payment_date').val();
    var ponumber = $('#ponumber').val();
    var comment = $('#comment').val();
    var createddate = ($('#po_date').text()).split(';')[1];
    if (ponumber == "" || ponumber == null) {
        $(".overlay").hide();
        errormsg(" Please Enter PurchaseOrder Number");
    }
    else {
    $.ajax({
        url: '/Products/GenratePurchaseOrder',
        type: 'POST',
        data: JSON.stringify({ cid: cid, cname: cname, Prchaseorder_no: ponumber, created_date: createddate, Payment_date: payment_date, shipping_date: shipping_date, payment_terms: payment_terms, shipping_terms: shipping_terms, remarks: comment, sub_total: totalamount, grand_total: totalamount }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                $(".overlay").hide();
                errormsg(" Please Enter Purchase Order Number");
                $("[id='ponumber']").val("");
                $("[id='shipping_date']").val("");
                $("[id='payment_terms']").val("");
                $("[id='shipping_date']").val("");
                $("[id='shipping_terms']").val("");
                $("[id='payment_date']").val("");
            }

            if (data == "success") {
                $(".overlay").hide();
                successmsg("Successfully Purchase Order Generated.");
                $("[id='ponumber']").val("");
                $("[id='shipping_date']").val("");
                $("[id='payment_terms']").val("");
                $("[id='shipping_date']").val("");
                $("[id='shipping_terms']").val("");
                $("[id='payment_date']").val("");
                location.href = '/Products/PosOfCustomer?cid=' + cid + '&cname=' + cname;//window.location
            }
            if (data == "out of stock") {
                $(".overlay").hide();
                warnmsg("out of stock");
            }
            if (data == "shipdate") {
                $(".overlay").hide();
                warnmsg("Enter Shipping Date.It Can't be Empty");
            }
            if (data == "exists") {
                $(".overlay").hide();
                existsmsg("Purchase Order Number Already Exists.Please Enter a Unique Number");
                $("[id='ponumber']").val("");
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
    }
}



function checkponumber(passedvalue) {
    if (passedvalue == "" || passedvalue == null) {
        $(".overlay").hide();
        errormsg(" Please Enter PurchaseOrder Number");
    }
//else{
//    $.ajax({
//        url: '/Products/CheckPoNum',
//        type: 'POST',
//        data: JSON.stringify({ Prchaseorder_no: passedvalue }),
//        dataType: 'json',
//        contentType: 'application/json',
//        success: function (data) {
//            if (data == "unique") {

//            }
//            else {
//                existsmsg("Purchase Order Number Already Exists.Please Enter a Unique Number");
//                $("[id='ponumber']").val("");
//            }
//        },
//        error: function (data)
//        { errormsg("Failed!!!"); }
//    });
//}
}

//for displaying all pos
function viewpodetails(ponumber, cid) {
  
    location.href = '/Products/ViewPoDetails?Prchaseorder_no=' + ponumber + '&cid=' + cid;
}

function errormsg(msg) {
    $("body").overhang({
        type: "error",
        message: msg,
        closeConfirm: false
    });
}

function successmsg(msg) {
    $("body").overhang({
        type: "success",
        message: msg,
        duration: 3,
        closeConfirm: false
    });
}

function existsmsg(msg) {
    $("body").overhang({
        type: "exists",
        message: msg,
        duration: 3,
        closeConfirm: false
    });
}

function warnmsg(msg) {
    $("body").overhang({
        type: "warn",
        message: msg,
        duration: 3
    });
}

//for quntity to accept numbers only
function isNumberKey(evt, value) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        warnmsg("Enter Number only");
        return false;
    }
    return true;
}

//view invoice
function viewinvoicedetails(ponumber, cid) {
    
    location.href = '/Invoice/ViewInvoiceDetails?Prchaseorder_no=' + ponumber + '&cid=' + cid;

}

function allproducts() {
    //var url = '/Products/allproducts';
    //$('#allproducts').empty().load(url);
    location.reload();
}