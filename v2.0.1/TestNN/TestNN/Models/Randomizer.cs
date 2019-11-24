using System;

namespace TestNN.Models
{
    static class Randomizer
    {
        private static Random rnd = new Random();
        public static double RandomValue
        {
            get => rnd.NextDouble() * 2 - 1;
        }

    }
}
