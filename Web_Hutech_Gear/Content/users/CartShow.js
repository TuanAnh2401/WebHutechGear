$('.js-show-cart').on('click', function () {
    $('.js-panel-cart').addClass('show-header-cart');
    $.ajax({
        url: '/ShoppingCart/Partial_Item_Cart', // URL của action method cần request
        type: 'GET', // Loại request (GET hoặc POST)
        success: function (result) {
            $('.show-header-cart').html(result); // Thay thế nội dung của div
        },
        error: function () {
            alert('Lỗi khi tải dữ liệu giỏ hàng');
        }
    });
});

$('.wrap-header-cart').on('click', function () {
    $('.js-panel-cart').removeClass('show-header-cart');
});