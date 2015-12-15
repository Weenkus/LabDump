package training;

import GA.Evolution;
import IOEngine.Parser;
import function.Function;
import neuralNet.*;

import java.io.IOException;
import java.text.ParseException;
import java.time.Duration;
import java.time.Instant;
import java.util.ArrayList;
import java.util.List;


/**
 * Created by weenkus on 12/7/15.
 */
public class Trainer {


    public static void main(String[] args) {

        // Load training set
        String fileName = "training-set.txt";
        List<Function> trainingSet = new ArrayList<Function>();
        try {
            Instant start = Instant.now();
            trainingSet = Parser.parseInput(fileName);
            Instant end = Instant.now();
            System.out.println(fileName + " loaded in " + Duration.between(start, end).toMillis() + " miliseconds (" + trainingSet.size()
                    + " samples)");
        } catch (NumberFormatException | ParseException | IOException e) {
            System.err.println("Couldn't open/find " + fileName);
            e.printStackTrace();
        }

        // Get all the needed values from the user
        int midLayerNeurons = Parser.askUserForIntegerNet();
        int populationNumber = Parser.askUserForIntegerGenetic();
        double mutationPercentage = Parser.askUserForDoubleMutataion();

        // Initialise the neural net population
        List<NeuralNet> population = new ArrayList<NeuralNet>();
        Instant start = Instant.now();
        for (int i = 0; i < populationNumber; i++) {
            NeuralNet newNet = new NeuralNet(midLayerNeurons);
            // newNet.printWeights();
            population.add(newNet);
        }
        Instant end = Instant.now();
        System.out.println("\nNueural net population created in " + Duration.between(start, end).toMillis()
                + " miliseconds (population number: " + populationNumber + ", mutatuion rate: " + mutationPercentage
                + ", neuron net architecture: 1 x " + midLayerNeurons + " x 1)");

        // Ask user how he wants to train
        NeuralNet masterNet = new NeuralNet(0);
        String input = Parser.askUserForDoubleEvolutionMethod();
        if (input.equals("iterative") || input.contains("i")) { // Iterative
            // Begin training iteration
            int iterationNumber = Parser.askUserForIntegerIteration();
            start = Instant.now();
            masterNet = Evolution.startWithIteration(population, iterationNumber, trainingSet, populationNumber, mutationPercentage);
            end = Instant.now();
            System.out.println("\nEvolution done in " + Duration.between(start, end).toMillis()
                    + " miliseconds (fitness of the master net: " + masterNet.getFitness() + ")");

        } else if (input.equals("error") || input.contains("er") || input.contains("or") || input.equals("e")) {
            // Begin training eror rate
            double error = Parser.askUserForIntegerEror();
            start = Instant.now();
            masterNet = Evolution.startWithError(population, error, trainingSet, populationNumber, mutationPercentage);
            end = Instant.now();
            System.out.println("\nEvolution done in " + Duration.between(start, end).toMillis()
                    + " miliseconds (fitness of the master net: " + masterNet.getFitness() + ")");
        } else {
            System.err.println("Wrong input.");
            System.exit(0);
        }

        // Load the test set
        fileName = "test-set.txt";
        List<Function> testSet = new ArrayList<Function>();
        try {
            start = Instant.now();
            testSet = Parser.parseInput(fileName);
            end = Instant.now();
            System.out.println("" + fileName + " loaded in " + Duration.between(start, end).toMillis() + " miliseconds ("
                    + trainingSet.size() + " samples)\n");
        } catch (NumberFormatException | ParseException | IOException e) {
            System.err.println("Couldn't open/find " + fileName);
            e.printStackTrace();
        }

        // Test the master net on the test set
        for (int i = 0; i < testSet.size(); i++) {
            double netOutput = masterNet.fire(testSet.get(i).getX());
            System.out.println("Output:     " + netOutput);
            System.out.println("Expected:   " + testSet.get(i).getY());
            System.out.println("Difference: " + Math.abs((netOutput - testSet.get(i).getY())) + "\n");
        }
    }
}
