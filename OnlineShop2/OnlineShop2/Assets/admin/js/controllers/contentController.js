var content = {
    init: function () {
        content.registEvent();
    },
    registEvent: function () {
        $('.btn-active').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: '/Admin/Content/ChangeStatus',
                data: { id: id },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        btn.text('Active');
                    } else {
                        btn.text('Blocked');
                    }
                    $('#alertBox').removeClass('hide');
                    $('#alertBox').delay(1000).slideUp(500);
                }
            });
        });
    }
}
content.init();
