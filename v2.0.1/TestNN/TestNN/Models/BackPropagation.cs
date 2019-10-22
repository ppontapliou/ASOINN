using System.Linq;

namespace TestNN.Models
{
    class BackPropagation
    {
        public double A { get; set; } = 0.3;
        public double E { get; set; } = 0.7;

        public void Learn(NNModel network, double[][] inLayer)
        {

            for (int k = 0; k < 100000; k++)
                for (int i = 0; i < inLayer.Length; i += 2)
                {
                    var calcLayer = inLayer[i];//
                    network.Compute(calcLayer);

                    var outLayer = inLayer[i + 1];
                    Layer lastLayer = network.Layers.Last();
                    //gpu.For(0, len, j=> {
                    //    double output = lastLayer.Neurons[j].Value;
                    //    double param = outLayer[j];
                    //    lastLayer.Neurons[j].Value = (param - output) * ((1 - output) * output);
                    //});
                    //reculc error on last layer
                    for (int j = 0; j < lastLayer.Neurons.Count; j++)
                    {
                        double output = lastLayer.Neurons[j].Value;
                        double param = outLayer[j];
                        lastLayer.Neurons[j].Value = (param - output) * ((1 - output) * output);
                    }

                    //reculc other layer with synapses
                    for (int j = network.Layers.Count - 2; j >= 0; j--)
                    {
                        RecalcLayer(network.Layers[j]);//перещет весов для каждого уровня
                    }
                }
        }

        private void RecalcLayer(Layer layer)
        {
            for (int i = 0; i < layer.Neurons.Count; i++)
            {
                double oldValue = layer.Neurons[i].Value;
                layer.Neurons[i].Value = Delta(layer.Neurons[i]);

                layer.Neurons[i].OutputSynapses.ForEach(syn =>
                {
                    double fpart = RefreshNeuronWeithg(GRAD(oldValue, syn.OutputNeuron.Value), syn.DeltaValue);
                    syn.DeltaValue = fpart;
                    syn.Value += fpart;
                });

            }
        }
        private double Delta(Neuron neuron)
        {
            double sum = neuron.OutputSynapses.Sum(x => { return x.Value * x.OutputNeuron.Value; });
            double output = neuron.Value;
            return sum * ((1 - output) * output);
        }
        private double GRAD(double outNeuron, double delta)
        {
            return outNeuron * delta;
        }
        private double RefreshNeuronWeithg(double GRAD, double w)
        {
            return E * GRAD + A * w;
        }
    }
}
