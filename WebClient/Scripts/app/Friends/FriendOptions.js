$(function () {
    console.log("FriendAction.js has fired!");

    var literals = {
        formFriendClass: "form-friend-options",
        userId: "data-user-id",
        containerClass: "container-friend-options",
        buttonClass: "btn-friend",
        buttonTypeClasses: {
            sendRequest: "send-friend-request",
            cancelRequest: "cancel-friend-request",
            acceptRequest: "accept-friend-request",
            rejectRequest: "reject-friend-request",
            cancelFriendship: "cancel-friend-relation",

        },
    };

    $("." + literals.containerClass).on('submit', "." + literals.formFriendClass, function (event) {
        onFriendFormSubmit(event);
    });

    $("." + literals.containerClass).on('click', "." + literals.buttonClass, function (event) {
        onButtonClick(event);
    })


    function onButtonClick(event) {
        var $container = $(event.target.closest("." + literals.containerClass));
        var idReceiver = $container.attr(literals.userId);

        if ($(event.target).hasClass("send-friend-request")) {
            sendFriendAction(idReceiver, globalInfo.ajaxUrl.sendFriendRequest);
        }

        if ($(event.target).hasClass("cancel-friend-request")) {
            sendFriendAction(idReceiver, globalInfo.ajaxUrl.cancelFriendRequest);
        }

        if ($(event.target).hasClass("accept-friend-request")) {
            sendFriendAction(idReceiver, globalInfo.ajaxUrl.acceptFriendRequest);
        }

        if ($(event.target).hasClass("reject-friend-request")) {
            sendFriendAction(idReceiver, globalInfo.ajaxUrl.rejectFriendRequest);
        }

        if ($(event.target).hasClass("cancel-friend-relation")) {
            sendFriendAction(idReceiver, globalInfo.ajaxUrl.cancelFriendRelation);
        }

        getFriendOptions(idReceiver, $container);
    }



    function getFriendOptions(idReceiver, container) {
        $.ajax({
            url: globalInfo.ajaxUrl.loadFriendOptions,
            type: "GET",
            async: true,
            data: {
                userId: idReceiver
            },
            success: function (result) {
                console.log(result);
                $(container).html(result);
            }
        });
    }

    function sendFriendAction(userId, ajaxUrl) {
        $.ajax({
            url: ajaxUrl,
            type: "POST",
            dataType: "json",
            async: true,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                userId: parseInt(userId, 10)
            }),
            success: function (result) {
                return true;
            },
            error: function (result) {
                return false;
            }
        });
    };



});



function User(email, name) {

}