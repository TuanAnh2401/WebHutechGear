// Bắt sự kiện khi nhập giá trị vào input
$('body').on('input', 'input[name="quantity"]', function (e) {
    var val = parseInt($(this).val());
    $(this).focus();
    if (isNaN(val) || val < 1) {
        $(this).val(1); // Nếu giá trị nhập vào nhỏ hơn 1 thì cập nhật lại giá trị thành 1
    } else if (val > 99) {
        $(this).val(99); // Nếu giá trị nhập vào lớn hơn 99 thì cập nhật lại giá trị thành 99
    }
});