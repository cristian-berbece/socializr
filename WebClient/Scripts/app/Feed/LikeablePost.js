 var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};

$(function () {

    (function (out) {
        var html = socializr.literals.html;
        var Post = socializr.feed.Post;
        var LikeablePost = function ($postDisplay) {
            //inheriting constructor body from Post 
            Post.call(this, $postDisplay);

            // DOM Related Elements
            this.$likeCountDisplay = $postDisplay.find("." + html.like.likeCountClass);
            this.$likeButton = $postDisplay.find("." + html.like.likeButtonClass);
        
            //Data Values
            this.isLikeButton = !!(this.$likeButton.hasClass("btn-like"));
	        this.likeCount = this.$likeCountDisplay.data("count");
        };

        /////////inheriting methods from Post
        LikeablePost.prototype = Object.create(Post.prototype);
        /////////

        
        LikeablePost.prototype.toggleLikeButtonType = function () {
            this.$likeButton.toggleClass("btn-like");
            this.$likeButton.toggleClass("btn-dislike");

            if (this.isLikeButton) {
                this.$likeButton.html("<span class='glyphicon glyphicon-thumbs-down'></span> Dislike");
                this.likeCount++;
            }
            else {
                this.$likeButton.html("<span class='glyphicon glyphicon-thumbs-up'></span> Like");
                this.likeCount--;
            }
            var bool = this.isLikeButton;
            this.isLikeButton = !bool;
            
            this.$likeCountDisplay.html(this.likeCount + " Likes");
            this.$likeCountDisplay.data("count",this.likeCount);
        };


        LikeablePost.prototype.submitLike = function () {
            var ajaxUrl = this.isLikeButton ? "/Socializr/Api/AddLike" : "/Socializr/Api/DeleteLike";
            var post = this;
            $.ajax({
                url: ajaxUrl,
                type: "POST",
                dataType: "json",
                async: true,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    idPost: post.idPost
                }),
                success: function () {
                    post.toggleLikeButtonType();
                }
            });
        }

        

        out.LikeablePost = LikeablePost;

    })(socializr.feed);
});