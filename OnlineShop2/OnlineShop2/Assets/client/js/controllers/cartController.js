var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = '/';
        });

        $('#btnUpdateCart').off('click').on('click', function () {
            var listItem = $('.txtQuantity');
            var cartList = [];
            $.each(listItem, function (i, item) {
                cartList.push({
                    Quantity: $(this).val(),
                    Product: {
                        ID: $(this).data('id')
                    }
                });
            });

            $.ajax({
                url: 'Cart/Update',
                dataType: 'json',
                type: 'POST',
                data: { cartModel: JSON.stringify(cartList) },
                success: function (resp) {
                    if (resp.success) {
                        window.location.href = '/cart';
                    }
                }
            });
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: 'Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (resp) {
                    if (resp.success) {
                        window.location.href = '/cart';
                    }
                }
            });
        });

        $('.btn-delete').off('click').on('click', function () {
            $.ajax({
                url: 'Cart/Delete',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (resp) {
                    if (resp.success) {
                        window.location.href = '/cart';
                    }
                }
            });
        });

        $('#btnPayment').off('click').on('click', function () {
            window.location.href = '/payment';
        });
    }
};
cart.init();