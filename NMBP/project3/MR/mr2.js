var map = function() {
	var words = this.content.split(" ")
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
	values.sort(function(a, b) {
		a = a[1];
		b = b[1];
		return a < b ? -1 : (a > b ? 1 : 0);
	});

	var out = ""
	for( var i = 0; i < 10; i++ ) {
		out = out + values[i] + " "
	}

	return out
};

var fin = function (key, values) {
	return values
};
