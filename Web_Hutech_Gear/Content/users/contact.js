$(document).ready(function () {
    $('#contactForm').submit(function (event) {
        event.preventDefault(); // Ngăn chặn form gửi đi theo cách thông thường

        var email = $('#Email').val();

        $.ajax({
            url: '@Url.Action("Partial_Contact", "Home")',
            type: 'POST',
            data: { email: email },
            success: function (response) {
                if (response.success) {
                    alert('Yêu cầu đã được gửi thành công!');
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Đã xảy ra lỗi. Vui lòng thử lại sau.');
            }
        });
    });
});