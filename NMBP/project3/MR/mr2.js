var map = function() {
	var words = this.content.split(/[\s,.!?]+/)
	var occurance = new Array()

	for( var i = 0; i < words.length; i++ ) {
    	occurance[ words[ i ].toLowerCase() ] = ( occurance[ words[ i ].toLowerCase() ] || 0 ) + 1
	}

	for( var key in occurance) {
		emit( this.author, key + " " + occurance[key])
	}
	
};

var reduce = function(key, values) {
	var max = 0
	for( var i = 0; i < values.length; i++ ) {
		var decomp = values[i].split(" ")
		if(decomp[1] > max)
			max = decomp[1]
	}

	var topWords = new Array()
	var insertedItems = 0
	var out = ""
	for(var i = max; i >= 0; i--) {
		for( var j = 0; j < values.length; j++ ) {
				var splitStr = values[j].split(" ")
				if(splitStr[1] == max) {
					out = out + values[j] + ", "
					insertedItems++
				}
				if(insertedItems == 10)
					break
		}
		if(insertedItems == 10)
			break
		max--
	}
	return out;
};

var fin = function (key, values) {
	return values
};



















var map = function() {
	var words = this.content.split(/[\s,.!?]+/)
	var occurance = new Array()

	var max = 0;
	for( var i = 0; i < words.length; i++ ) {
    	occurance[ words[ i ].toLowerCase() ] = ( occurance[ words[ i ].toLowerCase() ] || 0 ) + 1
		if(occurance[ words[ i ].toLowerCase() ] > max)
			max = occurance[ words[ i ].toLowerCase() ]
	}

	var topWords = new Array()
	var insertedItems = 0
	for(var i = max; i >= 0; i--) {
		for (var key in occurance) {
			if(occurance[key] == max) {
				topWords[insertedItems] = key + " : " + occurance[key];
				insertedItems++;	
				if(insertedItems == 10)
					break;	
			}
		}
		if(insertedItems == 10)
			break
		max--
	}

	emit( this.title, topWords)

};

var reduce = function(key, values) {
	var out = ""
	for( var i = 0; i < 10; i++ ) {
		out = out + values[i] + " "
	}

	return out
};

var fin = function (key, values) {
	return values
};
