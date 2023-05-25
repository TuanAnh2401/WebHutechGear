$(function () {
    $('#checkout-form').submit(function (event) {
        event.preventDefault(); // prevent default form submission
        var formData = $(this).serialize();
        $.ajax({
            url: '/shoppingcart/checkout',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    window.location.href = response.url; // Chuyển hướng đến view
                } else {
                    alert(response.message); // Hiện thông báo lỗi nếu có lỗi xảy ra
                }
            },
            error: function () {
                alert('Có lỗi xảy ra trong quá trình xử lý!');
            }
        });
    });
});