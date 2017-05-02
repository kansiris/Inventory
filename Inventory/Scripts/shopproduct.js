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

