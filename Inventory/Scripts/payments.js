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
    $('#paidamt').val(sum);
}

