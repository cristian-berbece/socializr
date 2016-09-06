$(function () {
    console.log("FriendRequestNotification.js has fired!");

    var source = $("#friend-request-template").html();
    var template = Handlebars.compile(source);
    var friendMendu = $("#friend-request-menu");
    $.ajax({
        url: "/Socializr/Friends/GetRecentFriendRequests",
        type: 'GET',
        async: true,
        success: function (result) {
            result.forEach(function (element, index) {
                var html = template(element);
                $("#friend-request-menu").prepend(html);
            });
        }
    });
});