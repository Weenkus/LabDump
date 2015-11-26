best <- function(state, outcome) {
	## Read outcome data
	outcomeData <- read.csv("outcome-of-care-measures.csv")
	hospitals <- read.csv("hospital-data.csv")

	## Check that state and outcome are valid
	# Pneumonia = 23, Herat failure = 17, Heart attack = 11
	if(outcome == "heart attack")
		colNumber <- 11
	else if(outcome == "heart failure")
		colNumber <- 17
	else if(outcome == "pneumonia")
		colNumber <- 23
	else
		stop("invalid outcome")

	#if( is.element(subset(outcomeData, State == state), state) == FALSE)
		#stop("invalid state")

	# Filter the states
	stateSpecificData <- subset(outcomeData, State == state)

	# returns the index of the lowest value in the vector
	numericOutcome <- as.numeric(stateSpecificData[,colNumber])
	minValues <- which(numericOutcome == min(numericOutcome))

	hospitalNames <- stateSpecificData[minValues, 2]


	## Return hospital name in that state with lowset 30-day death rate
	as.character(sort(hospitalNames)[1])
}
