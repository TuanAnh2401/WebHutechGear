$(function () {
    // Tạo kết nối tới SignalR Hub
    var chat = $.connection.chatHub;

    // Xử lý sự kiện nhận tin nhắn mới
    chat.client.receiveMessage = function (message, senderId) {
        // Thêm tin nhắn mới vào danh sách tin nhắn
        var messageList = $('#message-list');

        // Kiểm tra xem tin nhắn thuộc về người gửi hay người nhận
        if (senderId === $('#sender').val()) {
            var messageItem = $('<li>').addClass('message sent').text(message);
            messageList.append(messageItem);
        } else if (senderId === $('#receiver').val()) {
            var messageItem = $('<li>').addClass('message received').text(message);
            messageList.append(messageItem);
        }
        // Cuộn xuống cuối danh sách tin nhắn
        var chatHistory = $('#chat-history');
        chatHistory.scrollTop(chatHistory.prop("scrollHeight"));
    };

    // Khởi tạo kết nối SignalR
    $.connection.hub.start().done(function () {
        // Đặt thông tin đăng nhập của người gửi vào query string
        $.connection.hub.qs = { 'username': $('#sender').val() };

        // Xử lý sự kiện gửi tin nhắn mới
        $('#send-message-form').submit(function (event) {
            event.preventDefault();
            // Lấy nội dung tin nhắn từ form
            var messageInput = $('#message-input');
            var message = messageInput.val().trim();
            var receiver = $('#receiver').val();
            var sender = $('#sender').val();

            // Gửi tin nhắn cho người nhận
            chat.server.sendMessage(sender, receiver, message);

                // Reset input
                messageInput.val('');
        });
    });
});
