using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2.Models
{
    class Neuron
    {
        public float Value { get; set; }
        public float SetValue
        {
            set
            {
                Value = FActivation(value);
            }
        }
        public Synapse[] InputSynapses { get; set; }
        public Neuron() { }
        private float FActivation(double x)
        {
            return (float)(1 / (1 + Math.Pow(Math.E, -x)));
        }
    }
}
