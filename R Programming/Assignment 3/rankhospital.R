rankhospital <- function(state, outcome, num) {
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

	## Filter data
	stateData <- subset(outcomeData, State == state, na.rm = TRUE)
	stateData <- stateData[order(-as.numeric(stateData[,colNumber])), ]

	if(num == "worst")
		hospitalName <- stateData[1, 2]
	else if(num == "best")
		hospitalName <- stateData[length(stateData[,2]), 2]
	else
		hospitalName <- stateData[num, 2]

	## Return hospital name in that state with the given rank 30-day death rate
	if( length(subset(outcomeData, State == state)) < num )
		hospitalName <- NA
	
	as.character(hospitalName)
}