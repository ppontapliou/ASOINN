using System;

namespace TestNN.Models
{
    [Serializable]
    class Synapse
    {
        public double Value { get; set; }
        public double DeltaValue { get; set; } 
        public Neuron InputNeuron { get; set; }
        public Neuron OutputNeuron { get; set; }
        public Synapse()
        {
            Value = Randomizer.RandomValue;
        }
    }
}
