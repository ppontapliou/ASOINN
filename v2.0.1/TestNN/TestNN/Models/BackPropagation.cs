using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestNN.Models
{
    class BackPropagation
    {
        public double A { get; set; } = 0.3;
        public double E { get; set; } = 0.7;
        public bool RebuildInputData { get; set; } = true;

        public void Learn(NNModel network, double[][] inLayer)
        {
            if (RebuildInputData)
            {
                for (int i = 0; i < inLayer.Length; i += 2)
                {
                    var tmp = new double[inLayer[i].Length];
                    Array.Copy(inLayer[i], tmp, inLayer[i].Length);
                    for (int j = 0; j < 10; j++)
                    {//нормализация
                        //inLayer[i][j] = (inLayer[i][j] - tmp.Min()) / (tmp.Max() - tmp.Min());
                        inLayer[i][j] = 1 / (Math.Pow(Math.E, (inLayer[i][j]- (tmp.Max() - tmp.Min())/2)*-0.49) +1);
                    }
                }
            }
            for (int k = 0; k < 10000; k++)
            {
                // System.Console.WriteLine(k);
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
                        //Task[] task = new Task[];
                        RecalcLayer(network.Layers[j]);//перещет весов для каждого уровня

                    }
                }
            }
        }

        private void RecalcLayer(Layer layer)
        {
            Task[] taskNeurons = new Task[layer.Neurons.Count];
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
        private Task CalcTask(Neuron neuron)
        {
            return Task.Run(() =>
            {
                double oldValue = neuron.Value;
                neuron.Value = Delta(neuron);
                neuron.OutputSynapses.ForEach(syn =>
                {
                    double fpart = RefreshNeuronWeithg(GRAD(oldValue, syn.OutputNeuron.Value), syn.DeltaValue);
                    syn.DeltaValue = fpart;
                    syn.Value += fpart;
                });
            });
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
