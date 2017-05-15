//<!---------- Display Vendor Information and reset all forms --------------->
$("#add-customer").click(function () {
    $("#customer-information").css("display", "block");
    $("#additonal").css("display", "none");
    $("#contacttable").css("display", "none");
    $('#mySubmit').css("display", "block");
    $("#customer-information input").val("");
    $("#Vendor_Id").val("");
    $("#cuscompanypic").attr("src", "/images/user.png");
    $("#customer-information1 input, .cd-tabs input, .cd-tabs textarea, .cd-tabs dropdown").val("");
    $("#bill_country").val("");
    $("#ship_country").val("");
    $("#customer-information1").css("display", "none");
    $('#mySubmit').val("Save").text("Save");
    $('#mySubmit1').val("saveaddress").text("Save Address");
    $('#bankid').val("savebankdetails").text("Save Bank Details");
    $('#notebutton').val("Save Notes").text("Save Notes");
    $('#cuscontactbutton').val("savecontact").text("Save Contact");
    $('#myform input[type=text]').attr("disabled", false);
    $('#myform textarea').attr("disabled", false);
    $('#myform input[type=file]').attr("disabled", false);
    $('#mySubmit').show();
    $('#mySubmit1').show();
    $('#cuscontactbutton').show();
    $('#taxid').show();
    $('#bankid').show();
    $('#notebutton').show();
    $("#bill_country").attr("disabled", false);
    $("#ship_country").attr("disabled", false);
    $("#customer-information-cancel").show();
    $("#customer-information1-cancel").show();
    $("#customer-information2-cancel").show();
    $("#customer-information3-cancel").show();
    $("#customer-information4-cancel").show();
    $("#forclose").css("display", "none");
    $("#uploadtext").css("display", "block");
    $("#uploadcontact").css("display", "block");
    $("#cuscontactpic").attr("src", "/images/user.png");
});
$("#customer-information-cancel").click(function () {
    $("#customer-information input").val("");
    $("#customer-information1 input, .cd-tabs input, .cd-tabs textarea").val("");
    $('#mySubmit').val("Save").text("Save");
    $("#customer-information").css("display", "none");
    $("#customer-information1").css("display", "none");
});

function forCancel() {
    $("#additon").css("display", "block");
    $("#customer-information1 input, .cd-tabs input, .cd-tabs textarea").val("");
    $(".cd-tabs").css("display", "none");
    $("#cuscompanypic").attr("src", "/images/user.png");
    $("#cuscontactpic").attr("src", "/images/user.png");
    
    //$("#vendor-information1 input").val("");
}
$("#customer-information1-cancel").click(function () {
    forCancel();
});
$("#customer-information2-cancel").click(function () {
    forCancel();
});
$("#customer-information3-cancel").click(function () {
    forCancel();
});
$("#customer-information4-cancel").click(function () {
    forCancel();
});
$("#save-reset").click(function () {
    $(".contactperson input").val("");
});

//<!------------ List / Grid Views and reload page -------------->
var usedNames = {};
$("select[name='bill_country'] > option").each(function () {
    if (usedNames[this.text]) {
        $(this).remove();
    } else {
        usedNames[this.text] = this.value;
    }
});

var usedNames1 = {};
$("select[name='ship_country'] > option").each(function () {
    if (usedNames1[this.text]) {
        $(this).remove();
    } else {
        usedNames1[this.text] = this.value;
    }
});

$("#grid-view").click(function (e) {
    $("#customertable").css("display", "none");
    $("#customertable1").css("display", "block");
});

$("#list-view").click(function (e) {
    $("#customertable").css("display", "block");
    $("#customertable1").css("display", "none");
    var url = 'Customer/CustomerCompany';
    $('#cuscompanyrecords').empty().load(url, function () { Pagination(); });
    //location.reload();
});

$("#refresh").click(function (e) {
    var url = 'Customer/CustomerCompany';
    $('#cuscompanyrecords').empty().load(url, function () { Pagination(); });
});

//<!------------ List / Grid Views and reload page -------------->

//<!---------- Display Vendor Information and reset all forms --------------->

//<!-- Clone Shipping Address -->

$("input[type='checkbox']").change(function () {
    if ($("input[type='checkbox']").is(':checked')) {
        $("[id='ship_street']").val($("[id='bill_street']").val());
        $("[id='ship_city']").val($("[id='bill_city']").val());
        $("[id='ship_state']").val($("[id='bill_state']").val());
        $("[id='ship_postalcode']").val($("[id='bill_postalcode']").val());
        $("[id='ship_country']").val($("[id='bill_country']").val());
    }
    else {
        $("[id='ship_street']").val("");
        $("[id='ship_city']").val("");
        $("[id='ship_state']").val("");
        $("[id='ship_postalcode']").val("");
        $("[id='ship_country']").val("");
    }
});

//<!-- Clone Shipping Address -->
//<!------ Random Colors ------>

$(document).ready(function (e) {
    $(".top-button").each(function () {
        var colors = ["#f4511e", "#7e57c2", "#455a64", "#512da8", "#c2185b", "#5c6bc0", "#0288d1", "#f4511e", "#ef6c00", "#0097a7", "#5c6bc0", "#5d4037"];
        var len = colors.length;
        var rand = Math.floor(Math.random() * len);
        $(this).css("background", colors[rand]);

    });

    //  <!----- Table Pagination ---->
    Pagination();
    $("#customertable1").css("display", "none");
    $("#customer-information").css("display", "none");
    
    // <!----- Table Pagination ---->

    // $("#vendor-information1").hide();
    $(".cd-tabs").css("display", "none");

    $("#additonal").click(function () {
        $("#customer-information1").css("display", "block");
        $(".cd-tabs").css("display", "block");
    });
});
//$('#customertable tbody tr').css({ "display": "table", "width": "100%" });
function Pagination() {

    $('#customertable1').after('<div id="nav"></div>');
    var rowsShown = 3;
    var rowsTotal = $('#customertable tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#nav').append('<a href="#" class="btn btn-success" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#customertable tbody tr').hide();
    $('#customertable tbody tr').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {
        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#customertable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);
    });



    $('#customertable1 > div').hide();
    $('#customertable1 > div').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {

        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#customertable1 > div').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}

//<!------ Random Colors ------>
//<!------ Cloning contact person ------>

$("#add").click(function (e) {
    var len = $(".contactperson #clone-content").length;
    if (len < 2) {
        var v = $("li.contactperson > div.row:first-of-type > div:last-of-type");
        $("#clone-content").clone().appendTo(v);
    }
});

//<!------ Cloning contact person ------>
//<!------ Contact Person Pop Up Job Position ------->
$(".display-positions .position > i.fa-times").click(function () {
    $(this).parents(".add-position").css("display", "none");
    $(this).parents(".display-positions").css("display", "none");
    $(this).parents(".display-positions").nextAll(".add-position").css("display", "none");
    
});

function textval() {
    $(".display-positions .positions1").click(function () {
        var c = $(this).text();
        $("input.selected-position").val(c);
        $(".display-positions").css("display", "none");
        $(this).parents(".add-position").css("display", "none");

    });
}

$(".positions1 > i.fa-trash-o").click(function () {
    $(this).parent(".positions1").remove();
});

$(".selected-position").click(function () {
    $(".display-positions").css("display", "block");
    return textval();
});

$(".display-positions .position").click(function () {
    $(".add-position").css("display", "block");

    $(".add-position .add-button").click(function () {

        return textval();
    });

    $(".close-button").click(function () {
        $(this).parents(".add-position").css("display", "none");
    });

});


$("#Adhar_Number").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        $("#errmsg1").html("Enter Digits Only").show().fadeOut("slow");
        return false;
    }
});
$("#Bank_Acc_Number").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        $("#errmsg2").html("Enter Numbers only").show().fadeOut("slow");
        return false;
    }
});
$("#Mobile_No").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        $("#errmsg").html("Enter Digits Only").show().fadeOut("slow");
        return false;
    }
});
//<!------ Contact Person Pop Up Job Position ------->
//Deleting particular company based on id

function deleteRecord(id) {
    //alert(id);
    var retVal = confirm("Do you want to delete record...!");
    if (retVal == true) {
        $.ajax({
            url: '/Customer/deletecusRecord',
            type: 'POST',
            data: JSON.stringify({ cus_company_Id: id }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    alert("sai");
                }
                else {
                    
                    var url = 'Customer/CustomerCompany';
                    $('#cuscompanyrecords').empty().load(url, function () { Pagination(); });
                    alert("Company Deleted Successfully");
                    $('#customer-information').css('display', 'none');
                    $('#additonal').css('display', 'none');
                    $(".cd-tabs").css('display', 'none');

                }
            },
            error: function (data)
            { alert("Failed!!!"); }
        });

        return true;
    }

    else {
        return false;
    }
}
//Assigning values to inputs
function editFunction(array) {
    $('#cus_company_Id').val(array.cus_company_Id);
    $('#cus_company_name').val(array.cus_company_name);
    $('#cus_email').val(array.cus_email);
    //$('#Bank_Acc_Number').val(array.Bank_Acc_Number);
    //$('#Bank_Name').val(array.Bank_Name);
    //$('#Bank_Branch').val(array.Bank_Branch);
    //$('#IFSC_No').val(array.IFSC_No);
    $('#cus_Note').val(array.cus_Note);
    if (array.cus_logo != "/images/user.png" && array.cus_logo != null && array.cus_logo != "") {
        $('#cuscompanypic').attr('src', 'data:image/;base64,' + array.cus_logo);
    } else
        $('#cuscompanypic').attr('src', array.cus_logo);
    $('#Customer_Id').val(array.Customer_Id);
    $('#bill_city').val(array.bill_city);
    $('#bill_country').val(array.bill_country);
    $('#bill_state').val(array.bill_state);
    $('#bill_street').val(array.bill_street);
    $('#bill_postalcode').val(array.bill_postalcode);
    $('#ship_city').val(array.ship_city);
    $('#ship_country').val(array.ship_country);
    $('#ship_state').val(array.ship_state);
    $('#ship_street').val(array.ship_street);
    $('#ship_postalcode').val(array.ship_postalcode);
}

//Get Particular customer Record
function getEditDetails(id) {
    $('#mySubmit').val("update").text("Update Company");
    $('#mySubmit').show();
    $('#mySubmit1').show();
    //$('#bankid').show();
    $('#cuscontactbutton').show();
    $('#notebutton').show();
    $("#cuscontactpic").attr("src", "/images/user.png");
    $("#customer-information-cancel").show();
    $("#customer-information1-cancel").show();
    $("#customer-information2-cancel").show();
    $("#customer-information3-cancel").show();
    $("#customer-information4-cancel").show();
    $('#myform input[type=text]').attr("disabled", false);
    $('#myform textarea').attr("disabled", false);
    $('#myform input[type=file]').attr("disabled", false);
    $('#mySubmit1').val("updateaddress").text("Update Address");
    $('#bankid').val("updatebankdetails").text("Update Bank Details");
    $('#notebutton').val("updatenote").text("Update Notes");
    $('#cuscontactbutton').val("savecontact").text("Save Contact");
    $("#bill_country").attr("disabled", false);
    $("#ship_country").attr("disabled", false);
    $('#forclose').css('display', 'none');
    $("#uploadtext").css("display", "block");
    $("#uploadcontact").css("display", "block");

    $('#btnedit').click(function () {
        $('#cuscompany').css('display', 'none');
    });

    $.ajax({
        url: '/Customer/getAllcusDetails?cus_company_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ cus_company_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                var url = 'Customer/CustomerContact?id=' + array.cus_company_Id ;
                $('#customerrecords').load(url);
                editFunction(array);
                $('#customer-information').css('display', 'block');
                $('#additonal').css('display', 'block');
                $("#contacttable").css("display", "none");
                $(".cd-tabs").css("display", "none");
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}


// particular customer
function editcuscompany(clickedvalue) {
    //alert(clickedvalue)
    $('#update').click(function () {
        $('#cuscompany').css('display', 'none');
    });
    
    cuscompany_Id = $('#cus_company_Id').val();
    cusCompany_Name = $('#cus_company_name').val();
    cuslogo = $('#cuscompanypic').attr('src').replace('data:image/;base64,', '');
    //alert(cuslogo);
    cusEmail = $('#cus_email').val();
    //alert(cusEmail);

    if ((cusCompany_Name == "") || (cusEmail == "")) {
        if (cusCompany_Name == "")
            alert("Please Enter Company Name");
        else
            alert("Please Enter Email");
    }
    else {

        var email = document.getElementById('cus_email');
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.value)) {
            alert('Please provide a valid email address');
            email.focus;
            return false;
        }
        else {
            if (clickedvalue == 'update') {
                $.ajax({
                    url: '/Customer/updatecuscompany',
                    type: 'POST',
                    data: JSON.stringify({ cus_company_Id: cuscompany_Id, cus_company_name: cusCompany_Name, cus_email: cusEmail, cus_logo: cuslogo }),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data == "sucess") {
                            $('#savebutton').hide();
                            var url = 'Customer/CustomerCompany';
                            $('#cuscompanyrecords').empty().load(url, function () { Pagination(); });
                            alert("Company Updated Successfully");
                            $('#customertable1').css("display", "none");
                            $('#additonal').css('display', 'block');
                        }
                        else {
                            alert("not updated");
                        }
                    },
                    error: function (data)
                    { alert("Failed!!!");}
                });
            }
            if (clickedvalue == 'Save') {
                $.ajax({

                    url: '/Customer/savecuscompany',
                    type: 'POST',
                    data: JSON.stringify({ cus_company_name: cusCompany_Name, cus_email: cusEmail, cus_logo: cuslogo }),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data.Result == "sucess") {
                            $('#mySubmit').hide();
                            $('#cuscompany_pic').children().attr('disabled', 'disabled');
                            var url = 'Customer/CustomerCompany';
                            $('#cus_company_Id').val(data.ID);
                            $('#cuscompanyrecords').load(url, function () { Pagination(); });
                            alert("company saved Successfully");
                            $('customer-information1').css("display", "block");
                            $('#additonal').css('display', 'block');
                            cus_company_Id = $('#cus_company_Id').val();
                            var url = 'Customer/CustomerContact?id=' + cus_company_Id + '';
                            $('#customerrecords').empty().load(url);
                        }
                        else if (data = "exists") {
                            alert("Company Name alredy exists..Please enter another name");
                        }
                        else {
                            alert("not saved");
                        }
                    },
                    error: function (data)
                    { alert("Failed!!!"); }
                });
            }
        }
    }
}

//Particular customer Company Address
function editcuscompanyaddress(clickedvalue) {

    $('#updateaddress').click(function () {
        $('#cuscompany').css('display', 'none');
    });
    $('#updateaddress').click(function () {
        $('#additional').css('display', 'none');
    });
    cus_company_Id = $('#cus_company_Id').val();
    //alert(cus_company_Id);
    cus_company_name = $('#cus_company_name').val();
    cus_email = $('#cus_email').val();
    bill_street = $('#bill_street').val();
    bill_city = $('#bill_city').val();
    bill_state = $('#bill_state').val();
    bill_postalcode = $('#bill_postalcode').val();
    bill_country = $('#bill_country').val();
    ship_street = $('#ship_street').val();
    ship_city = $('#ship_city').val();
    ship_state = $('#ship_state').val();
    ship_postalcode = $('#ship_postalcode').val();
    ship_country = $('#ship_country').val();

    if ((cus_company_name == "") || (cus_email == "")) {
        if (cus_company_name == "")
            alert("Please Enter Company Name");
        else if (cus_email == "")
            alert("Please Enter Email");
    }
    else {
        if (clickedvalue == 'updateaddress') {
            $.ajax({
                url: '/Customer/updatecuscompanyaddress',
                type: 'POST',
                data: JSON.stringify({cus_company_Id: cus_company_Id, bill_street: bill_street, bill_city: bill_city, bill_state: bill_state, bill_postalcode: bill_postalcode, bill_country: bill_country, ship_street: ship_street, ship_city: ship_city, ship_state: ship_state, ship_postalcode: ship_postalcode, ship_country: ship_country }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data == "sucess") {
                        $('#savebutton').hide();

                        alert("Address Updated Successfully");
                    }
                    else {
                        alert("not updated");
                    }
                },
                error: function (data)
                { alert("Failed!!!"); }
            });
        }
        if (clickedvalue == 'saveaddress') {
          
            $.ajax({
                url: '/Customer/updatecuscompanyaddress',
                type: 'POST',
                data: JSON.stringify({ cus_company_Id: cus_company_Id, bill_street: bill_street, bill_city: bill_city, bill_state: bill_state, bill_postalcode: bill_postalcode, bill_country: bill_country, ship_street: ship_street, ship_city: ship_city, ship_state: ship_state, ship_postalcode: ship_postalcode, ship_country: ship_country }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data == "sucess") {
                        $('#savebutton').hide();
                        alert("Address Saved Successfully");
                    }
                    else {
                        alert("not Saved");
                    }
                },
                error: function (data)
                { alert("Failed!!!"); }
            });
        }
    }
}
//Vendor Bank Details
//function editcompanybankdetails(clickedvalue) {

//    $('#updatebakdetails').click(function () {
//        $('#company').css('display', 'none');
//    });
//    $('#updatebakdetails').click(function () {
//        $('#additional').css('display', 'none');
//    });
//    company_Id = $('#company_Id').val();
//    Bank_Acc_Number = $('#Bank_Acc_Number').val();
//    Bank_Name = $('#Bank_Name').val();
//    Bank_Branch = $('#Bank_Branch').val();
//    IFSC_No = $('#IFSC_No').val();
//    if (clickedvalue == 'updatebankdetails') {
//        $.ajax({
//            url: '/Vendor/updatecompanybankdetails',
//            type: 'POST',
//            data: JSON.stringify({ company_Id: company_Id, Bank_Acc_Number: Bank_Acc_Number, Bank_Name: Bank_Name, Bank_Branch: Bank_Branch, IFSC_No: IFSC_No }),
//            dataType: 'json',
//            contentType: 'application/json',
//            success: function (data) {
//                if (data == "sucess") {
//                    $('#savebutton').hide();

//                    alert("Bank details Updated Successfully");
//                }
//                else {
//                    alert("not updated");
//                }
//            },
//            error: function (data)
//            { alert("Failed!!!"); }

//        });
//    }
//    if (clickedvalue == 'savebankdetails') {
//        $.ajax({
//            url: '/Vendor/savecompanybankdetails',
//            type: 'POST',
//            data: JSON.stringify({ company_Id: company_Id, Bank_Acc_Number: Bank_Acc_Number, Bank_Name: Bank_Name, Bank_Branch: Bank_Branch, IFSC_No: IFSC_No }),
//            dataType: 'json',
//            contentType: 'application/json',
//            success: function (data) {
//                if (data == "sucess") {
//                    $('#savebutton').hide();
//                    alert("Bank details saved Successfully");
//                }
//                else {
//                    alert("not saved");
//                }
//            },
//            error: function (data)
//            { alert("Failed!!!"); }

//        });
//    }
//}

//customer Note
function updatecusnote(clickedvalue) {

    $('#updatenote').click(function () {
        $('#cuscompany').css('display', 'none');
    });
    $('#updatenote').click(function () {
        $('#additional').css('display', 'none');
    });
    cuscompany_Id = $('#cus_company_Id').val();
    cusNote = $('#cus_Note').val();
    if (clickedvalue == 'updatenote') {
        $.ajax({
            url: '/Customer/updatecuscompanynote?',
            type: 'POST',
            data: JSON.stringify({ cus_company_Id: cuscompany_Id, cus_Note: cusNote }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "sucess") {
                    $('#savebutton').hide();

                    alert("Notes Updated Successfully");
                }
                else {
                    alert("not updated");
                }
            },
            error: function (data)
            { alert("Failed!!!"); }
        });
    }
    if (clickedvalue == 'Save Notes') {
        $.ajax({
            url: '/Customer/savecuscompanynote?',
            type: 'POST',
            data: JSON.stringify({ cus_company_Id: cuscompany_Id, cus_Note: cusNote }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "sucess") {
                    $('#savebutton').hide();
                    alert("Notes Saved Successfully");
                }
                else {
                    alert("not Saved");
                }
            },
            error: function (data)
            { alert("Failed!!!"); }
        });
    }
}

//contact details
function updatecusContact(clickedvalue) {
    $('#updatecontact').click(function () {
        $('#cuscompany').css('display', 'none');
    });
    $('#updatecontact').click(function () {
        $('#additional').css('display', 'none');
    });
    cuscompany_Id = $('#cus_company_Id').val();
    CustomerId = $('#Customer_Id').val();
    //alert(Customer_Id);
    Customercontact_Fname = $('#Customer_contact_Fname').val();
    Customercontact_Lname = $('#Customer_contact_Lname').val();
    MobileNo = $('#Mobile_No').val();
    AdharNumber = $('#Adhar_Number').val();
    cusJob_position = $('#cus_Job_position').val();
    imagee = $('#cuscontactpic').attr('src').replace('data:image/;base64,', '');
    EmailId = $('#Email_Id').val();
    var email = document.getElementById('Email_Id');
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.value)) {
        alert('Please provide a valid email address');
        email.focus;
        return false;
    } else {
        if (clickedvalue == 'savecontact') {
           
            $.ajax({
                url: '/Customer/savecuscontactdetails',
                type: 'POST',
                data: JSON.stringify({ cus_company_Id: cuscompany_Id, Customer_contact_Fname: Customercontact_Fname, Customer_contact_Lname: Customercontact_Lname, Mobile_No: MobileNo, Email_Id: EmailId, Adhar_Number: AdharNumber, cus_Job_position: cusJob_position, image: imagee }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data.Result == "sucess") {
                        $('#savebutton').hide();
                        cus_company_Id = $('#cus_company_Id').val();
                        Customer_Id = $('#Customer_Id').val(data.ID);
                        var url = 'Customer/CustomerCompany';
                        $('#cus_company_Id').val(data.ID);
                        $('#cuscompanyrecords').load(url, function () { Pagination(); });
                        var url1 = 'Customer/CustomerContact?id=' + cus_company_Id + '';
                        $('#cuscontacttable').empty().load(url1, function () { Pagination(); });
                        alert("Contact Details saved Successfully");
                        $("[id='Customer_contact_Fname']").val("");
                        $("[id='Customer_contact_Lname']").val("");
                        $("[id='Mobile_No']").val("");
                        $("[id='Email_Id']").val("");
                        $("[id='Adhar_Number']").val("");
                        $("[id='cus_Job_position']").val("");
                        $("#cuscontactpic").attr("src", "/images/user.png");
                    }
                    else {
                        alert("not saved");
                    }
                },
                error: function (json)
                { alert("Failed!!!"); }
            });
        }
        if (clickedvalue == 'updatecontact') {
            $.ajax({
                url: '/Customer/updatecuscontactdetails',
                type: 'POST',
                data: JSON.stringify({ Customer_Id: CustomerId, Customer_contact_Fname: Customercontact_Fname, Customer_contact_Lname: Customercontact_Lname, Mobile_No: MobileNo, Email_Id: EmailId, Adhar_Number: AdharNumber, cus_Job_position: cusJob_position, image: imagee }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data == "sucess") {
                        $('#savebutton').hide();
                        cus_company_Id = $('#cus_company_Id').val();
                        //alert(cus_company_Id);
                        var url = 'Customer/CustomerCompany';
                        $('#cuscompanyrecords').load(url, function () { Pagination(); });
                        var url1 = 'Customer/CustomerContact?id=' + cus_company_Id + '';
                        $('#cuscontacttable').empty().load(url1);
                        alert("Contact Details updated Successfully");
                        $('#cuscontactbutton').val("savecontact").text("Save Contact");
                        $("[id='Customer_contact_Fname']").val("");
                        $("[id='Customer_contact_Lname']").val("");
                        $("[id='Mobile_No']").val("");
                        $("[id='Email_Id']").val("");
                        $("[id='Adhar_Number']").val("");
                        $("[id='cus_Job_position']").val("");
                        $("#cuscontactpic").attr("src", "/images/user.png");
                    }
                    else {
                        alert("not updated");
                    }
                },
                error: function (data)
                { alert("Failed!!!"); }
            });
        }
    }
}
//customer contact details editing based on customer id

function editcuscontactperson(id) {
    //alert(id);
    //alert($('#contactbutton').val());
    $('#cuscontactbutton').val("updatecontact").text("Update Contact");
    $.ajax({
        url: '/Customer/getCustomerContact?Customer_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ Customer_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                $('#Customer_Id').val(array.Customer_Id);
                $('#Customer_contact_Fname').val(array.Customer_contact_Fname);
                $('#Customer_contact_Lname').val(array.Customer_contact_Lname);
                $('#Mobile_No').val(array.Mobile_No);
                $('#Email_Id').val(array.Email_Id);
                $('#Adhar_Number').val(array.Adhar_Number);
                $('#cus_Job_position').val(array.cus_Job_position);
                if (array.image != "/images/user.png" && array.image != null && array.image != "") {
                    $('#cuscontactpic').attr('src', 'data:image/;base64,' + array.image);
                } else
                    $('#cuscontactpic').attr('src', array.image);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

//customer deleting

function deleteCustomer(id) {
    //alert(id);
    var retVal = confirm("Do you want to delete record...!");
    if (retVal == true) {
        $.ajax({
            url: '/Customer/deleteCustomer',
            type: 'POST',
            data: JSON.stringify({ Customer_Id: id }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "sucess") {
                    cus_company_Id = $('#cus_company_Id').val();
                    var url = 'Customer/CustomerContact?id=' + cus_company_Id + '';
                    $('#customerrecords').load(url);
                    alert("Contact Person Deleted Successfully");
                   
                }
                else {
                    alert("Not deleted");
                }
            },
            error: function (data)
            { alert("Failed!!!"); }
        });
        return true;
    }

    else {
        return false;
    }
}

//Customer contact person invite

function inviteCustomer(id) {
    //alert(id);
    cus_company_Id = $('#cus_company_Id').val()
    //alert(company_Id);
    var retVal = confirm("Do you want to send invitation...!");
    if (retVal == true) {
        $.ajax({
            url: '/Customer/inviteCustomer',
            type: 'POST',
            data: JSON.stringify({ Customer_Id: id, cus_company_Id: cus_company_Id }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    alert("sai");
                }
                else if (data == "Exists") {
                    alert("Email Id Already Exists!!! Try Another");
                }
                else {
                    cus_company_Id = $('#cus_company_Id').val();
                    var url = 'Customer/CustomerContact?id=' + cus_company_Id + '';
                    $('#customerrecords').load(url);
                    alert("Invitation sent Successfully.Please Click on Activation Link Sent to Your Registered Email-ID and Proceed Furthur");
                }
            },
            error: function (data)
            { alert("Failed!!!"); }
        });
        return true;
    }

    else {
        return false;
    }
}

//<!------ Image Upload ------>

function upload() {
    var ext = $('#fileupload').val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
        alert('Invalid File Type');
    }
    else {
        var data = new FormData();
        var files = $("#fileupload").get(0).files;
        if (files.length > 0) {
            data.append("helpSectionImages", files[0]);
        }
        $.ajax({
            url: '/Customer/UpdatecusCompanyPic',
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                $("#cuscompanypic").attr("src", "data:image/;base64," + response);
            },
            error: function (er) {
                alert("Failed To Upload Pic!!! Try Again");
            }
        });
    }
}

function upload1() {
    var ext = $('#fileupload1').val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
        alert('Invalid File Type');
    }
    else {
        var data = new FormData();
        var files = $("#fileupload1").get(0).files;
        if (files.length > 0) {
            data.append("helpSectionImages", files[0]);
        }
        $.ajax({
            url: '/Customer/UpdatecuscontactPic',
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                $("#cuscontactpic").attr("src", "data:image/;base64," + response);
            },
            error: function (er) {
                alert("Failed To Upload Pic!!! Try Again");
            }
        });
    }
}
//// closeing view with close button
//function closingview() {
//    var url = 'Customer/CustomerCompany';
//    $('#cuscompanyrecords').empty().load(url, function () { Pagination(); });
//    $('#customer-information').css('display', 'none');
//    $('#customer-information1').css('display', 'none');
//    $('#additonal').css('display', 'none');
//    $("#contacttable").css("display", "none");
//    $(".cd-tabs").css("display", "none");
//    $('#forclose').css('display', 'none');
//}

//View customer
function viewCustomer(id) {
    //alert(id);
    $('#forclose').css('display', 'block');
    $.ajax({
        url: '/customer/getAllcusDetails?cus_company_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ cus_company_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                var url = 'customer/CustomerContact?id=' + array.cus_company_Id + '';
                $('#customerrecords').load(url);
                editFunction(array);
                $('#customer-information').css('display', 'block');
                $('#customer-information1').css('display', 'block');
                $('#additonal').css('display', 'none');
                $("#contacttable").css("display", "block");
                $(".cd-tabs").css("display", "block");
                $('#mySubmit').hide();
                $('#mySubmit1').hide();
                $('#bankid').hide();
                $('#cuscontactbutton').hide();
                $('#taxid').hide();
                $('#notebutton').hide();
                $("#customer-information-cancel").hide();
                $("#customer-information1-cancel").hide();
                $("#customer-information2-cancel").hide();
                $("#customer-information3-cancel").hide();
                $("#customer-information4-cancel").hide();
                $('#myform input[type=text]').attr("disabled", true);
                $('#myform textarea').attr("disabled", true);
                $('#myform input[type=file]').attr("disabled", true);
                $("#bill_country").attr("disabled", true);
                $("#ship_country").attr("disabled", true);
                $("#uploadtext").css("display", "none");
                $("#uploadcontact").css("display", "none");
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}

function addingcusjobpositions() {
    cuscompany_Id = $('#cus_company_Id').val()
   // alert(cuscompany_Id);
    Jobposition = $('#newposition').val();
   // alert(Jobposition);
    $.ajax({
        url: '/Customer/addPosition',
        type: 'POST',
        data: JSON.stringify({ cus_Job_position: Jobposition, cus_company_Id: cuscompany_Id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.Result == "sucess") {
                var url = 'Customer/CustomerCompany';
                $('#cuscompanyrecords').load(url, function () { Pagination(); });
                alert("Job position added Successfully.click close and select from list");
                var array = data.ID;
                forunderstand(array);
                $('#newposition').val("");
            }
            else if (data = "exists") {
                alert("That Job Position alredy exists..Please select or enter new Position");
                $('#newposition').val("");
            }
            else {
                alert("not saved");
                $('#newposition').val("");
            }
        },
        error: function (data) {
            alert("Failed!!!");
            $('#newposition').val("");
        }
    });

}

function forunderstand(array) {

    var value = "";
    for (var i = 0; i < array.length; i++) {

        value = value + "<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + array[i] + "</div>";
    }
    var esc1 = value;
    $('#cusjobposition').empty().append(esc1);
    $('#cusjobposition').prepend('<div class="positions1 position"><i class="fa fa-plus-circle" aria-hidden="true"></i>Job Position<i class="fa fa-times" aria-hidden="true"></i></div>');

    $(".display-positions .position").click(function () {
        $(".add-position").css("display", "block");

        $(".add-position .add-button").click(function () {
            return textval();
        });

        $(".close-button").click(function () {
            $(".add-position").css("display", "none");
            $(".add-position input[type='text']").val("");
        });

    });
}
