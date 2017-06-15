function genarateInvoice() {
    var cid = document.URL.split('?')[1].split('&&')[0].split('=')[1];
    var cname = document.URL.split('&&')[1].split('=')[1];
    var checkboxval = $("input:checkbox:checked").prop('checked');
    //var checkboxval = $("#availpocheck").prop('checked');
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
                if (data == "unique") {
                    errormsg("No Data Available");
                }
                else {
                    var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
                    $('#invoicegenration').load(url);
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
    //var checkboxval = $("#availpocheck").prop('checked');
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
    var cname = document.URL.split('&&')[1].split('=')[1];
    
    var checkboxval = $("input:checkbox:checked").prop('checked');
    //alert(checkboxval);
    //var checkboxval = $("#availpocheck").prop('checked');
    if (checkboxval == true) {
        $("#completediv").css("display", "block");
        $("#invoicegenration").css("display", "block");
        $("#deliverynote").css("display", "block");
        var Prchaseorder_nos = $("input:checkbox:checked").map(function () {
            return this.value;
        }).toArray();
        var url = 'Invoice/GenarateInvoice?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos + '&customer_name=' + cname;
        $('#invoicegenration').load(url);
        var url1 = 'Invoice/GenarateDeliveryNote?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
        $('#deliverynote').load(url1);

    } else {
        errormsg("Please select atleast one PO");
    }
}


function saveInvoice(cid) {
    var vendorname = document.getElementById("vendor_name").textContent;
    
    var customerid = cid;
    var paydate = $("[id='date']").val();
    var createddate = (document.getElementById("createddate").textContent).split(':')[1];
    var grandtotl = (document.getElementById("grandtotal").textContent);
    var companyName = (document.getElementById("companyname").textContent).split(':')[1];
    var invoiceNum = $("[id='invoicenum']").val();
    var paymenterms = $("[id='paymentterms']").val();
    var Comment = $("[id='comment']").val();
    var Subtotal = (document.getElementById("subtotal").textContent);
    var Vat = (document.getElementById("vat").textContent).split('%')[0];
    var Discount = (document.getElementById("discount").textContent).split('%')[0];
    var Grandtotal1 = (document.getElementById("grandtotal1").textContent);
    var Prchaseordernos = $("input:checkbox:checked").map(function () { return this.value;}).toArray();
    //alert(Prchaseordernos);
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
                errormsg("Please Enter Invoice Number");

            }
            else {
                successmsg("Invoice saved successfully");
                $("[id='invoicenum']").val("");
               
               $("[id='date']").val("");
                //$("[id='vendor_name']").val("");
                $("[id='paymentterms']").val("");
                $("[id='comment']").val("");
                
            }
        },
        error: function (data)
        { errormsg("Failed!!!"); }

    });
    }
}


function checkinvoicenumber(passedvalue) {
    //alert(passedvalue);
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



