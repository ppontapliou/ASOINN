using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2.Models
{
    class Synapse
    {
        public float Value { get; set; }
        public float OldValue { get; set; }
        public Synapse()
        {
            Value = (float)new Random().NextDouble();
        }
    }
}
