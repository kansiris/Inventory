//<!-- Toogle Function -->
//    <script type="text/javascript">
        $(document).ready(function () {
            Pagination();
            $("#checkbox1").val($(this).is(':checked'));
            $("#allowaccess").val($(this).is(':checked'));

            $("#checkbox1").change(function () {
                if ($(this).is(":checked")) {
                    $(".toggle-box").css("visibility", "visible");
                } else {
                    $(".toggle-box").css("visibility", "hidden");
                }
            });
            //Permissions Area
            $("#allowaccess").change(function () {
                if ($(this).is(":checked")) {
                    $("#permissionsarea").css("visibility", "visible");
                } else {
                    $("#permissionsarea").css("visibility", "hidden");
                }
            });

            //Customer Access
            $("#Customer_Access").change(function () {
                if ($(this).is(":checked")) {
                    $("#Customer_Access").prop('checked', true);
                } else {
                    $("#Customer_Access").prop('checked', false);
                }
            });

            //Vendor Access
            $("#Vendor_Access").change(function () {
                if ($(this).is(":checked")) {
                    $("#Vendor_Access").prop('checked', true);
                } else {
                    $("#Vendor_Access").prop('checked', false);
                }
            });

        });
//</script>
//<!-- Toogle Function -->
//<!------ Random Colors ------>
//<script type="text/javascript">
    $(document).ready(function (e) {
        $(".top-button").each(function () {
            var colors = ["#f4511e", "#7e57c2", "#455a64", "#512da8", "#c2185b", "#5c6bc0", "#0288d1", "#f4511e", "#ef6c00", "#0097a7", "#5c6bc0", "#5d4037"];
            var len = colors.length;
            var rand = Math.floor(Math.random() * len);
            $(this).css("background", colors[rand]);
        });
        //var url = new URL(window.location.href).searchParams;
        var profileurl = location.search.split('type=')[1];//url.get('type');
        if (profileurl == 'upload') {
            $(".cd-tabs-navigation li a[data-content='essentials'], ul.cd-tabs-content li[data-content='essentials']").removeClass("selected");
            $(".cd-tabs-navigation li a[data-content='address'], ul.cd-tabs-content li[data-content='address']").addClass("selected");
        }
        var usedNames = {};
        $("select[name='Item2.country'] > option").each(function () {
            if (usedNames[this.text]) {
                $(this).remove();
            } else {
                usedNames[this.text] = this.value;
            }
            var options = 'Select Country' + $('#Item2_country option');
            options.sort(function (a, b) {
                if (a.text.toUpperCase() > b.text.toUpperCase()) return 1;
                else if (a.text.toUpperCase() < b.text.toUpperCase()) return -1;
                else return 0;
            });

            $('#Item2_country').empty().append(options);


        });
    });
//</script>
//<!------ Random Colors ------>
//<!------ Image Upload ------>

//<script>
    function upload() {
        var ext = $('#fileupload').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
            alert('Invalid File Type');
        }
        else {
            var id = location.search.split('id=')[1];
            var data = new FormData();
            var files = $("#fileupload").get(0).files;
            if (files.length > 0) {
                data.append("helpSectionImages", files[0]);
            }
            $.ajax({
                url: '/UserProfile/UpdateCompanyPic?id='+id,
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {
                    $("#companypic").attr("src","data:image/;base64,"+response);
                },
                error: function (er) {
                    alert("Failed To Upload Pic!!! Try Again");
                }
            });
        }
    }
//</script>

//<script>
    function upload1() {
        var ext = $('#fileupload1').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
            alert('Invalid File Type');
        }
        else {
            var file = $("#fileupload1").get(0).files;
            var id = location.search.split('id=')[1];//new URL(window.location.href).searchParams.get('id');
            var data = new FormData();
            var files = $("#fileupload1").get(0).files;
            if (files.length > 0) {
                data.append("helpSectionImages", files[0]);
            }
            $.ajax({
                url: '/UserProfile/UpdateProfilePic?id='+id,
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {
                    $("#smallprofilepic").attr("src","data:image/;base64,"+response);
                    $("#profilepic").attr("src","data:image/;base64,"+response);
                },
                error: function (er) {
                    alert("Failed To Upload Pic!!! Try Again");
                }
            });
        }
    }
//</script>

//<script>
    function upload2() {
        var ext = $('#fileupload2').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
            alert('Invalid File Type');
        }
        else {
            var id =$('#Item4_Staff_Id').val();
            var data = new FormData();
            var files = $("#fileupload2").get(0).files;
            if (files.length > 0) {
                data.append("helpSectionImages", files[0]);
            }
            $.ajax({
                url: '/UserProfile/UpdateStaffProfilePic?id='+id,
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {
                    $("#staffprofilepic").attr("src","data:image/;base64,"+response);
                },
                error: function (er) {
                    alert("Failed To Upload Pic!!! Try Again");
                }
            });
        }
    }
//</script>

//<!------ Image Upload ------>

//<script>
    function AssignStaff(staffid) {
        $.ajax({
            url: '/UserProfile/GetParticularStaff?id='+staffid+'&&command=particular',
            type: "POST",
            datatype: "json",
            data: staffid,
            success: function (data) {
                if (data == 'unique') {
                    alert("Failed To Fetch User Details");
                }
                else {
                    $('#btnupdatestaff').show();
                    $('#btnaddstaff').hide();
                    $('#Item4_Staff_Id').val(staffid);
                    $('#Item4_First_Name').val(data.First_Name);
                    $('#Item4_Last_Name').val(data.Last_Name);
                    $('#Item4_Mobile_No').val(data.Mobile_No);
                    $('#Item4_Email').val(data.Email);
                    $('#Item4_Job_position').val(data.Job_position);
                    if(data.Vendor_Access > 0 || data.Customer_Access > 0)
                    {
                        $('#allowaccess').prop('checked', true);
                        $("#permissionsarea").css("visibility", "visible");
                    }
                    else {
                        $('#allowaccess').prop('checked', false);
                        $("#permissionsarea").css("visibility", "hidden");
                    }
                    if(data.Vendor_Access > 0 )
                    {
                        $('#Vendor_Access').prop('checked', true);
                    }
                    else {
                        $('#Vendor_Access').prop('checked', false);
                    }
                    if(data.Customer_Access > 0)
                    {
                        $('#Customer_Access').prop('checked', true);
                    }
                    else {
                        $('#Customer_Access').prop('checked', false);
                    }
                    if(data.UserPic != '' && data.UserPic !='/images/user.png')
                        $("#staffprofilepic").attr("src","data:image/;base64,"+data.UserPic);
                    else
                        $("#staffprofilepic").attr("src","/images/user.png");
                }
            },
            error: function (er) {
                alert("error");
            }
        });
    }
//</script>

//<script>
    function updatevales(val) {
        var id = location.search.split('id=')[1];//new URL(window.location.href).searchParams.get('id');
        var usermaster = {
            First_Name:$('#Item1_First_Name').val(),
            Last_Name:$('#Item1_Last_Name').val(),
            Password:$('#Item1_Password').val(),
            Date_Format:$('#Item1_Date_Format').val(),
            Timezone:$('#Item1_Timezone').val(),
            Currency:$('#Item1_Currency').val()
        }
        var userAddress={
            Line1:$('#Item2_Line1').val(),
            Line2:$('#Item2_Line2').val(),
            city:$('#Item2_city').val(),
            state:$('#Item2_state').val(),
            postalcode:$('#Item2_postalcode').val(),
            country:$('#Item2_country').val(),
        }
        var ownerCompanyAddress={
            Line1:$('#Item3_Line1').val(),
            Line2:$('#Item3_Line2').val(),
            city:$('#Item3_city').val(),
            state:$('#Item3_state').val(),
            postalcode:$('#Item3_postalcode').val(),
            country:$('#Item3_country').val(),
        }
        var staff={
            Staff_Id:$('#Item4_Staff_Id').val(),
            First_Name:$('#Item4_First_Name').val(),
            Last_Name:$('#Item4_Last_Name').val(),
            Mobile_No:$('#Item4_Mobile_No').val(),
            Email:$('#Item4_Email').val(),
            Job_position:$('#Item4_Job_position').val(),
            Vendor_Access:$('#Vendor_Access:checked').length,
            Customer_Access:$('#Customer_Access:checked').length,
            UserPic: $('#staffprofilepic').attr('src').replace('data:image/;base64,',''),
        }

        $.ajax({
            url: '/UserProfile/UpdateUserProfile?command='+val+'&&id='+id,
            type: "POST",
            datatype: "json",
            data: { userMaster : usermaster, userAddress : userAddress , ownerCompanyAddress : ownerCompanyAddress ,ownerStaff : staff },
            success: function (response) {
                if (response == 'success') {
                    var url='Login/ProfileProgressPartial';
                    $('#partialpage').load(url);
                    //$('#partialpage').load("@Url.Action("ProfileProgressPartial", "Login")");
                    $('#fname').html($('#Item1_First_Name').val());
                    $('#lname').html($('#Item1_Last_Name').val());
                    alert("Profile updated SuccessFully");
                }
                else if(response == 'staffadded')
                {
                    var url = 'UserProfile/GetStaffRecords?id='+id+'';
                    $('#partialdiv').empty().load(url,function(){ Pagination(); });
                    $('input[id^=Item4]').val('');
                    $("#staffprofilepic").attr("src","/images/user.png");
                    $("#Customer_Access").prop('checked', false);
                    $("#Vendor_Access").prop('checked', false);
                    $("#allowaccess").prop('checked', false);
                    $("#permissionsarea").css("visibility", "hidden");
                    alert("User Added SuccessFully!!!");
                }
                else if (response == 'staffupdated') {
                    var url = 'UserProfile/GetStaffRecords?id='+id+'';
                    $('#partialdiv').empty().load(url,function(){ Pagination(); });
                    $('input[id^=Item4]').val('');
                    $("#staffprofilepic").attr("src","/images/user.png");
                    $("#Customer_Access").prop('checked', false);
                    $("#Vendor_Access").prop('checked', false);
                    $("#allowaccess").prop('checked', false);
                    $("#permissionsarea").css("visibility", "hidden");
                    alert("User Updated SuccessFully!!!");
                    $('#btnupdatestaff').hide();
                    $('#btnaddstaff').show();
                }
                else {
                    alert("Failed To Update Profile");
                }
            },
            error: function (er) {
                alert("error");
            }
        });
    }
//</script>

//<script>
    //$(document).ready(function(){
    function Pagination() {
        //$('#contacttable').after('<div id="nav"></div>');
        $('#vendortable1').after('<div id="nav"></div>');
        var rowsShown = 3;
        var rowsTotal = $('#contacttable tbody tr').length;
        var numPages = rowsTotal / rowsShown;
        for (i = 0; i < numPages; i++) {
            var pageNum = i + 1;
            $('#nav').append('<a class="btn btn-success" rel="' + i + '">' + pageNum + '</a> ');
        }
        $('#contacttable tbody tr').hide();
        $('#contacttable tbody tr').slice(0, rowsShown).show();
        $('#nav a:first').addClass('active');
        $('#nav a').bind('click', function () {

            $('#nav a').removeClass('active');
            $(this).addClass('active');
            var currPage = $(this).attr('rel');
            var startItem = currPage * rowsShown;
            var endItem = startItem + rowsShown;
            $('#contacttable tbody tr').css('opacity', '0.0').hide().slice(startItem, endItem).
                    css('display', 'table-row').animate({ opacity: 1 }, 300);
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
    };

//});
//</script>

////<!------------ List / Grid Views and reload page -------------->
//<script>
    $("#grid-view").click(function(e) {
        $("#contacttable").css("display", "none");
        $("#vendortable1").css("display","block");
    });

$("#list-view").click(function(e) {
    $("#contacttable").css("display", "block");
    $("#vendortable1").css("display","none");
    //location.reload();
    var id = location.search.split('id=')[1];//new URL(window.location.href).searchParams.get('id');
    var url = 'UserProfile/GetStaffRecords?id=' + id + '';
    $('#partialdiv').empty().load(url, function () { Pagination(); });
});

$("#refresh").click(function (e) {
    var id = location.search.split('id=')[1];//new URL(window.location.href).searchParams.get('id');
    var url = 'UserProfile/GetStaffRecords?id='+id+'';
    $('#partialdiv').empty().load(url,function(){ Pagination(); });


    });
//</script>
////<!------------ List / Grid Views and reload page -------------->

//<script type="text/javascript">
function textval(){
    $(".display-positions .positions1").click(function(){
        var c = $(this).text();
        $(this).parent(".display-positions").prev("input.selected-position").val(c);
        $(this).parent(".display-positions").css("display","none");
    });
}

$(".positions1 > i").click(function(){
    $(this).parent(".positions1").remove();
});

$(".selected-position").click(function(){
    $(this).next(".display-positions").css("display","block");
    return textval();
});

$(".display-positions .position").click(function(){
    $(this).parent(".display-positions").next(".add-position").css("display","block");

    $(".add-position .add-button").click(function(){
        var v = $(this).prev("input[type='text']").val();
        var value = $("<div class='positions1'>" + "<i class='fa fa-trash-o pull-right' aria-hidden='true'></i>" + v + "</div>");
        alert($.unique(value));
        $(this).parents(".add-position").prev(".display-positions").append(value);
        $(".positions1 > i").click(function(){
            $(this).parent(".positions1").remove();
        });
        return textval();
    });

    $(".close-button").click(function(){
        $(this).parents(".add-position").css("display","none");
    });

});

//</script>
//}
