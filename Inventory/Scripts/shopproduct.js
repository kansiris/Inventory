
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
                alert("nodata available");
            }
            else {
                var url1 = 'Products/Getproductsbysubcategory?sub_category=' + data;
                $('#allproducts').empty.load(url1);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
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
                alert("nodata available");
            }
            else {
                var url = 'Products/Getproductsbysubcategory?sub_category=' + data;
                $('#allproducts').load(url);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
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
                alert("no data available");
            }
            else {
                alert("Removed from cart Successfully .");
                var url = 'Products/Addtocartpartial?cid=' + cid;
                $('#cartrecords').empty.load(url);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

//for genarate pos

function genaratepo() {
    var customerid = document.URL.split('=')[1];
    location.href = '/Products/GenaratePOs?cid=' + customerid;

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
                alert("not inserted");
            }
            else {
                alert("Successfully Updated Cart.");
                var url = 'Products/Addtocartpartial?cid=' + cid;
                $('#cartrecords').load(url);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
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
                alert("not inserted");
            }
            else {
                alert("Successfully Updated Cart.");
                var url = 'Products/Addtocartpartial?cid=' + cid;
                $('#cartrecords').load(url);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}



