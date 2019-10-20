using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2.Models
{
    class Layer
    {
        public Neuron[] Neurons { get; set; }
        public Layer(int countNeuron)
        {
            Neurons = new Neuron[countNeuron];            
            for(int i = 0; i < countNeuron; i++)
            {
                Neurons[i] = new Neuron();
            }
        }
        public Layer(float[] inputValue)
        {
            int lenght = inputValue.Length;
            for(int i = 0; i < lenght; i++)
            {
                Neurons[i].SetValue = inputValue[i];
            }
            
        }
    }
}
