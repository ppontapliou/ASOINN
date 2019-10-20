using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2.Models
{
    class NNModel
    {
        public Layer[] Layers { get; set; }
        public NNModel(int[] layersSize)
        {
            Layers = new Layer[layersSize.Length];
            for (int i = 0; i < layersSize.Length; i++)
            {
                Layers[i] = new Layer(layersSize[i]);
                //Layers[i].Neurons = new Neuron[layersSize[i]];
            }
            for (int i = 1; i < layersSize.Length; i++)
            {
                for (int j = 0; j < Layers[i].Neurons.Length; j++)
                {
                    Layers[i].Neurons[j].InputSynapses = new Synapse[layersSize[i - 1]];
                    for (int k = 0; k < layersSize[i - 1]; k++)
                    {
                        Layers[i].Neurons[j].InputSynapses[k] = new Synapse();
                    }
                }
            }
        }
        public Layer Compute(params float[] inputs)
        {
            for (int i = 0, lenght = Layers[0].Neurons.Length; i < lenght; i++)
            {
                Layers[0].Neurons[i].Value = inputs[i];
            }
            for (int i = 1, lenght = Layers.Length; i < lenght; i++)
            {
                var currentL = Layers[i];
                var preL = Layers[i-1];
                for (int j = 0; j < currentL.Neurons.Length; j++)
                {
                    float summ = 0;
                    for (int k = 0; k < preL.Neurons.Length; k++)
                    {
                        summ += currentL.Neurons[j].InputSynapses[k].Value * preL.Neurons[k].Value;
                    }
                    Layers[i].Neurons[j].SetValue = summ + 1;
                }
            }
            return this.Layers.Last();
        }
    }
}
