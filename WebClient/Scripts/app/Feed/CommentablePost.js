var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};

$(function() {

	(function (out){
		var html = socializr.literals.html;
		var Post = socializr.feed.Post;
		var CommentablePost = function ($postDisplay) {
			//inheriting constructor from Post
			Post.call(this,$postDisplay);

			//DOM Elements
			this.$commentInput = $postDisplay.find("." + html.comment.inputClass);
			this.$commentListDisplay = $postDisplay.find("." + html.comment.listDisplayClass);

			//Data fields
			this.commentPageNumber = this.$commentListDisplay.data(html.comment.pageNumberData);

            //Comment Handlebar template
            var templateSource = $("#" + html.comment.handlebarsTemplateId).html();
            this.commentTemplate = Handlebars.compile(templateSource);
		};

		//inheriting methods from Post prototype
		CommentablePost.prototype = Object.create(Post.prototype);

        CommentablePost.prototype.submitNewComment = function () {
            var ajaxUrl = globalInfo.ajaxUrl.addComment;
            commentBody = this.$commentInput.val();
            var post = this;
            $.ajax({
                url: ajaxUrl,
                type: "POST",
                dataType: "json",
                async: true,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    Body: commentBody,
                    IdPost: post.idPost
                }),
                success: function (result) {
                    //To do : display the newly added Comment
                    post.$commentInput.val("");
                    post.displayLatestComment();
                },
                error: function (result) {
                    console.log("Error!");
                }
            });
        };

        CommentablePost.prototype.displayLatestComment = function() {
        	var ajaxUrl = globalInfo.ajaxUrl.getLatestComment;
        	var post = this;
        	$.ajax({
	            url: ajaxUrl,
	            type: 'GET',
	            dataType: 'json',
	            async: true,
	            data: {
	                postId: post.idPost,
	            },
	            success: function (result) {
	            	post.appendCommentAtBottom(result);
	            },
        	});
        };

        CommentablePost.prototype.loadMoreComments = function() {   
        	var ajaxUrl = globalInfo.ajaxUrl.getComments;
        	var post = this;
        	$.ajax({
	            url: ajaxUrl,
	            type: 'GET',
	            dataType: 'json',
	            async: true,
	            data: {
	                postId: post.idPost,
	                pageNumber: post.commentPageNumber + 1,
	            },
	            success: function (result) {
	            	result.forEach(function(v,k){
	            		post.appendCommentAtTop(v);
	            	});
	            	post.setPageNumber(post.commentPageNumber + 1);
	            }
        	});
        };

        CommentablePost.prototype.hideComments = function(value) {
        	this.$commentListDisplay.html("");
        	this.setPageNumber(0);
        }

        CommentablePost.prototype.setPageNumber = function(value){
        	this.$commentListDisplay.data(html.comment.pageNumberData, value);
        };

        CommentablePost.prototype.appendCommentAtTop = function(comment) {
        	var source = $("#display-comment-template").html();
   			var commentTemplate = Handlebars.compile(source);
        	var newHtml = commentTemplate(comment);
        	this.$commentListDisplay.prepend(newHtml);
        };

        CommentablePost.prototype.appendCommentAtBottom = function(comment) {
            var source = $("#display-comment-template").html();
            var commentTemplate = Handlebars.compile(source);
            var newHtml = commentTemplate(comment);
            this.$commentListDisplay.append(newHtml);
        };
		out.CommentablePost = CommentablePost;
	})(socializr.feed);
});