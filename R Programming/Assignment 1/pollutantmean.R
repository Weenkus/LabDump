pollutantmean <- function(directory, pollutant, id = 1:332) {
        # Get all the file names
        filenames <- list.files(path = directory, full.names = TRUE )

        # Create a vector of dataframes
        dataFrames <- lapply(filenames[id], read.csv)

        # Connect all the dara frames
        dataFrame <- Reduce(function(x, y) rbind(x, y), dataFrames)

        # Calculate the mean
        mean(dataFrame[,pollutant], na.rm = TRUE)
}
