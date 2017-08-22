function genarateInvoice() {
    var cid = document.URL.split('?')[1].split('&')[0].split('=')[1];
    var cname = location.search.split('cname=')[1];
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
                    var url = 'GenarateDeliveryNote?cid=' + cid + '&Prchaseorder_nos=' + Prchaseorder_nos;
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
    $(".overlay").show();

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
    var companyName = (document.getElementById("companyname").textContent);
    //alert(companyName);
    var invoiceNum = $("[id='invoicenum']").val();
    var paymenterms = $("[id='paymentterms']").val();
    var Comment = $("[id='comment']").val();
    if (document.getElementById("subtotal").textContent.startsWith('$')) {
        var Subtotal = ((document.getElementById("subtotal").textContent).split('$')[1]);
    }
    else {
        var Subtotal = ((document.getElementById("subtotal").textContent).split('₹')[1]);
    }

    var Vat = $("[id='vat']").val();
    var Discount = $("[id='discount']").val();
    var Grandtotal1 = ((document.getElementById("grandtotal1").textContent).split('$')[1]);
    var Prchaseordernos = $("input:checkbox:checked").map(function () { return this.value; }).toArray();
    if (invoiceNum == "") {
        $(".overlay").hide();
        errormsg("Please Enter Invoice Number");
    }
    else {
        $.ajax({
            url: '/Invoice/InsertInvoice?Prchaseorder_nos=' + Prchaseordernos,
            type: 'POST',
            data: JSON.stringify({ Invoice_no: invoiceNum, vendor_name: vendorname, customer_id: customerid, company_name: companyName, created_date: createddate, payment_date: paydate, grand_total: grandtotl, payment_terms: paymenterms, comment: Comment, sub_total: Subtotal, vat: Vat, discount: Discount }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    $(".overlay").hide();
                    errormsg("Invoice generated already.");
                }

                if (data == "sapceinnum") {
                    $(".overlay").hide();
                    errormsg("Spaces not allowed Between Invoice number");
                }

                if (data == "paymentdate") {
                    $(".overlay").hide();
                    warnmsg("Enter Payment Due Date.It Can't be Empty.");
                }

                if (data == "exists") {
                    $(".overlay").hide();
                    warnmsg("Invoice Number Already Exists.Please Enter a Unique Number");
                    $("[id='invoicenum']").val("");
                }
                if (data == "success") {
                    $(".overlay").hide();
                    successmsg("Invoice Created successfully");

                    $("[id='producttable']").css("display", "none");
                    $("[id='GenarateInvoice']").css("display", "none");
                    $("[id='back-invoice1']").css("display", "none");
                    $("[id='saveinvoice']").css("display", "none");
                    $("[id='add-vendor3']").css("display", "block");

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

    var createddate = (document.getElementById("createddatedeliv").textContent).split(':')[1];
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
            data: JSON.stringify({ Delivernote_no: delivnotenum, vendor_name: vendorname, customer_id: customerid, created_date: createddate, comment: Comment, sub_total: Subtotal }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    errormsg("Delivery Note not generated");
                }
                if (data == "exists") {
                    warnmsg("Deliverynote Number Already Exists.Please Enter a Unique Number");
                    $("[id='delivnotenum']").val("");
                }
                if (data == "success") {
                    successmsg("Delivery Note Generated successfully");
                    $("[id='delivnotenum']").val("");
                    $("[id='comment']").val("");
                    location.reload();
                }
            },
            error: function (data)
            { errormsg("Failed!!!"); }
        });
    }
}
$.fn.regexMask = function (mask) {
    $(this).keypress(function (event) {
        if (!event.charCode) return true;
        var part1 = this.value.substring(0, this.selectionStart);
        var part2 = this.value.substring(this.selectionEnd, this.value.length);
        if (!mask.test(part1 + String.fromCharCode(event.charCode) + part2))
            return false;
    });
};

function checkstatus() {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    var checkedOne = Array.prototype.slice.call(checkboxes).some(x => x.checked);
    if (checkedOne == true)
        $("#GenarateInvoice").css("display", "block");
    else
        $("#GenarateInvoice").css("display", "none");
}

//as above need to chnge
function checkstatus1(deliv) {
    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    var checkedOne = Array.prototype.slice.call(checkboxes).some(x => x.checked);
    if (checkedOne == true)
        $("#GenarateDelivNote").css("display", "block");
    else
        $("#GenarateDelivNote").css("display", "none");
}


//view invoice ponumber,
function viewinvoicedetails(cid) {
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


function errormsg(msg) {
    $("body").overhang({
        type: "error",
        message: msg
    });
}
function confirmmsg(msg) {
    $("body").overhang({
        type: "confirm",
        message: msg
    });
}



