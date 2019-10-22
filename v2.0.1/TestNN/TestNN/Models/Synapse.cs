using System;

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
