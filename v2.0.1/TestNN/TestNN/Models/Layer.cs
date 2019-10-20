using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNN.Models
{
    [Serializable]
    class Layer
    {
        public List<Neuron> Neurons { get; set; }
        public Layer() { }
        public Layer(int countNeuron)
        {
            Neurons = new List<Neuron>();
            for(int i = 0; i < countNeuron; i++)
            {
                Neurons.Add(new Neuron());
            }
        }
        public Layer(double[] inputValue)
        {
            Neurons = new List<Neuron>();
            foreach(double val in inputValue)
            {
                Neurons.Add(new Neuron() { Value  = val });
            }
        }
    }
}
