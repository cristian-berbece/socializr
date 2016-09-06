var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};

$(function () {
	console.log("commentSystem.js");
	var Post = socializr.feed.Post;
	var CommentablePost = socializr.feed.CommentablePost;
    var html = socializr.literals.html;

    var $mainPostList = $("." + socializr.literals.html.post.postListClass);
    
    //Handlebars stuff
    var source = $("#display-comment-template").html();
    var commentTemplate = Handlebars.compile(source);
    var newComment = commentTemplate(
        {
            idAuthor: 1, 
            body:"Handlebars says hi!", 
            date: "123",
            authorName : "Berbece Cristian",
        });
    
    //Bind the submits on the addComment Form 
    $mainPostList.on("submit", "." + html.comment.formClass, function(event) {
    	event.preventDefault();
        var $parentDisplay = $(socializr.feed.getParentPostDisplay(event.target));
        var post = new CommentablePost($parentDisplay);
        post.submitNewComment();
    });

    $mainPostList.on("click", "." + html.comment.buttonLoadClass, function(event) {
        var $parentDisplay = $(socializr.feed.getParentPostDisplay(event.target));
        var post = new CommentablePost($parentDisplay);
        post.loadMoreComments();
    });

    $mainPostList.on("click", "." + html.comment.buttonHideClass, function(event) {
        var $parentDisplay = $(socializr.feed.getParentPostDisplay(event.target));
        var post = new CommentablePost($parentDisplay);
        post.hideComments();
    });

});