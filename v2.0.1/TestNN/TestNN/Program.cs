using Newtonsoft.Json;
using System;
using System.IO;
using TestNN.Models;

namespace TestNN
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Testing();
            }
            else
            {
                if (args.Length == 1)
                {
                    StartWithPath(args[0]);
                }
                if (args.Length > 3)
                {
                    NewNN(args);
                }
            }
        }

        static void Testing()
        {
            int[] array = { 2, 8, 4 };

            string str = File.ReadAllText("TD1.txt");
            double[][] vs1 = JsonConvert.DeserializeObject<double[][]>(str);
            NNModel model = new NNModel(array);
            //NNModel model = NNModel.Open("nnmodel.dat");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BackPropagation backPropagation = new BackPropagation();
            backPropagation.Learn(model, vs1);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
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
        static void StartWithPath(string path)
        {
            if (File.Exists(path))
            {
                //NNModel model = NNModel.Open(path);
            }
        }
        static void NewNN(string[] args)
        {
            int[] array = new int[args.Length - 1];
            for (int i = 1; i < args.Length; i++)
            {
                if (Int32.TryParse(args[i], out int res))
                {
                    array[i] = res;
                }
                else
                {
                    return;
                }
            }
            string str = File.ReadAllText(args[0]);
            double[][] vs1 = JsonConvert.DeserializeObject<double[][]>(str);

            NNModel model = new NNModel(array);
            BackPropagation backPropagation = new BackPropagation();
            backPropagation.Learn(model, vs1);

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
