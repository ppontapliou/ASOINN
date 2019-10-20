using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TestNN.Models;

namespace TestNN
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 2, 8, 4 };
            
            string str = File.ReadAllText("TD1.txt");
            double[][] vs1 = JsonConvert.DeserializeObject<double[][]>(str);
            //NNModel model = new NNModel(array);   
            NNModel model = NNModel.Open("nnmodel.dat");
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            //BackPropagation backPropagation = new BackPropagation();
            //backPropagation.Learn(model, vs1);
            //watch.Stop();
            //Console.WriteLine(watch.ElapsedMilliseconds);
            for (int i = 0; i < vs1.Length; i += 2)
            {
                var inp = vs1[i];
                model.Compute(inp).Neurons.ForEach(x =>
                {
                    Console.WriteLine(x.Value + "  ");
                });
                Console.WriteLine();
            }            
            Console.ReadKey();
        }
    }
}
