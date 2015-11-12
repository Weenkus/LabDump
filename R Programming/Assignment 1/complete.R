complete <- function(directory, id = 1:332) {
        # Get all the file names
        filenames <- list.files(path = directory, full.names = TRUE )

        # Count the number of not NA rows
        nobs <- vector(mode="integer", length=0)
        for(i in id) {
                nobs <- c(nobs, nrow(na.omit(read.csv(filenames[i]))))
        }

        # Create the dataframe and fill it with values
        df <- data.frame(id, nobs) 
        df
}
