var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};


$(function(){
	(function(out){
		var html = socializr.literals.html;

		//intended to be a Singleton
		var FeedManager = function() {
			this.$postListDisplay = $("." + html.feed.postListClass);
			this.pageNumber = 1;
			this.likeButtonTemplate = $("#" + html.feed.LikeButtonTemplateId).html();
			this.dislikeButtonTemplate = $("#" + html.feed.DisikeButtonTemplateId).html();
		};

		FeedManager.prototype.getNewPosts = function() {
			var manager = this;
			var functionResult = {};
			var ajaxUrl = globalInfo.ajaxUrl.getFeedPosts;
			$.ajax({
		        url: ajaxUrl,
		        type: 'GET',
		        dataType: 'json',
		        async: true,
		        data: {
		            pageNumber : manager.pageNumber + 1,
		        },
		        success: function (result) {
		        	manager.pageNumber ++;
		        	manager.processNewPosts(result);
		        },
		        error: function (ceva) {
		        	console.log(ceva);
		        },
	    	});

	    	return functionResult;
		};

		FeedManager.prototype.appendPost = function(htmlContent) {
			this.$postListDisplay.append(htmlContent);
		};

		FeedManager.prototype.createPostHtml = function(jsonElement) {
		    var source = $("#" + html.feed.handlebarsPostId).html()
		    var template = Handlebars.compile(source);
		    var newHtml = template(jsonElement);
		    var a = $.parseHTML(newHtml);
		    a = $(a);
		    var isLiked = !!(a.find(".like-button-container").data("isliked")) ;
		    var likeButton = isLiked ? this.dislikeButtonTemplate : this.likeButtonTemplate;

		    a.find(".like-button-container").prepend(likeButton);
		    return a;
		};

		/**
		* Main public method, will be called from other file(probably feedSystem.js)
		*/
		FeedManager.prototype.loadNextPosts = function() {
			var newPosts = this.getNewPosts();
		};

		FeedManager.prototype.processNewPosts = function(resultArray) {
			var manager = this;
			resultArray.forEach( function(element) {
				manager.$postListDisplay.append(manager.createPostHtml(element));
				console.log(manager.$postListDisplay.find("." + html.post.postDisplayClass).last());
			});
		};

		/**I *strongly* believe this is a feasible way of making a singleton
		*/
		out.FeedManager = new FeedManager();

	})(socializr.feed);

});