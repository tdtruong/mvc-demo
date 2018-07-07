﻿var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (resp) {
                        response(resp.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
        .autocomplete("ui-autocomplete")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<a>" + item.label + "</a>")
              .appendTo(ul);
        };
    }
}
common.init();