var map = function() {
	if (typeof this.comments !== 'undefined') {
		emit( Number(1/(1+this.comments.length)) + "#" + this.title, this.title)
	} else {
		emit( Number(1/0) + "#" + this.title, this.title)
	}                     
};

var reduce = function(key, values) {
	return values
};

var fin = function (key, values) {
	return values
};
