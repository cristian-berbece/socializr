$(function () {
    console.log("PostManagement/DeletePost.js has fired!");

    var literals = {
        FormDeletePost: {
            FormClass: "form-delete-post"
        },
        PostDisplay: {
            Class: "display-post-options",
            PostId: "data-post-id",
        }
    };

    $("." + literals.FormDeletePost.FormClass).submit(function (event) {
        event.preventDefault();
        if (confirm("Do you want to delete?") == true) {
            var $parent = $(event.target).closest("." + literals.PostDisplay.Class);
            deletePost($parent);
        }
       
    });

    function deletePost(postDisplay) {
        console.log(GetIdFromPostDisplay(postDisplay));
        $.ajax({
            url: globalInfo.ajaxUrl.deletePost,
            type: "POST",
            dataType: "json",
            async: true,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                idPost: GetIdFromPostDisplay(postDisplay),
            }),
            success: function (result) {
                postDisplay.html("");
            },
            error: function (result) {
                alert("error!");
            }
        });
    }

    function GetIdFromPostDisplay(postDisplay) {
        return parseInt(postDisplay.attr(literals.PostDisplay.PostId),10);
    }

});
