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
    //alert(document.URL.split('?')[1].split('cid=')[1].split('&')[0]);
    $('#Customer_comapnyId').val(document.URL.split('?')[1].split('cid=')[1].split('&')[0]);
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




function savepayments(cid, cname) {
    alert(cid);
    var sum = 0;
    var ponum = $("input:checkbox:checked").map(function () {
        return this.value.split(',')[1];
    }).toArray();
    var paymentsddate = (document.getElementById("payment_date").textContent).split(';')[1].trimRight();
    var invoicedtotal = (document.getElementById("invoicedtotal").textContent).split('₹')[1];
    if (document.getElementById('radio-cheque').checked) {
        var chequedate = $("#chequedate").val();
        var bankname = $("#bankname").val();
        var chequeno = $("#chequeno").val();
    }
    else {
        var chequedate = "";
        var bankname = "";
        var chequeno = "";
    }
    if (document.getElementById('radio-credit').checked) {
        var creditdate = $("#creditdate").val();
        var creditcardholdername = $("#creditcardholdername").val();
        var creditcardlast4digits = $("#creditcardlast4digits").val();
    }
    else {
        var creditdate = "";
        var creditcardholdername = "";
        var creditcardlast4digits = "";
    }

    if (document.getElementById('radio-banktransfer').checked) {
        var banktransferdate = $("#banktransferdate").val();
        var banknametrans = $("#banknametrans").val();
        var transactionid = $("#transactionid").val();
        var ifsccode = $("#ifsccode").val();
        var branchname = $("#branchname").val();
    }
    else {
        var banktransferdate = "";
        var banknametrans = "";
        var transactionid = "";
        var ifsccode = "";
        var branchname = "";
    }
    if (document.getElementById('radio-cash').checked) {
        var cashdate = $("#cashdate").val();
        var holdername = $("#holdername").val();
    }
    else {
        var cashdate = "";
        var holdername = "";
    }
    if (document.getElementById('radio-wallet').checked) {
        var walletdate = $("#walletdate").val();
        var mobilenumber = $("#mobilenumber").val();
    }
    else {
        var walletdate = "";
        var mobilenumber = "";
    }
    var invamt = $("#invoiced_amount").val();
    var previous = $("#opening_balance").val();
    var recevdamnt = $("#Received_amount").val();
    var currntamt = $("#current_balance").val();
    var comment = $("#comment").val();
    var payments = {
        payments_date:paymentsddate,
        cheque_date:chequedate, 
        cheque_bankname:bankname,
        cheque_num:chequeno, 
        creditORdebitcard_date:creditdate, 
        card_holder_name:creditcardholdername, 
        card_last4digits:creditcardlast4digits, 
        bank_taransfer_date:banktransferdate,
        bank_transfer_name:banknametrans,
        bank_transaction_id:transactionid, 
        cash_date:cashdate, 
        cash_card_holdername:holdername, 
        wallet_date:walletdate, 
        wallet_number:mobilenumber,
        invoiced_amount:invamt, 
        Received_amount:recevdamnt, 
        opening_balance:previous, 
        current_balance:currntamt, 
        bank_transfer_IFSCcode:ifsccode,
        bank_transfer_branchname: branchname,
        Customer_company_name:cname,
         Customer_comapnyId:cid,
        remarks:comment
    }
    //alert(payments.payments_date);
$.ajax({
    url: '/Payments/InsertPayments?Prchaseorder_no=' + ponum,
    type: 'POST',
    data: JSON.stringify({ payments: payments}),
    dataType: 'json',
    contentType: 'application/json',
    success: function (data) {
        if (data == "unique") {
            errormsg("Payments already completed.");
        }
        else {
            successmsg("Payments saved successfully");
            //location.reload();
        }
    },
    error: function (data)
    { errormsg("Failed!!!"); }
});
}

