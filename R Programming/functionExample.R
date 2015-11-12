add <- function(x, y) {
	x + y
}

aboveTen <- function(x) {
	use <- x > 10
	x[use]
}

aboveNumber <- function(aboveNum = 10, vector) {
	boolVector <- vector > aboveNum
	vector[boolVector]
}

columnMean <- function(x, removeNA = TRUE) {
	nc <- ncol(x)
	means <- numeric(nc)
	for(i in 1:nc) {
		means[i] <- mean(x[,i], na.rm = removeNA)
	}
	means	# Return a vector of means
}

cube <- function(x, n) {
	x^3
}

f <- function(x) {
        g <- function(y) {
                y + z
        }
        z <- 4
        x + g(x)
}