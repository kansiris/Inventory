function addtotal() {
    //    alert(totalval);
    var sum = 0;
    var totalamnt = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    var sum = 0;
    var lent = totalamnt.length;
    for (var i = 0; i < lent; i++) {
        sum = sum + parseInt(totalamnt[i]);
    }
    $('#invoicedamt').val(sum);
}

function calculateamnt() {
    var invamt = $("#invoicedamt").val();
    var previous = $("#previousamt").val();
    var recevdamnt = $("#paidamt").val();
    $("#currntamt").val((parseInt(previous) - recevdamnt));
}

function savepayments(cid,cname) {
    var paymentsddate = (document.getElementById("paymentsddate").textContent).split(';')[1];
    var invoicedtotal = (document.getElementById("invoicedtotal").textContent).split('₹')[1];
    if (document.getElementById('radio-cheque').checked) {
         var chequedate = $("#chequedate").val();
        alert(chequedate);
        var bankname = $("#bankname").val();
        alert(bankname);
        var chequeno = $("#chequeno").val();
        alert(chequeno);
    } 
    if (document.getElementById('radio-credit').checked) {
        var creditdate = $("#creditdate").val();
        alert(creditdate);
        var creditcardholdername = $("#creditcardholdername").val();
        alert(creditcardholdername);
        var creditcardlast4digits = $("#creditcardlast4digits").val();
        alert(creditcardlast4digits);
    } 
    
    if (document.getElementById('radio-banktransfer').checked) {
        var banktransferdate = $("#banktransferdate").val();
        alert(banktransferdate);
        var banknametrans = $("#banknametrans").val();
        alert(banknametrans);
        var transactionid = $("#transactionid").val();
        alert(transactionid);
        var ifsccode = $("#ifsccode").val();
        alert(ifsccode);
        var branchname = $("#branchname").val();
        alert(branchname);
    }
    if (document.getElementById('radio-cash').checked) {
        var cashdate = $("#cashdate").val();
        alert(cashdate);
        var holdername = $("#holdername").val();
        alert(holdername);
    }
    if (document.getElementById('radio-wallet').checked) {
        var walletdate = $("#walletdate").val();
        alert(walletdate);
        var mobilenumber = $("#mobilenumber").val();
        alert(mobilenumber);
    } 
    var invamt = $("#invoicedamt").val();
    var previous = $("#previousamt").val();
    var recevdamnt = $("#paidamt").val();
    var currntamt = $("#currntamt").val();
    var comment = $("#comment").val();
    alert(invamt);
    alert(previous);
    alert(recevdamnt);
    alert(currntamt);

    $.ajax({
        url: '/Payments/InsertPayments',
        type: 'POST',
        data: JSON.stringify({
            payments_date: paymentsddate, cheque_date: chequedate, cheque_bankname: bankname, cheque_num: chequeno, creditORdebitcard_date: creditdate, card_holder_name: creditcardholdername, card_last4digits: creditcardlast4digits, bank_taransfer_date: banktransferdate, bank_transfer_name: banknametrans, bank_transaction_id: transactionid,
            cash_date: cashdate, cash_card_holdername: holdername, wallet_date: walletdate, wallet_number: mobilenumber, invoiced_amount: invamt, Received_amount: recevdamnt, opening_balance: previous, current_balance: currntamt, bank_transfer_IFSCcode: ifsccode, bank_transfer_branchname: branchname
            , Customer_comapnyId: cid, Customer_company_name: cname, remarks: comment
        }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("Payments already completed.");
            }
            else {
                successmsg("Payments saved successfully");
                $("#invoicedamt").val("");
                $("#previousamt").val("");
                $("#paidamt").val("");
                $("#currntamt").val("");
                $("[id='comment']").val("");
                $("#chequedate").val("");
                $("#bankname").val("");
                $("#chequeno").val("");
                $("#creditdate").val("");
                $("#creditcardholdername").val("");
                $("#creditcardlast4digits").val("");
                $("#banktransferdate").val("");
                $("#banknametrans").val("");
                $("#transactionid").val("");
                $("#ifsccode").val("");
                $("#branchname").val("");
                $("#cashdate").val("");
                $("#holdername").val("");
                $("#walletdate").val("");
                $("#mobilenumber").val("");
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
}

