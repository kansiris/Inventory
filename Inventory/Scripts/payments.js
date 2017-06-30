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
    //var previous = $("#previousamt").val();
    //var recevdamnt = $("#paidamt").val();
    //$("#currntamt").val(parseInt(previous) + sum);
    
}

function calculateamnt() {
    var invamt = $("#invoicedamt").val();
    var previous = $("#previousamt").val();
    var recevdamnt = $("#paidamt").val();
    $("#currntamt").val((parseInt(previous) - recevdamnt));
}

