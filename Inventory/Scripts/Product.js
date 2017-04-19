var product = {
    product_name : $('#product_name').val(),
    brand: $('#brand').val(),
    model: $('#model').val(),
    category: $('#category').val(),
    sub_category: $('#sub_category').val(),
    cost_price: $('#cost_price').val(),
    selling_price: $('#selling_price').val(),
    tax: $('#tax').val(),
    discount: $('#discount').val(),
    shipping_price: $('#shipping_price').val(),
    total_price: $('#total_price').val(),
    Measurement: $('#Measurement').val(),
    weight: $('#weight').val(),
    size: $('#size').val(),
    color: $('#color').val(),
    item_shape: $('#item_shape').val(),
    product_consumable: $('#product_consumable').val(),
    product_type: $('#product_type').val(),
    product_perishability: $('#product_perishability').val(),
    product_expirydate: $('#product_expirydate').val(),
    product_description: $('#product_description').val(),
    product_tags: $('#myTags').val(),
}
function updateproduct(val) {
    $.ajax({
        url: '/AddProduct/UpdateProducts?command=' + val,
        type: "POST",
        datatype: "json",
        data: { product: product},
        success: function (response) {
            if (response == 'success') {
                alert("");
                location.href = '/AllProducts';
            }
            else {
                alert("Failed To Add Product");
            }
        },
        error: function (er) {
            alert("error");
        }
    })
}