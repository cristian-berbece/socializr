var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};
$(function () {
    console.log("feedSystem.js");
    var html = socializr.literals.html;

    $("#" + html.feed.buttonLoadPostsId).on("click", function(){
        socializr.feed.FeedManager.loadNextPosts();
    });

});