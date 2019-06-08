$('.writercheckbox').on('change', function (e) {
    e.preventDefault();
    if (this.checked) {
        $.ajax({
            url: '/users/addwriterpermissions/' + this.id.replace('writer ', ''),
        })
    } else {
        $.ajax({
            url: '/users/removewriterpermissions/' + this.id.replace('writer ', ''),
        })
    }
})

$('.admincheckbox').on('change', function (e) {
    e.preventDefault();
    if (this.checked) {
        $.ajax({
            url: '/users/addadminpermissions/' + this.id.replace('admin ', ''),
        })
    } else {
        $.ajax({
            url: '/users/removeadminpermissions/' + this.id.replace('admin ', ''),
        })
    }
})

$('.deletebutton').on('click', function (e) {
    e.preventDefault();
    var id = this.id.replace('delete ', '');
    $.ajax({
        url: '/users/delete/' + id,
        success: function () {
            var row = document.getElementById('tablerow ' + id);
	        row.parentNode.removeChild(row);
        }
    });
})

function myFunction() {
    // Declare variables 
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("searchusers");
    filter = input.value.toUpperCase();
    table = document.getElementById("users");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}