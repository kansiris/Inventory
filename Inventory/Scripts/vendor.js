//<!---------- Display Vendor Information and reset all forms --------------->
$("#add-vendor").click(function () {
    $("#vendor-information").css("display", "block");
    $("#additon").css("display", "none");
    //$("#contacttable").css("display", "block");
    $('#mySubmit').css("display", "block");
    $("#vendor-information input").val("");
    $("#Vendor_Id").val("");
    $("#companypic").attr("src", "/images/user.png");
    $("#vendor-information1 input, .cd-tabs input, .cd-tabs textarea, .cd-tabs dropdown").val("");
    $("#bill_country").val("");
    $("#ship_country").val("");
    $("#vendor-information1").css("display", "none");
    $('#mySubmit').val("Save").text("Save");
    $('#mySubmit1').val("saveaddress").text("Save Address");
    $('#bankid').val("savebankdetails").text("Save Bank Details");
    $('#notebutton').val("Save Notes").text("Save Notes");
    $('#contactbutton').val("savecontact").text("Save Contact");
    $('#myform input[type=text]').attr("disabled", false);
    $('#myform textarea').attr("disabled", false);
    $('#myform input[type=file]').attr("disabled", false);
    $('#mySubmit').show();
    $('#mySubmit1').show();
    $('#bankid').show();
    $('#contactbutton').show();
    $("#contactbutton").css("display", "block");
    $('#notebutton').show();
    $("#bill_country").attr("disabled", false);
    $("#ship_country").attr("disabled", false);
    $("#vendor-information-cancel").show();
    $("#vendor-information1-cancel").show();
    $("#vendor-information2-cancel").show();
    $("#vendor-information3-cancel").show();
    $("#vendor-information4-cancel").show();
    $("#forclose").css("display", "none");
    $("#uploadtext").css("display", "block");
    $("#uploadcontact").css("display", "block");
    $("#contactpic").attr("src", "/images/user.png");
});
$("#vendor-information-cancel").click(function () {
    $("#vendor-information input").val("");
    $("#vendor-information1 input, .cd-tabs input, .cd-tabs textarea").val("");
    $('#mySubmit').val("Save").text("Save");
    $("#vendor-information").css("display", "none");
    $("#vendor-information1").css("display", "none");
});

function forCancel() {
    $("#additon").css("display", "block");
    $(".cd-tabs").css("display", "none");
    //$("#vendor-information1 input").val("");
}
$("#vendor-information1-cancel").click(function () {
    forCancel();
});
$("#vendor-information2-cancel").click(function () {
    forCancel();
});
$("#vendor-information3-cancel").click(function () {
    forCancel();
});
$("#vendor-information4-cancel").click(function () {
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
    $("#vendortable").css("display", "none");
    $("#vendortable1").css("display", "block");
});

$("#list-view").click(function (e) {
    $("#vendortable").css("display", "block");
    $("#vendortable1").css("display", "none");
    var url = 'Vendor/VendorCompany';
    $('#companyrecords').load(url, function () { Pagination(); });
});

$("#refresh").click(function (e) {
    var url = 'Vendor/VendorCompany';
    $('#companyrecords').load(url, function () { Pagination(); });
    location.reload();
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


    // <!----- Table Pagination ---->

    // $("#vendor-information1").hide();
    $(".cd-tabs").css("display", "none");

    $("#additon").click(function () {
        $("#vendor-information1").css("display", "block");
        $(".cd-tabs").css("display", "block");
        //$("#contacttable").css("display", "block");
    });
});

//function Pagination() {

//    $('#vendortable1').after('<div id="nav"></div>');
//    var rowsShown = 3;
//    var rowsTotal = $('#vendortable tbody tr').length;
//    var numPages = rowsTotal / rowsShown;
//    for (i = 0; i < numPages; i++) {
//        var pageNum = i + 1;
//        $('#nav').append('<a href="#" class="btn btn-success" rel="' + i + '">' + pageNum + '</a> ');
//    }
//    $('#vendortable tbody tr').hide();
//    $('#vendortable tbody tr').slice(0, rowsShown).show();
//    $('#nav a:first').addClass('active');
//    $('#nav a').bind('click', function () {
//        $('#nav a').removeClass('active');
//        $(this).addClass('active');
//        var currPage = $(this).attr('rel');
//        var startItem = currPage * rowsShown;
//        var endItem = startItem + rowsShown;
//        $('#vendortable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
//                css('display', 'table-row').animate({ opacity: 1 }, 300);
//    });



//    $('#vendortable1 > div').hide();
//    $('#vendortable1 > div').slice(0, rowsShown).show();
//    $('#nav a:first').addClass('active');
//    $('#nav a').bind('click', function () {

//        $('#nav a').removeClass('active');
//        $(this).addClass('active');
//        var currPage = $(this).attr('rel');
//        var startItem = currPage * rowsShown;
//        var endItem = startItem + rowsShown;
//        $('#vendortable1 > div').css('opacity', '0.0').hide().slice(startItem, endItem).
//                css('display', 'table-row').animate({ opacity: 1 }, 300);
//    });
//}

//<!------ Random Colors ------>

//<!----- Table Pagination ---->
function Pagination() {
    if (window.matchMedia('(max-width: 768px)').matches) {
        $('#vendortable tbody tr').css({ "display": "table", "width": "100%" });
    }
    $('#vendortable1').after('<div id="nav"></div>');
    var rowsShown = 3;
    var rowsTotal = $('#vendortable tbody tr').length;
    var numPages = rowsTotal / rowsShown;
    for (i = 0; i < numPages; i++) {
        var pageNum = i + 1;
        $('#nav').append('<a href="#" class="btn btn-success" rel="' + i + '">' + pageNum + '</a> ');
    }
    $('#vendortable tbody tr').hide();
    $('#vendortable tbody tr').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {

        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#vendortable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);
        if (window.matchMedia('(max-width: 768px)').matches) {
            $('#vendortable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table').animate({ opacity: 1 }, 300);
        }
    });

    $('#vendortable1 > div').hide();
    $('#vendortable1 > div').slice(0, rowsShown).show();
    $('#nav a:first').addClass('active');
    $('#nav a').bind('click', function () {
        $('#nav a').removeClass('active');
        $(this).addClass('active');
        var currPage = $(this).attr('rel');
        var startItem = currPage * rowsShown;
        var endItem = startItem + rowsShown;
        $('#vendortable1 > div').css('opacity', '0.0').hide().slice(startItem, endItem).
                css('display', 'table-row').animate({ opacity: 1 }, 300);
    });
}
//<!----- Table Pagination ---->





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

function textval() {
    $(".display-positions .positions1").click(function () {
        var c = $(this).text();
        $("input.selected-position").val(c);
        $(".display-positions").css("display", "none");
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
        $(".add-position").css("display", "none");
        $(".add-position input[type='text']").val("");
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

//function deleteRecord(id) {
//    // alert(id);
//    var retVal = confirm("Do you want to delete record...!");
//    if (retVal == true) {
//        $.ajax({
//            url: '/Vendor/deleteRecord',
//            type: 'POST',
//            data: JSON.stringify({ company_Id: id }),
//            dataType: 'json',
//            contentType: 'application/json',
//            success: function (data) {
//                if (data == "unique") {
//                    alert("sai");
//                }
//                else {
//                    alert("Company Deleted Successfully");
//                    var url = 'Vendor/VendorCompany';
//                    $('#companyrecords').empty().load(url, function () { Pagination(); });
//                    $('#vendor-information').css('display', 'none');
//                    $('#additon').css('display', 'none');
//                    $(".cd-tabs").css('display', 'none');

//                }
//            },
//            error: function (data)
//            { alert("Failed!!!"); }
//        });

//        return true;
//    }

//    else {
//        return false;
//    }
//}
//Assigning values to inputs
function editFunction(array) {
    $('#company_Id').val(array.company_Id);
    $('#Company_Name').val(array.Company_Name);
    $('#Email').val(array.Email);
    $('#Bank_Acc_Number').val(array.Bank_Acc_Number);
    $('#Bank_Name').val(array.Bank_Name);
    $('#Bank_Branch').val(array.Bank_Branch);
    $('#IFSC_No').val(array.IFSC_No);
    $('#Note').val(array.Note);
    if (array.logo != "/images/user.png" && array.logo != null && array.logo != "") {
        $('#companypic').attr('src', 'data:image/;base64,' + array.logo);
    } else
        $('#companypic').attr('src', array.logo);
    $('#Vendor_Id').val(array.Vendor_Id);
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

//Get Particular Vendor Record
function getEditDetails(id) {
    $('#mySubmit').val("update").text("Update Company");
    $('#mySubmit').show();
    $('#mySubmit1').show();
    $('#bankid').show();
    $('#contactbutton').show();
    $('#notebutton').show();
    $("#contactpic").attr("src", "/images/user.png");
    $("#vendor-information-cancel").show();
    $("#vendor-information1-cancel").show();
    $("#vendor-information2-cancel").show();
    $("#vendor-information3-cancel").show();
    $("#vendor-information4-cancel").show();
    $('#myform input[type=text]').attr("disabled", false);
    $('#myform textarea').attr("disabled", false);
    $('#myform input[type=file]').attr("disabled", false);
    $('#mySubmit1').val("updateaddress").text("Update Address");
    $('#bankid').val("updatebankdetails").text("Update Bank Details");
    $('#notebutton').val("updatenote").text("Update Notes");
    $('#contactbutton').val("savecontact").text("Save Contact");
    $("#bill_country").attr("disabled", false);
    $("#ship_country").attr("disabled", false);
    $('#forclose').css('display', 'none');
    $("#uploadtext").css("display", "block");
    $("#uploadcontact").css("display", "block");

    $('#btnedit').click(function () {
        $('#company').css('display', 'none');
    });

    $.ajax({
        url: '/Vendor/getAllDetails?company_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ company_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                var url = 'Vendor/VendorContact?id=' + array.company_Id + '';
                editFunction(array);
                $('#vendor-information').css('display', 'block');
                $('#additon').css('display', 'block');
                //$("#contacttable").css("display", "block");
                $(".cd-tabs").css("display", "none");
                $('#vendorrecords').load(url);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}


//Particular Vendor
function editcompany(clickedvalue) {

    $('#update').click(function () {
        $('#company').css('display', 'none');
    });
    $('#update').click(function () {
        $('#additional').css('display', 'none');
    });
    company_Id = $('#company_Id').val();
    Company_Name = $('#Company_Name').val();
    logo = $('#companypic').attr('src').replace('data:image/;base64,', '');
    Email = $('#Email').val();

    if ((Company_Name == "") || (Email == "")) {
        if (Company_Name == "")
            alert("Please Enter Company Name");
        else
            alert("Please Enter Email");
    }
    else {

        var email = document.getElementById('Email');
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.value)) {
            alert('Please provide a valid email address');
            email.focus;
            return false;
        }
        else {
            if (clickedvalue == 'update') {
                $.ajax({
                    url: '/Vendor/updatecompany',
                    type: 'POST',
                    data: JSON.stringify({ company_Id, Company_Name, Email, logo }),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data == "sucess") {
                            $('#savebutton').hide();
                            var url = 'Vendor/VendorCompany';
                            $('#companyrecords').empty().load(url, function () { Pagination(); });
                            successmes("Company Updated Successfully");
                            //alert("Company Updated Successfully");
                            $('#additon').css('display', 'block');
                        }
                        else {
                            alert("not updated");
                        }
                    },
                    error: function (data)
                    { alert("Failed!!!"); }
                });
            }
            if (clickedvalue == 'Save') {
                $.ajax({

                    url: '/Vendor/savecompany',
                    type: 'POST',
                    data: JSON.stringify({ Company_Name, Email, logo }),
                    dataType: 'json',
                    contentType: 'application/json',
                    success: function (data) {
                        if (data.Result == "sucess") {
                            $('#mySubmit').hide();
                            $('#company_pic').children().attr('disabled', 'disabled');
                            var url = 'Vendor/VendorCompany';
                            $('#company_Id').val(data.ID);
                            $('#companyrecords').load(url, function () { Pagination(); });
                            alert("Company saved.");
                            $("#additon").css("display", "block");
                            //successmsg(data.Result);
                            $('vendor-information1').css("display", "block");

                            company_Id = $('#company_Id').val();
                            var url = 'Vendor/VendorContact?id=' + company_Id + '';
                            $('#vendorrecords').empty().load(url);

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

//Particular vendor Company Address
function editcompanyaddress(clickedvalue) {

    $('#updateaddress').click(function () {
        $('#company').css('display', 'none');
    });
    $('#updateaddress').click(function () {
        $('#additional').css('display', 'none');
    });
    company_Id = $('#company_Id').val();
    // alert(company_Id);
    Company_Name = $('#Company_Name').val();
    Email = $('#Email').val();
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

    if ((Company_Name == "") || (Email == "")) {
        if (Company_Name == "")
            alert("Please Enter Company Name");
        else if (Email == "")
            alert("Please Enter Email");
    }
    else {
        if (clickedvalue == 'updateaddress') {
            $.ajax({
                url: '/Vendor/updatecompanyaddress',
                type: 'POST',
                data: JSON.stringify({ company_Id: company_Id, bill_street: bill_street, bill_city: bill_city, bill_state: bill_state, bill_postalcode: bill_postalcode, bill_country: bill_country, ship_street: ship_street, ship_city: ship_city, ship_state: ship_state, ship_postalcode: ship_postalcode, ship_country: ship_country }),
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
                url: '/Vendor/savecompanyaddress',
                type: 'POST',
                data: JSON.stringify({ company_Id: company_Id, bill_street: bill_street, bill_city: bill_city, bill_state: bill_state, bill_postalcode: bill_postalcode, bill_country: bill_country, ship_street: ship_street, ship_city: ship_city, ship_state: ship_state, ship_postalcode: ship_postalcode, ship_country: ship_country }),
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
function editcompanybankdetails(clickedvalue) {

    $('#updatebakdetails').click(function () {
        $('#company').css('display', 'none');
    });
    $('#updatebakdetails').click(function () {
        $('#additional').css('display', 'none');
    });
    company_Id = $('#company_Id').val();
    Bank_Acc_Number = $('#Bank_Acc_Number').val();
    Bank_Name = $('#Bank_Name').val();
    Bank_Branch = $('#Bank_Branch').val();
    IFSC_No = $('#IFSC_No').val();
    if (clickedvalue == 'updatebankdetails') {
        $.ajax({
            url: '/Vendor/updatecompanybankdetails',
            type: 'POST',
            data: JSON.stringify({ company_Id: company_Id, Bank_Acc_Number: Bank_Acc_Number, Bank_Name: Bank_Name, Bank_Branch: Bank_Branch, IFSC_No: IFSC_No }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "sucess") {
                    $('#savebutton').hide();

                    alert("Bank details Updated Successfully");
                }
                else {
                    alert("not updated");
                }
            },
            error: function (data)
            { alert("Failed!!!"); }

        });
    }
    if (clickedvalue == 'savebankdetails') {
        $.ajax({
            url: '/Vendor/savecompanybankdetails',
            type: 'POST',
            data: JSON.stringify({ company_Id: company_Id, Bank_Acc_Number: Bank_Acc_Number, Bank_Name: Bank_Name, Bank_Branch: Bank_Branch, IFSC_No: IFSC_No }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "sucess") {
                    $('#savebutton').hide();
                    alert("Bank details saved Successfully");
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

//Vendor Note
function updatecompanynote(clickedvalue) {

    $('#updatenote').click(function () {
        $('#company').css('display', 'none');
    });
    $('#updatenote').click(function () {
        $('#additional').css('display', 'none');
    });
    company_Id = $('#company_Id').val();
    Note = $('#Note').val();
    if (clickedvalue == 'updatenote') {
        $.ajax({
            url: '/Vendor/updatecompanynote?company_Id=' + company_Id + '&Note=' + Note,
            type: 'POST',
            data: JSON.stringify({ company_Id, Note }),
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
            url: '/Vendor/savecompanynote?company_Id=' + company_Id + '&Note=' + Note,
            type: 'POST',
            data: JSON.stringify({ company_Id, Note }),
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
function updateContact(clickedvalue) {
    $('#updatecontact').click(function () {
        $('#company').css('display', 'none');
    });
    $('#updatecontact').click(function () {
        $('#additional').css('display', 'none');
    });
    company_Id = $('#company_Id').val();
    Vendor_Id = $('#Vendor_Id').val();
    // alert(clickedvalue);
    Contact_PersonFname = $('#Contact_PersonFname').val();
    Contact_PersonLname = $('#Contact_PersonLname').val();
    Mobile_No = $('#Mobile_No').val();
    Adhar_Number = $('#Adhar_Number').val();
    Job_position = $('#Job_position').val();
    image = $('#contactpic').attr('src').replace('data:image/;base64,', '');
    emailid = $('#emailid').val();
    var email = document.getElementById('emailid');
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.value)) {
        alert('Please provide a valid email address');
        email.focus;
        return false;
    } else {
        if (clickedvalue == 'savecontact') {
            $("#contacttable").css("display", "block");

            $.ajax({
                url: '/Vendor/savecontactdetails',
                type: 'POST',
                data: JSON.stringify({ company_Id: company_Id, Contact_PersonFname: Contact_PersonFname, Contact_PersonLname: Contact_PersonLname, Mobile_No: Mobile_No, emailid: emailid, Adhar_Number: Adhar_Number, Job_position: Job_position, image: image }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data.Result == "sucess") {
                        $('#savebutton').hide();
                        company_Id = $('#company_Id').val();
                        $('#Vendor_Id').val(data.ID);
                        var url = 'Vendor/VendorContact?id=' + company_Id + '';
                        $('#vendorrecords').load(url);
                        alert("Contact Details saved Successfully");
                        $("[id='Contact_PersonFname']").val("");
                        $("[id='Contact_PersonLname']").val("");
                        $("[id='Mobile_No']").val("");
                        $("[id='emailid']").val("");
                        $("[id='Adhar_Number']").val("");
                        $("[id='Job_position']").val("");
                        $("#contactpic").attr("src", "/images/user.png");
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
                url: '/Vendor/updatecontactdetails',
                type: 'POST',
                data: JSON.stringify({ Vendor_Id: Vendor_Id, Contact_PersonFname: Contact_PersonFname, Contact_PersonLname: Contact_PersonLname, Mobile_No: Mobile_No, emailid: emailid, Adhar_Number: Adhar_Number, Job_position: Job_position, image: image }),
                dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    if (data == "sucess") {
                        $('#savebutton').hide();
                        company_Id = $('#company_Id').val();
                        var url = 'Vendor/VendorContact?id=' + company_Id + '';
                        $('#vendorrecords').load(url);
                        alert("Contact Details updated Successfully");
                        $('#contactbutton').val("savecontact").text("Save Contact");
                        $("[id='Contact_PersonFname']").val("");
                        $("[id='Contact_PersonLname']").val("");
                        $("[id='Mobile_No']").val("");
                        $("[id='emailid']").val("");
                        $("[id='Adhar_Number']").val("");
                        $("[id='Job_position']").val("");
                        $("#contactpic").attr("src", "/images/user.png");
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
//vendor contatc details editing based on vendor id

function editcontactperson(id) {
    //alert(id);
    $('#contactbutton').val("updatecontact").text("Update Contact");
    $.ajax({
        url: '/Vendor/getVendorContact?Vendor_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ Vendor_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                $('#Vendor_Id').val(array.Vendor_Id);
                $('#Contact_PersonFname').val(array.Contact_PersonFname);
                $('#Contact_PersonLname').val(array.Contact_PersonLname);
                $('#Mobile_No').val(array.Mobile_No);
                $('#emailid').val(array.emailid);
                $('#Adhar_Number').val(array.Adhar_Number);
                $('#Job_position').val(array.Job_position);
                if (array.image != "/images/user.png" && array.image != null && array.image != "") {
                    $('#contactpic').attr('src', 'data:image/;base64,' + array.image);
                } else
                    $('#contactpic').attr('src', array.image);
            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });

}

//vendor deleting

function deleteVendor(id) {
    //alert(id);
    var retVal = confirm("Do you want to delete record...!");
    if (retVal == true) {
        $.ajax({
            url: '/Vendor/deleteVendor',
            type: 'POST',
            data: JSON.stringify({ Vendor_Id: id }),
            dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data == "unique") {
                    alert("Not deleted");
                }
                else {
                    company_Id = $('#company_Id').val();
                    var url = 'Vendor/VendorContact?id=' + company_Id + '';
                    $('#vendorrecords').load(url);
                    alert("Contact Person Deleted Successfully");

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

//vendor invite

function inviteVendor(id) {
    //alert(id);
    company_Id = $('#company_Id').val()
    //alert(company_Id);
    var retVal = confirm("Do you want to send invitation...!");
    if (retVal == true) {
        $.ajax({
            url: '/Vendor/inviteVendor',
            type: 'POST',
            data: JSON.stringify({ Vendor_Id: id, company_Id: company_Id }),
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
                    company_Id = $('#company_Id').val();
                    var url = 'Vendor/VendorContact?id=' + company_Id + '';
                    $('#vendorrecords').load(url);
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
            url: '/Vendor/UpdateCompanyPic',
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                $("#companypic").attr("src", "data:image/;base64," + response);
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
            url: '/Vendor/UpdateVendorPic',
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            success: function (response) {
                $("#contactpic").attr("src", "data:image/;base64," + response);
            },
            error: function (er) {
                alert("Failed To Upload Pic!!! Try Again");
            }
        });
    }
}




//View vendor
function viewVendor(id) {
    //alert(id);
    $("#forclose").css("display", "block");
    $.ajax({
        url: '/Vendor/getAllDetails?company_Id=' + id,
        type: 'POST',
        data: JSON.stringify({ company_Id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data == "unique") {
                alert("sai");
            }
            else {
                var array = JSON.parse(data);
                var url = 'Vendor/VendorContact?id=' + array.company_Id + '';
                $('#vendorrecords').load(url);
                editFunction(array);
                $('#vendor-information').css('display', 'block');
                $('#vendor-information1').css('display', 'block');
                $('#additon').css('display', 'none');
                $("#contacttable").css("display", "block");
                $(".cd-tabs").css("display", "block");
                $('#mySubmit').hide();
                $('#mySubmit1').hide();
                $('#bankid').hide();
                $('#contactbutton').hide();
                $('#notebutton').hide();
                $("#vendor-information-cancel").hide();
                $("#vendor-information1-cancel").hide();
                $("#vendor-information2-cancel").hide();
                $("#vendor-information3-cancel").hide();
                $("#vendor-information4-cancel").hide();
                $('#myform input[type=text]').attr("disabled", true);
                $('#myform textarea').attr("disabled", true);
                $('#myform input[type=file]').attr("disabled", true);
                $("#bill_country").attr("disabled", true);
                $("#ship_country").attr("disabled", true);
                $("#forclose").css("display", "block");
                $("#uploadtext").css("display", "none");
                $("#uploadcontact").css("display", "none");
                //$("#contactpic").attr("src", "/images/user.png");

            }
        },
        error: function (data)
        { alert("Failed!!!"); }
    });
}

function addingjobpositions(company_Id) {
    company_Id = $('#company_Id').val();
    Job_position = $('#newposition').val();
    //alert(company_Id);
    //alert(Job_position);
    $.ajax({
        url: '/Vendor/addPosition',
        type: 'POST',
        data: JSON.stringify({ Job_position: Job_position, company_Id: company_Id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.Result == "sucess") {
                var url = 'Vendor/VendorCompany';
                $('#companyrecords').load(url, function () { Pagination(); });
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
    $('#jobposition').empty().append(esc1);
    $('#jobposition').prepend('<div class="positions1 position"><i class="fa fa-plus-circle" aria-hidden="true"></i>Job Position</div>');

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

//   function errormsg(msg) {
//       $("body").overhang({
//           type: "error",
//           message: msg,
//           closeConfirm: false
//       });
//   }

//function successmsg(msg) {
//    $("body").overhang({
//        type: "success",
//        message: msg,
//        closeConfirm: false
//    });
//}
