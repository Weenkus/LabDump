package neuralNet;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;


/**
 * Created by weenkus on 12/7/15.
 */
public class NeuralNet {

    private double input;
    private Neuron output;
    private NeuronLayer middleLayer;
    private double fitness = 0.0;

    public NeuralNet(double input, Neuron output, NeuronLayer middleLayer) {
        super();
        this.input = input;
        this.output = output;
        this.middleLayer = middleLayer;
    }

    public NeuralNet(int midLayerNeuronNumber) {
        this.middleLayer = new NeuronLayer();
        int rangeMin = -10, rangeMax = 10;

        // Generate random starting weights for input Neuron
        Random r = new Random();
        this.input = 0.0;

        // Generate random starting weights for output Neuron
        List<Double> weightsOutput = new ArrayList<Double>();
        for (int i = 0; i < midLayerNeuronNumber ; i++) {
            weightsOutput.add(rangeMin + (rangeMax - rangeMin) * r.nextDouble()); // one extra for w0
        }
        weightsOutput.add(0.0);
        // weightsOutput.add(rangeMin + (rangeMax - rangeMin) * r.nextDouble());
        this.output = new Neuron(weightsOutput, TransferFunction.LINEAR);

        // Generate random midLayer Weights
        for (int i = 0; i < midLayerNeuronNumber; i++) {
            List<Double> weightsMid = new ArrayList<Double>();
            weightsMid.add(rangeMin + (rangeMax - rangeMin) * r.nextDouble());
            weightsMid.add(rangeMin + (rangeMax - rangeMin) * r.nextDouble());
            this.middleLayer.addNeuron(new Neuron(weightsMid, TransferFunction.SIGMOIDAL));
        }
    }

    /**
     * Create a child in the evolution algorithm
     *
     * @param net1
     *            first parent
     * @param net2
     *            second parent
     */
    public NeuralNet(NeuralNet net1, NeuralNet net2) {
        this.middleLayer = new NeuronLayer();
        Random r = new Random();
        // Go thorugh each neuron
        for (int i = 0; i < net1.middleLayer.numberOfNeurons(); i++) {
            List<Double> crossList = new ArrayList<Double>();
            // Cross all the weights (from parents) for each neuron
            for (int j = 0; j < net1.middleLayer.getNeuron(i).numberOfWeights(); j++) {
                double rangeMin = net1.middleLayer.getNeuron(i).getWeights().get(j);
                double rangeMax = net2.middleLayer.getNeuron(i).getWeights().get(j);
                double newWeight = rangeMin + (rangeMax - rangeMin) * r.nextDouble();
                crossList.add(newWeight);
            }
            this.middleLayer.addNeuron(new Neuron(crossList, TransferFunction.SIGMOIDAL));
        }

        // Do the output neuron
        List<Double> crossOutput = new ArrayList<Double>();
        // System.out.println("net1.size = " + net1.output.numberOfWeights());
        for (int i = 0; i < net1.output.numberOfWeights()-1; i++) {
            double rangeMin = net1.output.getWeights().get(i);
            double rangeMax = net2.output.getWeights().get(i);
            double newWeight = rangeMin + (rangeMax - rangeMin) * r.nextDouble();
            crossOutput.add(newWeight);
        }
        crossOutput.add(0.0);
        this.output = new Neuron(crossOutput, TransferFunction.LINEAR);
        this.fitness = 0.0;
    }

    public double fire(double input) {
        // Middle layer
        List<Double> inputList = new ArrayList<>();
        inputList.add(input);
        for (int i = 0; i < this.middleLayer.numberOfNeurons(); i++) {
            this.middleLayer.getNeuron(i).net(inputList);
        }

        // Clear net values
        //this.clearNet();
        return this.output.calculateOutput(this.middleLayer);
    }

    public double getInput() {
        return input;
    }

    public void setInput(double input) {
        this.input = input;
    }

    public Neuron getOutput() {
        return output;
    }

    public void setOutput(Neuron output) {
        this.output = output;
    }

    public NeuronLayer getMiddleLayer() {
        return middleLayer;
    }

    public void setMiddleLayer(NeuronLayer middleLayer) {
        this.middleLayer = middleLayer;
    }

    public double getFitness() {
        return fitness;
    }

    public void setFitnessIncrement(double fitness) {
        this.fitness += fitness;
    }

    public void setFitness(double fitness) {
        this.fitness = fitness;
    }


    public void mutate(double K) {
        Random r = new Random();
        double min = 0;
        double p = (this.middleLayer.numberOfNeurons() + 2) / (double) this.getNumberOfWeights();
        // System.err.println((this.middleLayer.numberOfNeurons() + 2) + " / " + this.getNumberOfWeights());

        // Mutate the weights in the mid layer
        for (int i = 0; i < this.middleLayer.numberOfNeurons(); i++) {
            List<Double> mutationList = this.middleLayer.getNeuron(i).getWeights();
            for (int j = 0; j < this.middleLayer.getNeuron(i).numberOfWeights(); j++) {
                double percent = min + (100 - min) * r.nextDouble();
                // Mutate only the p percent
                if (percent <= p) {
                    // System.err.println("MUTATION");
                    Double weight = mutationList.get(j);
                    double rndMutation = min + (K - min) * r.nextGaussian();
                    weight += rndMutation;
                    mutationList.set(j, weight);
                }
            }
            // Return the mutationed wegiths
            this.middleLayer.getNeuron(i).setWeights(mutationList);
        }

        // Mutate the weights of the output neuron
        List<Double> outputWeights = this.output.getWeights();
        for (int i = 0; i < this.output.numberOfWeights()-1; i++) {
            double percent = min + (100 - min) * r.nextDouble();
            if (percent <= p) {
                Double oweight = outputWeights.get(i);
                double rndMutation = min + (K - min) * r.nextDouble();
                oweight += rndMutation;
                outputWeights.set(i, oweight);
            }
        }
        this.output.setWeights(outputWeights);
    }

    public int getNumberOfWeights() {
        return (this.middleLayer.getNeuron(0).numberOfWeights() * this.middleLayer.numberOfNeurons()) + this.output.numberOfWeights();
    }

    public void printWeights() {

        // for (int i = 0; i < this.middleLayer.numberOfNeurons(); i++) {
        System.out.println("MID: " + this.middleLayer.getNeuron(0).numberOfWeights());
        // }
        System.out.println("OUTPUT: " + this.output.numberOfWeights());
    }

    public void printStrucute() {
        System.out.println("1 x " + this.middleLayer.numberOfNeurons() + " x 1");
    }

    public void clearNet() {
        for(int i = 0; i < this.middleLayer.numberOfNeurons(); i++) {
            this.middleLayer.getNeuron(i).setNet(0.0);;
        }
        this.output.setNet(0.0);
    }

}
