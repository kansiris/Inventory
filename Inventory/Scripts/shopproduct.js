$(document).ready(function (e) {
    
    var tableprice, divprice, tabletotal, divtotal;
    $('.spinner .btn:first-of-type').click(function () {
        var plus, v;
        v = parseInt($(this).parent("div").prev("input[type='text']").val());
        //alert(v);
        if (v > 0) {
            plus = v + 1;
            $(this).parent("div").prev("input[type='text']").val(plus);
            tableprice = parseInt($(this).parents(".quantity-changes").nextAll(".product-price").children("span").text());
            divprice = parseInt($(this).parents(".rate-add").children(".product-price").children("span").text());

            tabletotal = plus * tableprice;
            divtotal = plus * divprice;
            $(this).parents(".quantity-changes").nextAll(".product-price-total").children("span").remove();
            $(this).parents(".quantity-changes").nextAll(".product-price-total").append("<span>" + tabletotal + "</span>");
            $(this).parents(".rate-add").children(".product-price-total").children("span").remove();
            $(this).parents(".rate-add").children(".product-price-total").append("<span>" + divtotal + "</span>");
        }
        return vals();
    });
    $('.spinner .btn:last-of-type').click(function () {
        var minus, v;
        v = parseInt($(this).parent("div").prev("input[type='text']").val());

        if (v > 1) {
            minus = v - 1;
            $(this).parent("div").prev("input[type='text']").val(minus);
            tableprice = parseInt($(this).parents(".quantity-changes").nextAll(".product-price").children("span").text());;
            divprice = parseInt($(this).parents(".rate-add").children(".product-price").children("span").text());
            tabletotal = minus * tableprice;
            divtotal = minus * divprice; $(this).parents(".quantity-changes").nextAll(".product-price-total").children("span").remove();
            $(this).parents(".quantity-changes").nextAll(".product-price-total").append("<span>" + tabletotal + "</span>");
            $(this).parents(".rate-add").children(".product-price-total").children("span").remove();
            $(this).parents(".rate-add").children(".product-price-total").append("<span>" + divtotal + "</span>");
        }
        return vals();
    });

    $("select").on('change', function () {
        var v = $(this).children("option:selected").text().split("₹")[1];
        $(this).parents(".product-variation").nextAll(".rate-add").children(".product-price").empty().append("Price ₹ " + "<span>" + v + "</span>");
        $(this).parents(".product-variation").nextAll(".rate-add").children(".product-price-total").empty().append("Total ₹ " + "<span>" + v + "</span>");
        $(this).parents(".product-variation").nextAll(".product-price").empty().append("₹ " + "<span>" + v + "</span>");
        $(this).parents(".product-variation").nextAll(".product-price-total").empty().append("₹ " + "<span>" + v + "</span>");
        $(this).closest('div.row').find("div.qty").children("div.spinner").children("input").val('1');

    });
});

function vals() {
  
    var total, totalquantity = 0, productprice, carttotal = 0;
    $(".added-products .spinner input[type='text']").each(function () {
        total = parseInt($(this).val());
        totalquantity = total + totalquantity;
    });
    $(".added-products .product-price-total span").each(function () {
        productprice = parseInt($(this).text());
        carttotal = productprice + carttotal;
    });
    $(".cart-total span").remove();
    $(".cart-total").append("<span>" + carttotal + "</span>");
    $(".cart-icn span").remove();
    $(".cart-icn").append("<span>" + totalquantity + "</span>");
}

$(".add-to-cart-table > .btn, .add-to-cart > .btn").click(function () {
    return vals();
    
});

$(".remove").click(function () {
    $(this).parents("div.well,tr").remove();
    return vals();

});

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
               //location.reload();
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
    //alert(cartid);
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
                location.reload();
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

//for genarate pos

function genaratepo() {
    var customerid = document.URL.split('=')[1];
    //alert(customerid);
    location.href='/Products/GenaratePOs?cid='+customerid;
    
}
//updatecart
function updatecart(cartid, quantity, costprice) {
    alert(cartid);
    alert(quantity);
    alert(costprice);
    newquant = quantity + 1;
    newamunt = costprice * newquant;
    alert("new" + newquant);
    alert("new" + newamunt);

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
                var url = 'Products/Addtocartpartial';
                $('#cartrecords').empty().load(url);
                location.reload();
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}
function updatecart1(cartid, quantity, costprice) {
    alert(cartid);
    alert(quantity);
    alert(costprice);
    newquant = quantity - 1;
    newamunt = costprice * newquant;
    alert("new" + newquant);
    alert("new" + newamunt);

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
                var url = 'Products/Addtocartpartial';
                $('#cartrecords').empty().load(url);
                location.reload();
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

