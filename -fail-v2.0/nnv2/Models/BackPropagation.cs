using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2.Models
{
    class BackPropagation
    {
        public float A { get; set; } = 0.3F;
        public float E { get; set; } = 0.7F;
        public void Learn(NNModel network, float[][] inLayer)
        {
            for (int k = 0; k < 100000; k++)
                for (int i = 0; i < inLayer.Length; i += 2)
                {
                    var calcLayer = inLayer[i];//
                    network.Compute(calcLayer);

                    //float[] old = new float[inLayer[i + 1].Length];
                    for (int j = 0; j < inLayer[i + 1].Length; j++)
                    {
                        float output = network.Layers.Last().Neurons[j].Value;//old[j] =
                        float param = inLayer[i + 1][j];
                        network.Layers.Last().Neurons[j].Value = (param - output) * ((1 - output) * output);
                    }
                    for (int j = network.Layers.Length - 2; j >= 0; j--)
                    {
                        Layer calc = network.Layers[j];
                        Layer prev = network.Layers[j + 1];
                        for (int n = 0; n < calc.Neurons.Length; n++)
                        {
                            //float GRAD;
                            float oldVal = calc.Neurons[n].Value;
                            calc.Neurons[n].Value = prev.Neurons.Sum(neu => { return neu.Value * neu.InputSynapses[n].Value; }) * ((1 - oldVal) * oldVal);
                            //network.Layers[j + 1].Neurons
                            for (int m = 0; m < network.Layers[j + 1].Neurons.Length; m++)
                            {
                                float delta = oldVal * calc.Neurons[n].Value * E + A * prev.Neurons[m].InputSynapses[n].OldValue;
                                prev.Neurons[m].InputSynapses[n].OldValue = delta;
                                prev.Neurons[m].InputSynapses[n].Value += delta;
                            }
                            //calc.Neurons[n].Value;
                        }
                    }
                    //network.Layers.Last().Neurons.Length;
                }
        }

    }
}
