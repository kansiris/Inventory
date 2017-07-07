function updateproductitems(command) {
    var product = {
        brand: $('#addnewbrand').val(),
        product_name: $('#product_name').val(),
        model: $('#addnewmodel').val(),
        category_id : $('#selectedcategoryid').val(),
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
        url: '/AddProduct/UpdateProductsItems?command=' + command, //+'&&id='+product.category_id,
        type: "POST",
        datatype: "json",
        data: { product: product },
        success: function (response) {
            if (response == 'Failed') { errormsg("Failed to Add Record!!! Try Again Later"); }
            if (response.command == 'addweight') { successmsg(product.weight + " Added SuccessFully"); $('#addnewweight').val(''); $('#weight').val(product.weight); loadproducts(response); } //Weight
            if (response.command == 'addsize') { successmsg(product.size + " Size Added SuccessFully"); $('#addnewsize').val(''); $('#size').val(product.size); loadproducts(response); } //Size
            if (response.command == 'addcolor') { successmsg(product.color + " Color Added SuccessFully"); $('#addnewcolor').val(''); $('#color').val(product.color); loadproducts(response); } //Color
            if (response.command == 'additemshape') { successmsg(product.item_shape + " Added SuccessFully"); $('#addnewitemshape').val(''); $('#item_shape').val(product.item_shape); loadproducts(response); } //Item Shape
            if (response.command == 'addcategory') { successmsg(product.category + " Category Added SuccessFully"); $('#addnewcategory').val(''); $('#category').val(product.category); loadproducts(response); } //Category
            if (response.command == 'addsubcategory') { successmsg(product.sub_category + " Sub-Category Added SuccessFully"); $('#addnewsubcategory').val(''); $('#sub_category').val(product.sub_category); loadproducts(response); } //Sub-Category
            if (response.command == 'addbrand') { successmsg(product.brand + " Added SuccessFully"); $('#addnewbrand').val(''); $('#brand').val(product.brand); loadproducts(response); } //Brand
            //if (response.command == 'addmodel') { alert(product.model + " Added SuccessFully"); } //Model
        },
        error: function (er) {
            errormsg("Something Went Wrong!!!Try Again Later");
        }
    })
}

function getsub(type, id) {
    $('#selectedcategoryid').val(id);
    $.ajax({
        url: '/AddProduct/getsub',
        type: "POST",
        datatype: "json",
        data: { 'type': type, 'id': id },
        success: function (response) {
            if (response == 'empty') {
                $('#sub_category').val('');
                $('#subcategories').empty();
                errormsg("No Sub-Caregories Available");
            }
            else {
                if (type == 'category') {
                    $('#sub_category').val('');
                    var value = "";
                    for (var i = 0; i < response.length; i++) {
                        value = value + "<div class='positions1'><i class='fa fa-trash-o pull-right' onclick=deleteitem('delsubcategory','" + response[i].subcategory_id + "') aria-hidden='true'></i>" + response[i].subcategory + "</div>";
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
            errormsg("Something Went Wrong!!!Try Again Later");
        }
    })
}

function loadproducts(response) {
    if (response.command == 'addbrand' || response.command == 'delbrand') { //Brand
    var value = "";
    for (var i = 0; i < response.records.length; i++) {
        value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delbrand','" + response.records[i].brand_id + "') aria-hidden='true'></i>" + response.records[i].brand + "</div>";
        }
    $('#allbrands').empty().append(value);
    }
    if (response.command == 'addcategory' || response.command == 'delcategory') { //Category
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delcategory','" + response.records[i].category_id + "') aria-hidden='true'></i>" + response.records[i].category + "</div>";
        }
        $('#allcategories').empty().append(value);
    }
    if (response.command == 'addsubcategory' || response.command == 'delsubcategory') { //Sub-category
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delsubcategory','" + response.records[i].subcategory_id + "') aria-hidden='true'></i>" + response.records[i].subcategory + "</div>";
        }
        $('#subcategories').empty().append(value);
    }
    if (response.command == 'addweight' || response.command == 'delweight') { //Weight
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delweight','" + response.records[i].weight_id + "') aria-hidden='true'></i>" + response.records[i].weight + "</div>";
        }
        $('#allweights').empty().append(value);
    }
    if (response.command == 'addsize' || response.command == 'delsize') { //size
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delsize','" + response.records[i].size_id + "') aria-hidden='true'></i>" + response.records[i].size + "</div>";
        }
        $('#allsizes').empty().append(value);
    }
    if (response.command == 'addcolor' || response.command == 'delcolor') { //color
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delcolor','" + response.records[i].color_id + "') aria-hidden='true'></i>" + response.records[i].color + "</div>";
        }
        $('#allcolors').empty().append(value);
    }
    if (response.command == 'additemshape' || response.command == 'delitemshape') { //item shape
        var value = "";
        for (var i = 0; i < response.records.length; i++) {
            value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' onclick=deleteitem('delitemshape','" + response.records[i].itemshape_id + "') aria-hidden='true'></i>" + response.records[i].itemshape + "</div>";
        }
        $('#allitemshapes').empty().append(value);
    }
}

function deleteitem(command, productitemid) {
    var product = {
        category_id: $('#selectedcategoryid').val(),
    }
    $.ajax({
        url: '/AddProduct/UpdateProductsItems',
        type: "POST",
        datatype: "json",
        data: { command: command, id: productitemid ,product:product},
        success: function (response) {
            if (response == 'Failed') { errormsg("Failed to Add Record!!! Try Again Later"); }
            if (response.command == 'delweight') { errormsg("Weight Removed"); $('#weight').val(''); loadproducts(response); } //Weight
            if (response.command == 'delsize') { errormsg("Size Removed"); $('#size').val(''); loadproducts(response); } //Size
            if (response.command == 'delcolor') { errormsg("Color Removed"); $('#color').val(''); loadproducts(response); } //Color
            if (response.command == 'delitemshape') { errormsg("Item Shape Removed"); $('#item_shape').val(''); loadproducts(response); } //Item Shape
            if (response.command == 'delcategory') { errormsg("Category Removed"); $('#category').val(''); loadproducts(response); } //Category
            if (response.command == 'delsubcategory') { errormsg("Sub-Category Removed"); $('#sub_category').val(''); loadproducts(response); } //Sub-Category
            if (response.command == 'delbrand') { errormsg("Brand Removed"); $('#brand').val(''); loadproducts(response); } //Brand
            //if (response.command == 'delmodel') { alert(product.model + " Added SuccessFully"); } //Model
        },
        error: function (er) {
            errormsg("Something Went Wrong!!!Try Again Later");
        }
    })
}

function removeimage(image) {
    $.ajax({
        url: '/AddProduct/removeproductimage',
        type: "POST",
        datatype: "json",
        data: { image: image },
        success: function (response) {
            if (response == "success") {
                successmsg("Image Removed");
            }
        },
        error: function (er) {
            errormsg("Something Went Wrong!!!Try Again Later");
        }
    })
}