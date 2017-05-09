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
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}


function getproducts(value) {
    ajaxcalling(value);
}
//function selctprice(paseedvalue) {
//    alert(paseedvalue);

//    var totalstring = paseedvalue.split("-");
//    var measurement = totalstring[0];
//    alert(measurement + "f");
//    var totalprice = totalstring[1];
//    var totalprice1 = totalprice.split("₹");
//    alert(totalprice1[1] + "g");
//    jQuery("#price").text("message");
//    //$("#product-price span").text(totalprice1[1]);
//    //document.getElementById("price").textContent = totalprice1[1];
//    ////document.getElementById("price").innerHTML = totalprice1[1];
//    //var span = document.getElementById('price');
//    //document.getElementById("price").textContent = totalprice1[1];
//    //alert(document.getElementById("price").textContent = totalprice1[1]);
//    //while (span.firstChild) {
//    //    span.removeChild(span.firstChild);
//    //}
//    //document.getElementById("price").innerHTML = totalprice1[1];
//}
