// Hiển thị Partial_Chat khi nhấp vào .js-show-mess
$('.js-show-mess').on('click', function (e) {
    $('.js-panel-mess').removeClass('hide-header-mess').addClass('show-header-mess');
});
$('.js-hide-mess').on('click', function () {
    $('.wrap-header-mess').removeClass('show-header-mess').addClass('hide-header-mess');
});