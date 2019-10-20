using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNN.Models
{
    [Serializable]
    class Synapse
    {
        public double Value { get; set; }
        public double DeltaValue { get; set; } = 0;
        public Neuron InputNeuron { get; set; }
        public Neuron OutputNeuron { get; set; }
        public Synapse()
        {
            Value = Randomizer.RandomValue;
        }
    }
}
