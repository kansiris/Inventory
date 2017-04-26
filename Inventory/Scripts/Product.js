function updateproductitems(command) {
    var product = {
        brand: $('#addnewbrand').val(),
        product_name: $('#product_name').val(),
        model: $('#addnewmodel').val(),
        category: $('#addnewcategory').val(),
        sub_category: $('#addnewsubcategory').val(),
        cost_price: $('#cost_price').val(),
        selling_price: $('#selling_price').val(),
        tax: $('#tax').val(),
        discount: $('#discount').val(),
        shipping_price: $('#shipping_price').val(),
        total_price: $('#total_price').val(),
        Measurement: $('#Measurement').val(),
        weight: $('#addnewweight').val(),
        size: $('#addnewsize').val(),
        color: $('#addnewcolor').val(),
        item_shape: $('#addnewitemshape').val(),
        product_consumable: $('#product_consumable').val(),
        product_type: $('#product_type').val(),
        product_perishability: $('#product_perishability').val(),
        product_expirydate: $('#product_expirydate').val(),
        product_description: $('#product_description').val(),
        product_tags: $('#myTags').val(),
    }
    $.ajax({
        url: '/AddProduct/UpdateProductsItems?command=' + command,
        type: "POST",
        datatype: "json",
        data: { product: product },
        success: function (response) {
            if (response == 'Failed') { alert("Failed to Add Record!!! Try Again Later"); }
            if (response.command == 'addweight') { alert(product.weight + " Added SuccessFully"); $('#addnewweight').val(''); $('#weight').val(''); loadproducts(response); } //Weight
            if (response.command == 'addsize') { alert(product.size + " Size Added SuccessFully"); $('#addnewsize').val(''); $('#size').val(''); loadproducts(response); } //Size
            if (response.command == 'addcolor') { alert(product.color + " Color Added SuccessFully"); $('#addnewcolor').val(''); $('#color').val(''); loadproducts(response); } //Color
            if (response.command == 'additemshape') { alert(product.item_shape + " Added SuccessFully"); $('#addnewitemshape').val(''); $('#item_shape').val(''); loadproducts(response); } //Item Shape
            if (response.command == 'addcategory') { alert(product.category + " Category Added SuccessFully"); $('#addnewcategory').val(''); $('#category').val(''); loadproducts(response); } //Category
            if (response.command == 'addsubcategory') { alert(product.sub_category + " Sub-Category Added SuccessFully"); $('#addnewsubcategory').val(''); $('#sub_category').val(''); loadproducts(response); } //Sub-Category
            if (response.command == 'addbrand') { alert(product.brand + " Added SuccessFully"); $('#addnewbrand').val(''); $('#brand').val(''); loadproducts(response); } //Brand
            if (response.command == 'addmodel') { alert(product.model + " Added SuccessFully"); } //Model
        },
        error: function (er) {
            alert("error");
        }
    })
}

function getsub(type, id) {
    alert(id);
    $('#selectedcategoryid').val(id);
    $.ajax({
        url: '/AddProduct/getsub',
        type: "POST",
        datatype: "json",
        data: { 'type': type, 'id': id },
        success: function (response) {
            if (response == 'empty') {
                //alert("No Records Available");
            }
            else {
                if (type == 'category') {
                    var value = "";
                    for (var i = 0; i < response.length; i++) {
                        value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response[i].subcategory + "</div>";
                    }
                    $('#subcategories').empty().append(value);
                }
                //if (type == 'brand') {
                //    var value = "";
                //    for (var i = 0; i < response.length; i++) {
                //        value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response[i].brandmodel + "</div>";
                //    }
                //    $('#subbrands').empty().append(value);
                //}
            }
        },
        error: function (er) {
            alert("Something Went Wrong!!!Try Again Later");
        }
    })
}

function loadproducts(response) {
    if (response.command == 'addbrand') { //Brand
    var value = "";
    for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].brand + "</div>";
        }
    $('#allbrands').empty().append(value);
    }
    if (response.command == 'addcategory') { //Category
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].category + "</div>";
        }
        $('#allcategories').empty().append(value);
    }
    if (response.command == 'addsubcategory') { //Sub-category
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].sub_category + "</div>";
        }
        $('#subcategories').empty().append(value);
    }
    if (response.command == 'addweight') { //Weight
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].weight + "</div>";
        }
        $('#allweights').empty().append(value);
    }
    if (response.command == 'addsize') { //size
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].size + "</div>";
        }
        $('#allsizes').empty().append(value);
    }
    if (response.command == 'addcolor') { //color
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].color + "</div>";
        }
        $('#allcolors').empty().append(value);
    }
    if (response.command == 'additemshape') { //item shape
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + response.records[i].itemshape + "</div>";
        }
        $('#allitemshapes').empty().append(value);
    }
}



//sravani added code start //



//sravani added code end //