var socializr = socializr || {};
socializr.feed = socializr.feed || {};

$(function () {
	console.log("LikeSystem.js");
	var Post = socializr.feed.Post;
	var LikeablePost = socializr.feed.LikeablePost;
    var $mainPostList = $("." + socializr.literals.html.post.postListClass);

    //Bind the click on the like button
    $mainPostList.on("click", "." + socializr.literals.html.like.likeButtonClass, function (event) {
    	var $parentDisplay = $(socializr.feed.getParentPostDisplay(event.target));
    	var post = new LikeablePost($parentDisplay);
    	post.submitLike();
        
        
    });
});