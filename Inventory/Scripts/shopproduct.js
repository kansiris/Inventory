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

//function addtocart(id) {
//    alert(id);
//    $.ajax({
//        url: '/Products/Addtocart?id=' + id,
//        type: 'POST',
//        data: JSON.stringify({}),
//        dataType: 'json',
//        contentType: 'application/json',
//        success: function (data) {
//            alert(data);
//            if (data == "unique") {
//                alert("nodata available");
//            }
//            else {
//                alert("inserted");
//                //var url1 = 'Products/Getproductsbysubcategory';
//                //$('#allproducts').empty().load(url1);
//            }
//        },
//        error: function (data)
//        { alert("Failed!!!"); }
//    });

//}


//for genarate pos

function genaratepo() {
    //alert(id);
    $.ajax({
        url: '/Products/Genaratepo?',
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
                alert("inserted");
                var url1 = 'Products/GenaratePOs';
                $('#allproducts').empty().load(url1);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}