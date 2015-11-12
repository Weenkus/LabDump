complete <- function(directory, id = 1:332) {
        # Get all the file names
        filenames <- list.files(path = directory, full.names = TRUE )

        # Count the number of not NA rows
        for(i in id) {
                dataFrame <- read.csv(filenames[i])
                nops <- c(length(which(!is.na(dataFrame))))
        }

        # Create the dataframe and fill it with values
        df <- data.frame(id, nops) 
        df
}