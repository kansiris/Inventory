function ajaxcalling(clickeditem) {
    //alert(clickeditem);
    $.ajax({
        url: '/Products/Getproducts?sub_category=' + clickeditem,
        type: 'POST',
        data: JSON.stringify({}),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            alert(data);
            if (data == "unique") {
                alert("nodata available");
            }
            else {
                var url1 = 'Products/Getproductsbysubcategory?sub_category=' + data;
                $('#allproducts').empty().load(url1);
                var url2 = 'Products/Addtocartpartial';
                $('#cartrecords').empty().load(url2);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}


function getproducts(value) {
    ajaxcalling(value);
}



//removing from cart
function removecart(cartid) {
    alert(cartid);
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
                var url = 'Products/Addtocartpartial';
                $('#cartrecords').empty().load(url);
                $('#allproducts').load();
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

//for genarate pos

function genaratepo() {
    var customerid = document.URL.split('=')[1];
    alert(customerid);
    location.href='/Products/GenaratePOs?cid='+customerid;
    
}