var socializr = socializr || {};
socializr.literals = socializr.literals || {};
(function (out) {
	var literals = {
		html : {
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
			},
		},
	};
	socializr.literals = literals;
})(socializr.literals);

