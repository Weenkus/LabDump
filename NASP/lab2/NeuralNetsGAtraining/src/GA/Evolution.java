package GA;

import function.Function;
import neuralNet.NeuralNet;

import java.util.List;
import java.util.Random;
import java.util.LinkedList;


/**
 * Created by weenkus on 12/7/15.
 */
public class Evolution {

    public static NeuralNet startWithIteration(List<NeuralNet> population, int iterationNumber, List<Function> set, int popSize,
                                               double mutationRate) {
        // Generations
        for (int i = 0; i < iterationNumber; i++) {

            // Go through each net in the population and give them a fitness score
            double sumFitness = 0;
            double bestFitness = 100000;
            int bestNetindex = 0;
            double[] roundRobin = new double[popSize];
            for (int j = 0; j < popSize; j++) {

                // For each neuron go through the entire set
                for (int k = 0; k < set.size(); k++) {

                    // Test current input on current neuron within the population
                    population.get(j).setInput(set.get(k).getX());// set the input for the neuron net
                    double output = population.get(j).fire(set.get(k).getX());

                    // Edit the fitness level
                    double fitness = (output) - (set.get(k).getY());
                    population.get(j).setFitnessIncrement(Math.abs(fitness));
                }

                // Remember the best net
                double neuralNetFitnessAVG = population.get(j).getFitness() / set.size();
                if (neuralNetFitnessAVG < bestFitness) {
                    bestFitness = neuralNetFitnessAVG;
                    bestNetindex = j;
                }
                roundRobin[j] = 1/(neuralNetFitnessAVG+1);
                sumFitness += 1/(neuralNetFitnessAVG+1);
            }

            // Round robin
            List<NeuralNet> newPopulation = new LinkedList<NeuralNet>();
            for (int k = 0; k < popSize; k++) {
                roundRobin[k] = roundRobin[k] / sumFitness;
                //System.out.println(roundRobin[k]);
            }

            while (newPopulation.size() < popSize - 1) { // Leave room for the best on from the generation
                // Pick two nets with the round robin
                double firstNet = getRandomDouble(0.0, 1.0);
                int indexNet1 = 0, indexNet2 = 0;
                double robinSum = 0;
                for (int k = 0; k < popSize; k++) {
                    robinSum += roundRobin[k];
                    if (k > 0 && robinSum > firstNet && (robinSum - roundRobin[k]) < firstNet) {
                        indexNet1 = k;
                        break;
                    } else if (firstNet < roundRobin[0]) {
                        indexNet1 = 0;
                        break;
                    }
                }
                robinSum = 0;
                double secondNet = getRandomDouble(0.0, 1.0);
                for (int k = 0; k < popSize; k++) {
                    robinSum += roundRobin[k];
                    if (k > 0 && robinSum > secondNet && (robinSum - roundRobin[k]) < secondNet) {
                        indexNet2 = k;
                        break;
                    } else if (secondNet < roundRobin[0]) {
                        indexNet2 = 0;
                        break;
                    }
                    if( k == popSize - 1 && indexNet1 == indexNet2) {
                        secondNet = getRandomDouble(0.0, 1.0);
                        k = 0;
                    }
                }
                //System.err.println(indexNet1 + " , " + indexNet2);
                NeuralNet net1 = population.get(indexNet1);
                NeuralNet net2 = population.get(indexNet2);
                NeuralNet child = new NeuralNet(net1, net2);
                // Mutate and add the cshild to the new population
                child.mutate(mutationRate);
                newPopulation.add(child);
            }

            // The end return the best net
            if (i == (iterationNumber - 1)) {
                population.get(bestNetindex).setFitness(population.get(bestNetindex).getFitness() / set.size());
                return population.get(bestNetindex);
            }

            // Prepare the next generation
            newPopulation.add(population.get(bestNetindex)); // Add the best net
            population = newPopulation;

            // Clear fitness
            for (int j = 0; j < population.size(); j++) {
                population.get(j).setFitness(0); // Clear the fitness level
            }

            System.out.println((i + 1) + ". generation, average fitness: " + (sumFitness / popSize) + " (best fitness: " + bestFitness
                    + ")");

        }
        return population.get(0);
    }

    public static NeuralNet startWithError(List<NeuralNet> population, double error, List<Function> set, int popSize, double mutationRate) {
        // Generations
        for (int i = 0; 0 < 1; i++) {

            // Go through each net in the population and give them a fitness score
            double sumFitness = 0;
            double bestFitness = 1000000;
            int bestNetindex = 0;
            double[] roundRobin = new double[popSize];
            for (int j = 0; j < popSize; j++) {

                // For each neuron go through the entire set
                for (int k = 0; k < set.size(); k++) {

                    // Test current input on current neuron within the population
                    population.get(j).setInput(set.get(k).getX());// set the input for the neuron net
                    double output = population.get(j).fire(set.get(k).getX());

                    // Edit the fitness level
                    double fitness = (output) - (set.get(k).getY());
                    population.get(j).setFitnessIncrement(Math.abs(fitness));
                }

                // Remember the best net
                double neuralNetFitnessAVG = population.get(j).getFitness() / set.size();
                if (neuralNetFitnessAVG < bestFitness) {
                    bestFitness = neuralNetFitnessAVG;
                    bestNetindex = j;
                }
                roundRobin[j] = 1/(neuralNetFitnessAVG+1);
                sumFitness += 1/(neuralNetFitnessAVG+1);
            }

            // Round robin
            List<NeuralNet> newPopulation = new LinkedList<NeuralNet>();
            for (int k = 0; k < popSize; k++) {
                roundRobin[k] = roundRobin[k] / sumFitness;
                //System.out.println(roundRobin[k]);
            }

            while (newPopulation.size() < popSize - 1) { // Leave room for the best on from the generation
                // Pick two nets with the round robin
                double firstNet = getRandomDouble(0.0, 1.0);
                int indexNet1 = 0, indexNet2 = 0;
                double robinSum = 0;
                for (int k = 0; k < popSize; k++) {
                    robinSum += roundRobin[k];
                    if (k > 0 && robinSum > firstNet && (robinSum - roundRobin[k]) < firstNet) {
                        indexNet1 = k;
                        break;
                    } else if (firstNet < roundRobin[0]) {
                        indexNet1 = 0;
                        break;
                    }
                }
                robinSum = 0;
                double secondNet = getRandomDouble(0.0, 1.0);
                for (int k = 0; k < popSize; k++) {
                    robinSum += roundRobin[k];
                    if (k > 0 && robinSum > secondNet && (robinSum - roundRobin[k]) < secondNet) {
                        indexNet2 = k;
                        break;
                    } else if (secondNet < roundRobin[0]) {
                        indexNet2 = 0;
                        break;
                    }
                    if( k == popSize - 1 && indexNet1 == indexNet2) {
                        secondNet = getRandomDouble(0.0, 1.0);
                        k = 0;
                    }
                }
                //System.err.println(indexNet1 + " , " + indexNet2);
                NeuralNet net1 = population.get(indexNet1);
                NeuralNet net2 = population.get(indexNet2);
                NeuralNet child = new NeuralNet(net1, net2);
                // Mutate and add the child to the new population
                child.mutate(mutationRate);
                newPopulation.add(child);
            }

            // The end return the best net
            if (bestFitness < error) {
                population.get(bestNetindex).setFitness(population.get(bestNetindex).getFitness() / set.size());
                return population.get(bestNetindex);
            }

            // Prepare the next generation
            newPopulation.add(population.get(bestNetindex)); // Add the best net
            population = newPopulation;

            // Clear fitness
            for (int j = 0; j < population.size(); j++) {
                population.get(j).setFitness(0); // Clear the fitness level
            }

            System.out.println((i + 1) + ". generation, average fitness: " + (sumFitness / popSize) + " (best fitness: " + bestFitness
                    + ")");

        }
    }

    public static int getRandomInt(int min, int max) {
        Random random = new Random();
        return random.nextInt(max - min + 1) + min;
    }

    public static double getRandomDouble(double min, double max) {
        Random r = new Random();
        return min + (max - min) * r.nextDouble();
    }

}
