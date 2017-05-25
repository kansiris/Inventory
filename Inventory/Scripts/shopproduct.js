
function ajaxcalling(clickeditem) {
    var cid = document.URL.split('=')[1].split('#')[0];
    $.ajax({
        url: '/Products/Getproducts',
        type: 'POST',
        data: JSON.stringify({ sub_category: clickeditem }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            alert(data);
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
            alert(data);
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
    alert(customerid);
    var cname = document.URL.split('&&')[1].split('=')[1];
    alert(cname);

    location.href = '/Products/GenaratePOs?cid=' + customerid + '&cname=' + cname;


}

//updatecart
function updatecart(cartid, quantity, costprice) {
    cid = location.search.split('cid=')[1];
    newquant = quantity + 1;
    newamunt = costprice * newquant;
    $.ajax({
        url: '/Products/UpdateCart',
        type: 'POST',
        data: JSON.stringify({ cart_id: cartid, Quantity: newquant, total_price: newamunt }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("not inserted");
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
function updatecart1(cartid, quantity, costprice) {

    newquant = quantity - 1;
    newamunt = costprice * newquant;
    cid = location.search.split('cid=')[1];
    $.ajax({
        url: '/Products/UpdateCart',
        type: 'POST',
        data: JSON.stringify({ cart_id: cartid, Quantity: newquant, total_price: newamunt }),
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
    var cid=location.search.split('&')[0].split('cid=')[1];
    var cname = location.search.split('&')[1].split('cname=')[1];
    alert(totalamount);
    alert($("[id='shipping_terms']").val());
    alert($("[id='shipping_date']").val());
    alert($("[id='payment_terms']").val());
    alert($("[id='payment_date']").val());
    alert($("[id='ponumber']").val());
    alert($("[id='comment']").val());
    alert($("[id='vat']").val());
    alert($("[id='discount']").val());
    var shipping_terms = $('#shipping_terms').val();
    var shipping_date = $('#shipping_date').val();
    var payment_terms = $('#payment_terms').val();
    var payment_date = $('#payment_date').val();
    var ponumber = $('#ponumber').val();
    var comment = $('#comment').val();
    var vat = $('#vat').val();
    var discount = $('#discount').val();
    var grand_total = $('#grandtotal').text();
    var createddate = $('#po_date').text();
    //Date of create Date :  document.write(new Date().toLocaleDateString('en-GB'));25/05/2017
    var dateofcreate = createddate.split(';')[1];
    alert("Date of create"+createddate.split(';')[1]);
  
    $.ajax({
        url: '/Products/GenratePurchaseOrder',
        type: 'POST',
        data: JSON.stringify({ cid: cid, cname: cname, Prchaseorder_no: ponumber, created_date: dateofcreate, Payment_date: payment_date, shipping_date: shipping_date, payment_terms: payment_terms, shipping_terms: shipping_terms, remarks: comment, sub_total: totalamount, vat: vat, discount: discount, grand_total: grand_total }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("Not Inserted");
            }

            else {
                successmsg("Successfully Inserted");
                //var url = 'Products/Addtocartpartial?cid=' + cid;
                //$('#cartrecords').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
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