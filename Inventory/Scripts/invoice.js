function genarateInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var cname = document.URL.split('&&')[1].split('=')[1];

    var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();

    $("#completediv").css("display", "block");
    $("#sravaniadded").css("display", "block");
    $("#sravaniadded1").css("display", "none");

    $.ajax({
        url: '/Invoice/GenarateInvoicejson?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname,
        type: 'POST',
        data: JSON.stringify({}),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else {
                var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
                $('#sravaniadded').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}

function genarateDelivNote() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];

    $("#completediv").css("display", "block");
    $("#sravaniadded1").css("display", "block");
    $("#sravaniadded").css("display", "none");

    var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    ;

    $.ajax({
        url: '/Invoice/GenarateDelivjson?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos,
        type: 'POST',
        data: JSON.stringify({}),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else {
                var url = 'Invoice/GenarateDeliveryNote?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
                $('#sravaniadded1').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}

function genarateDelivInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var cname = document.URL.split('&&')[1].split('=')[1];
    $("#completediv").css("display", "block");
    $("#sravaniadded").css("display", "block");
    $("#sravaniadded1").css("display", "block");
    var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();

    var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
    $('#sravaniadded').load(url);
    var url1 = 'Invoice/GenarateDeliveryNote?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
    $('#sravaniadded1').load(url1);
}



