package neuralNet;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by weenkus on 12/7/15.
 */
public class Neuron {

    private List<Double> weights;
    private TransferFunction transferFunction;
    private double net;

    public Neuron() {
        this.weights = new ArrayList<Double>();
        this.transferFunction = TransferFunction.LINEAR;
    }

    public Neuron(List<Double> weights, TransferFunction transferFunction) {
        this.weights = new ArrayList<>(weights);
        this.transferFunction = transferFunction;
    }

    public List<Double> getWeights() {
        return weights;
    }

    public void setWeights(List<Double> weights) {
        this.weights = weights;
    }

    public TransferFunction getTransferFunction() {
        return transferFunction;
    }

    public void setTransferFunction(TransferFunction transferFunction) {
        this.transferFunction = transferFunction;
    }

    public String toString() {
        return "Transfer function: " + this.transferFunction + "\nWeights:\n" + this.weights.toString();
    }

    public void net(List<Double> input) {
        if (this.weights.size() != input.size() + 1) {
            System.err.println("Input and weight number don't match (" + this.weights.size() + " != " + (input.size() + 1) + ")");
        }
        // Calculate the sum
        double sum = 0.0;
        for (int i = 0; i < this.weights.size(); i++) {
            if (i == 0) {
                sum += 1 * this.weights.get(0);
            } else {
                // System.out.println(this.weights.size());
                sum += input.get(i - 1) * this.weights.get(i);
            }
        }

        // Use the transfer function
        if (this.transferFunction == TransferFunction.SIGMOIDAL) {
            sum = 1 / (1 + Math.pow(Math.E, -1 * sum));
        }
        this.net = sum;
    }

    public double getNet() {
        return this.net;
    }

    public void setNet(double n) {
        this.net = n;
    }

    public double calculateOutput(NeuronLayer netLayer) {
        double sum = 0.0;
        for (int i = 0; i < netLayer.numberOfNeurons() + 1; i++) {
            if (i == 0) {
                sum += 1 * this.weights.get(0);
            } else {
                sum += netLayer.getNeuron(i - 1).net * this.weights.get(i);
            }
        }
        return sum;
    }

    public int numberOfWeights() {
        return this.weights.size();
    }


}
