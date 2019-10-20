using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNN.Models
{
    [Serializable]
    class Neuron
    {
        public double Value { get; set; }
        public List<Synapse> InputSynapses { get; set; }
        public List<Synapse> OutputSynapses { get; set; }
        public Neuron()
        {
            Value = Randomizer.RandomValue;
            InputSynapses = new List<Synapse>();
            OutputSynapses = new List<Synapse>();
        }
        private double FActivation(double x)
        {
            return 1 / (1+Math.Pow( Math.E,-x));
        }

        public double SetValue
        {
            set
            {
                Value = FActivation(value);
            }
        }
    }
}
