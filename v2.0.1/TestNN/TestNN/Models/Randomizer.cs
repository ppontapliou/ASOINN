using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNN.Models
{
    static class Randomizer
    {
        private static readonly Random rnd = new Random();
        public static double RandomValue
        {
            get => rnd.NextDouble() * 2 - 1;
        }

    }
}
