package neuralNet;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by weenkus on 12/7/15.
 */
public class NeuronLayer {

    private List<Neuron> layerNeurons;

    public NeuronLayer(List<Neuron> layerNeurons) {
        this.layerNeurons = new ArrayList<Neuron>(layerNeurons);
    }

    public NeuronLayer() {
        this.layerNeurons = new ArrayList<>();
    }

    public void addNeuron(Neuron neuron) {
        this.layerNeurons.add(neuron);
    }

    public Neuron getNeuron(int index) {
        return this.layerNeurons.get(index);
    }

    public int numberOfNeurons() {
        return this.layerNeurons.size();
    }

    @Override
    public String toString() {
        return "NeuronLayer [layerNeurons=" + this.layerNeurons + "]";
    }

}
