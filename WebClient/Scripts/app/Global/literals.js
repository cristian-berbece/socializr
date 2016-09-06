var socializr = socializr || {};
socializr.literals = socializr.literals || {};
(function (out) {
	var literals = {
		html : {

			feed: {
				buttonLoadPostsId : "btn-load-posts",
                postListClass : "post-list",
				handlebarsPostId : "display-post-template",
				LikeButtonTemplateId : "like-button-template",
				DisikeButtonTemplateId : "dislike-button-template",

			},

			post : {
			    postDisplayClass: "display-post",
                postListClass : "post-list",
                dataIdPost : "idpost",
			},

			like : {
				likeCountClass: "like-count",
            	likeButtonClass: "btn-set-like",
			},
			comment : {
				inputClass : "input-add-comment",
				listDisplayClass : "comments-display",
				pageNumberData : "pagenumber",
				formClass : "form-add-comment",
				buttonLoadClass : "btn-load-comments",
				buttonHideClass : "btn-hide-comments",
				handlebarsTemplateId : "display-comment-template",
			},
		},
	};
	socializr.literals = literals;
})(socializr.literals);

