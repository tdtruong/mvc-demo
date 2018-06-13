var contact = {
    init: function() {
        contact.regEvents();
    },
    regEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            // get contact information
            var name = $('#txtName').val();
            var phone = $('#txtPhone').val();
            var email = $('#txtEmail').val();
            var address = $('#txtAddress').val();
            var content = $('#txtContent').val();

            $.ajax({
                url: '/Contact/Send',
                type: 'POST',
                dataType: 'json',
                data: { name: name, phone: phone, email: email, address: address, content: content },
                success: function (response) {
                    if (response.success) {
                        alert("Thank you for sending feeback!");
                        contact.resetForm();
                    } else {
                        alert("Something went wrong!");
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#txtName').val('');
        $('#txtPhone').val('');
        $('#txtEmail').val('');
        $('#txtAddress').val('');
        $('#txtContent').val('');
    }
}
contact.init();