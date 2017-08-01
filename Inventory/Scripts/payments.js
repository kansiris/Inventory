function addtotal() {
    var sum = 0;
    var totalamnt = $("input:checkbox:checked").map(function () {
        return this.value.split(',')[0];
    }).toArray();
    var poid = $("input:checkbox:checked").map(function () { return this.value.split(',')[1]; }).toArray();
    var sum = 0;
    var lent = totalamnt.length;
    for (var i = 0; i < lent; i++) {
        sum = sum + parseInt(totalamnt[i]);
    }
    $('#invoiced_amount').val(sum);
    $('#poid').val(poid);
    $('#Customer_comapnyId').val(location.search.split('cid=')[1]);
    //$('#Customer_comapnyId').val(document.URL.split('?')[1].split('&&')[0].split('=')[1]);
    $('#Customer_company_name').val(location.search.split('cname=')[1]);
    var paymentsddate = (document.getElementById("payment_date").textContent).split(';')[1].trimRight();
    $('#payments_date').val(paymentsddate);
}

function calculateamnt() {
    var invamt = $("#invoiced_amount").val();
    var previous = $("#opening_balance").val();
    var recevdamnt = $("#Received_amount").val();
    if (recevdamnt == null || recevdamnt == "") {
        warnmsg("Enter Some Amount.It can't be empaty..");
    }

    if (parseInt(recevdamnt) > parseInt(invamt)) {
        warnmsg("Enter less than or Equal Of Invoiced Amount...");
        $('#Received_amount').val("");
    //    $("#current_balance").val((parseInt(previous) - $('#Received_amount').val()));
    }
    //else{
    $("#current_balance").val((parseInt(previous) - recevdamnt));
    //}
}



function errormsg(msg) {
    $("body").overhang({
        type: "error",
        message: msg,
        closeConfirm: false
    });
}

function successmsg(msg) {
    $("body").overhang({
        type: "success",
        message: msg,
        closeConfirm: false
    });
}

function existsmsg(msg) {
    $("body").overhang({
        type: "exists",
        message: msg,
        duration: 3,
        closeConfirm: false
    });
}

function warnmsg(msg) {
    $("body").overhang({
        type: "warn",
        message: msg,
        duration: 3
    });
}

function confirmmsg(msg) {
    $("body").overhang({
        type: "confirm",
        message: "msg"
    });
}
