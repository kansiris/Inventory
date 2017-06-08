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

    location.href = '/Invoice/GenarateInvoice?cid=' + cid +'&Prchaseorder_nos=' + Prchaseorder_nos;
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



