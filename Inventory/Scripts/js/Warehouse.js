$(document).ready(function () {

    $('#emailid').focusout(function () {

        $('#emailid').filter(function () {
            var emil = $('#emailid').val();
            var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            if (!emailReg.test(emil)) {
                alert('Please enter valid email');
            } else {
                alert('Thank you for your valid email');
            }
        })
    });
});