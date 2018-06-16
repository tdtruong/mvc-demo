var user = {
    init: function () {
        user.loadProvince();
        user.registerEvents();
    },
    loadProvince: function () {
        var html = '';
        $.ajax({
            url: '/User/LoadProvince',
            dataType: 'json',
            type: 'POST',
            success: function (resp) {
                if (resp.success) {
                    var data = resp.data;
                    html += '<option value="">-- Select Province --</option>';
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
                    });
                    $('#ddlProvince').html(html);
                }
            }
        });
    },
    registerEvents: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id == '') {
                $('#ddlDistrict').html('');
            } else {
                var html = '';
                $.ajax({
                    url: '/User/LoadDistrict',
                    dataType: 'json',
                    type: 'POST',
                    data: { provinceId: parseInt(id) },
                    success: function (resp) {
                        if (resp.success) {
                            var data = resp.data;
                            $.each(data, function (i, item) {
                                html += '<option value="' + item.ID + '">' + item.Name + '</option>';
                            });
                            $('#ddlDistrict').html(html);
                        }
                    }
                });
            }
        });
    }
}
user.init();