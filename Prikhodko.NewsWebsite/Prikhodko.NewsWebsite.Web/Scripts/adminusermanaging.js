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

$('.blockbutton').on('click', function (e) {
    e.preventDefault();
    if (this.value === "Block") {
        var currentbutton = this;
        $.ajax({
            url: '/users/block/' + this.id.replace('block ', ''),
            success: function () {
                currentbutton.value = "Unblock";
            }
        });
    } else {
        var currentbutton = this;
        $.ajax({
            url: '/users/unblock/' + this.id.replace('block ', ''),
            success: function () {
                currentbutton.value = "Block";
            }
        });
    }
})

$('.deletebutton').on('click', function (e) {
    e.preventDefault();
    var id = this.id.replace('delete ', '');
    $.ajax({
        url: '/users/delete/' + id,
        success: function () {
            $('#tablerow ' + id).remove();
        }
    });
})