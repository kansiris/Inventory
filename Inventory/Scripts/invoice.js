function genarateInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    alert(cid);

    var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    alert(Prchaseorder_nos.length);

    $("#completediv").css("display", "block");
    $("#sravaniadded").css("display", "block");
    $("#sravaniadded1").css("display", "none");
    $("#fainalbuttons").css("display", "block");
    $.ajax({
        url: '/Invoice/GenarateInvoicejson?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos,
        type: 'POST',
        data: JSON.stringify({}),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("No Data Available");
            }
            else{
                successmsg("Invoice Genarated Successfully .");
                var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
                $('#sravaniadded').load(url);
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}

function genarateDelivNote() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    alert(cid);
    $("#completediv").css("display", "block");
    $("#sravaniadded1").css("display", "block");
    $("#sravaniadded").css("display", "none");
    $("#fainalbuttons").css("display", "block");
}

function genarateDelivInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    alert(cid);
    $("#completediv").css("display", "block");
    $("#sravaniadded").css("display", "block");
    $("#sravaniadded1").css("display", "block");
    $("#fainalbuttons").css("display", "block");
}



