using Newtonsoft.Json;
using nnv2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnv2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 2, 8, 4 };

            string str = File.ReadAllText("TD1.txt");
            float[][] vs1 = JsonConvert.DeserializeObject<float[][]>(str);
            NNModel model = new NNModel(array);


            BackPropagation backPropagation = new BackPropagation();
            backPropagation.Learn(model, vs1);

            for (int i = 0; i < vs1.Length; i += 2)
            {
                var inp = vs1[i];
                Array.ForEach(model.Compute(inp).Neurons, x =>
                {
                    Console.WriteLine(x.Value + "  ");
                });
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
