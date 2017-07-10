function genarateInvoice() {
    var cid = document.URL.split('?')[1].split('&')[0].split('=')[1];
    var cname = location.search.split('cname=')[1];
    //alert(cname);
    var checkboxval = $("input:checkbox:checked").prop('checked');
    var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    if (checkboxval == true) {
        $("#completediv").css("display", "block");
        $("#invoicegenration").css("display", "block");
        $("#deliverynote").css("display", "none");

        $.ajax({
            url: '/Invoice/GenarateInvoicejson?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname,
            type: 'POST',
            data: JSON.stringify({}),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "success") {
                    
                    var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
                    $('#invoicegenration').load(url);
                }
                else {
                    errormsg("No Data Available");
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    } else {
        errormsg("Please select atleast one PO");
    }
}

function genarateDelivNote() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var checkboxval = $("input:checkbox:checked").prop('checked');
    if (checkboxval == true) {
        $("#completediv").css("display", "block");
        $("#deliverynote").css("display", "block");
        $("#invoicegenration").css("display", "none");
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
                    $('#deliverynote').load(url);
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    } else {
        errormsg("Please select atleast one PO");
    }
}

function genarateDelivInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var cname = location.search.split('cname=')[1];
    
    var checkboxval = $("input:checkbox:checked").prop('checked');
    if (checkboxval == true) {
        $("#completediv").css("display", "block");
        $("#invoicegenration").css("display", "block");
        $("#deliverynote").css("display", "block");
        //var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
        //    return this.value;
        //}).toArray();
        //var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
        //$('#invoicegenration').load(url);
        //var url1 = 'Invoice/GenarateDeliveryNote?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
        //$('#deliverynote').load(url1);

    } else {
        errormsg("Please select atleast one PO");
    }
}


function saveInvoice(cid) {
    var vendorname = document.getElementById("vendor_name").textContent;
    
    var customerid = cid;
    var paydate = $("[id='pay_date']").val();
    var createddate = (document.getElementById("createddate").textContent).split(':')[1];
    //var grandtotl = (document.getElementById("grandtotal").textContent);
    if (document.getElementById("grandtotal").textContent.startsWith('$')) {
        var grandtotl = ((document.getElementById("grandtotal").textContent).split('$')[1]);
    }
    else {
        var grandtotl = ((document.getElementById("grandtotal").textContent).split('₹')[1]);
    }
    var companyName = (document.getElementById("companyname").textContent).split(':')[1];
    var invoiceNum = $("[id='invoicenum']").val();
    var paymenterms = $("[id='paymentterms']").val();
    var Comment = $("[id='comment']").val();
    if (document.getElementById("subtotal").textContent.startsWith('$')) {
        var Subtotal = ((document.getElementById("subtotal").textContent).split('$')[1]);
    }
    else{
        var Subtotal = ((document.getElementById("subtotal").textContent).split('₹')[1]);
    }
    //alert(Subtotal);
    //var cgst=
  
    var Vat = $("[id='vat']").val();
    var Discount = $("[id='discount']").val(); 
    var Grandtotal1 = ((document.getElementById("grandtotal1").textContent).split('$')[1]);
    var Prchaseordernos = $("input:checkbox:checked").map(function () { return this.value;}).toArray();
        if (invoiceNum == "") {
        errormsg("Please Enter Invoice Number");
    }
    else{
    $.ajax({
        url: '/Invoice/InsertInvoice?Prchaseorder_nos=' + Prchaseordernos,
        type: 'POST',
        data: JSON.stringify({ Invoice_no: invoiceNum, vendor_name: vendorname, customer_id: customerid, company_name: companyName, created_date: createddate, payment_date: paydate, grand_total: grandtotl, payment_terms: paymenterms, comment: Comment, sub_total: Subtotal, vat: Vat, discount: Discount }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                errormsg("Invoice generated already.");
            }
            else {
                successmsg("Invoice saved successfully");
                $("[id='invoicenum']").val("");
               $("[id='date']").val("");
                $("[id='paymentterms']").val("");
                $("[id='comment']").val("");
                location.href = '/Invoice?cid=' + customerid + '&cname=' + companyName;
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }
    });
    }
}


function saveDeliverynote(cid) {
    var vendorname = document.getElementById("vendor_name").textContent;
    var customerid = cid;
       var createddate = (document.getElementById("createddate").textContent).split(':')[1];
       var grandtotl = (document.getElementById("grandTotal").textContent);
    var delivnotenum = $("[id='delivnotenum']").val();
    var Comment = $("[id='coMment']").val();
    var Subtotal = (document.getElementById("subTotal").textContent);
   
    var Prchaseordernos = $("input:checkbox:checked").map(function () { return this.value; }).toArray();
    if (delivnotenum == "") {
        errormsg("Please Enter Delivery Note Number");
    }
    else {
        $.ajax({
            url: '/Invoice/InsertDeliveryNote?Prchaseorder_nos=' + Prchaseordernos,
            type: 'POST',
            data: JSON.stringify({ Delivernote_no: delivnotenum, vendor_name: vendorname, customer_id: customerid, created_date: createddate,comment: Comment, sub_total: Subtotal}),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    errormsg("Delivery Note not generated");
                }
                else {
                    successmsg("Delivery Note saved successfully");
                    $("[id='delivnotenum']").val("");
                    $("[id='comment']").val("");
                    var cname = (document.URL.split('&&')[1]).split('=')[1];
                    location.href = '/Invoice?cid='+ customerid +'&cname=' + cname;
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
}

function checkinvoicenumber(passedvalue) {
    if (passedvalue == "") {
        errormsg("please enter Invoice number");
    }
    else {
        $.ajax({
            url: '/Invoice/CheckInvoiceNum',
            type: 'POST',
            data: JSON.stringify({ Invoice_no: passedvalue }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                }
                else {
                    existsmsg("Invoice Number Alredy Exists.Please Enter a Unique Number");
                    $("[id='invoicenum']").val("");
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
}

function checkdelivnote(passedvalue) {
    if (passedvalue == "") {
        errormsg("please enter delivery Note number");
    }
    else {
        $.ajax({
            url: '/Invoice/CheckDeliveryNoteNum',
            type: 'POST',
            data: JSON.stringify({ Delivernote_no: passedvalue }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                }
                else {
                    existsmsg("Deliverynote Number Alredy Exists.Please Enter a Unique Number");
                    $("[id='delivnotenum']").val("");
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
}
function checkstatus(invoice, deliv) {
    if (invoice == 1 && deliv == 0) {
        $("#GenarateDelivNote").css("display", "block");
        $("#GenarateInvoice").css("display", "none");
        $("#GenarateDelivInvoice").css("display", "none");
    }

    if (deliv == 1 &&  invoice == 0) {
        $("#GenarateInvoice").css("display", "block");
        $("#GenarateDelivNote").css("display", "none");
        $("#GenarateDelivInvoice").css("display", "none");
    }
    if (invoice == 0 && deliv==0) {
        $("#GenarateDelivInvoice").css("display", "block");
        $("#GenarateDelivNote").css("display", "block");
        $("#GenarateInvoice").css("display", "block");
    }

}

//view invoice
function viewinvoicedetails(ponumber, cid) {
}

function calculate(totalprice) {
    var vat = $('#vat').val();
    var discount = $('#discount').val();
    if (parseInt(vat) <= 100 && parseInt(discount) <= 100) {
        var total = (parseInt(totalprice) + (parseInt(totalprice) * (parseInt(vat) / 100)) - (parseInt(discount) / 100));
        $('#grandtotal1').text(total.toFixed(2));
        $('#grandtotal').text(total.toFixed(2));
    }
    else {
        errormsg("Please Enter < 100 %");
    }
}

