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

//for mutton
function getmutton() {
    var clickeditem1 = $('#mutton').text();
    if (clickeditem1 == 'Mutton') {
        ajaxcalling(clickeditem1);
        return true;
    }
}
// for Seafood
function getseefood() {
    var clickeditem = $('#seafood').text();
    if (clickeditem == 'Seafood') {
        ajaxcalling(clickeditem);
        return true;
    }
}

//for cutsandsprouts
function getcutsandsprouts() {
    var clickeditem = $('#cutsandsprouts').text();
    if (clickeditem == 'Cuts and Sprouts') {
        ajaxcalling(clickeditem);
        return true;
    }
}
//for flowers
function getflowers() {
    var clickeditem = $('#flowers').text();
    if (clickeditem == 'Flowers') {
        ajaxcalling(clickeditem);
        return true;
    }
}
//for fruits
function getfruits() {
    var clickeditem = $('#fruits').text();
    if (clickeditem == 'Fresh Fruits') {
        ajaxcalling(clickeditem);
        return true;
    }
}

//for fruits
function getherbs() {
    var clickeditem = $('#herbs').text();
    if (clickeditem == 'fresh Herbs and Seasonings') {
        ajaxcalling(clickeditem);
        return true;
    }
}

