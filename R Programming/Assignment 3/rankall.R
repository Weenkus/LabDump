rankall <- function(outcome, num = "best") {
	## Read outcome data
	outcomeData <- read.csv("outcome-of-care-measures.csv")

	## Check that state and outcome are valid
	if(outcome == "heart attack")
		colNumber <- 11
	else if(outcome == "heart failure") 
		colNumber <- 17
	else if(outcome == "pneumonia") 
		colNumber <- 23
	else
		stop("invalid outcome")

	states <- outcomeData[, 7]
	if( any(is.element(states, state), na.rm = FALSE) == FALSE)
		stop("invalid state")
		
	## For each state, find the hospital of the given rank
	## Return a data frame with the hospital names and the
	## (abbreviated) state name
}