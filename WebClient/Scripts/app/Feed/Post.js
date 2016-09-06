var socializr = socializr || {};
socializr.feed = socializr.feed || {};
socializr.literals = socializr.literals || {};

$(function (){
	//Helper functions
	var html = socializr.literals.html;
	(function(out){
		var Post = function($postDispay) {
	    

	        //DOM Related Elements
	        this.$postDispay = $postDispay
	        // this.$commentInput = $postDispay.find("." + enums.html.commentInputClass);

	        //Data values
	        this.idPost = $postDispay.data("idpost");
	    };

	    var getParentPostDisplay = function (domElement) {
	    	return $(domElement).closest("." + html.post.postDisplayClass); 
	    }

	    Post.prototype.testMethod = function() {
	    	console.log("Ieeeei!");	
	    };

	    out.Post = Post;
	    out.getParentPostDisplay = getParentPostDisplay;
	    out.test = function() {
	    	console.log("here!");
	    }
	})(socializr.feed);
});