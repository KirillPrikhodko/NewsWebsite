$(function() {
	var commentsHub = $.connection.commentsHub;
	commentsHub.hub.AddComment = function() {};
	commentsHub.client.addNewComment = function(username, userid, content, rating, commentid) {

	};

	$.connection.hub.start();
});
