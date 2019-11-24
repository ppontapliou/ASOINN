using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TestNN.Models;

namespace TestNN
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                //Testing();
                TestingOpen();
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
        static void Testing1()
        {
            int[] array = { 9, 20, 1 };

            string str = File.ReadAllText("TD1.txt");
            double[][] vs1 = JsonConvert.DeserializeObject<double[][]>(str);
            for (int i = 0; i < vs1.Length; i += 2)
            {
                var tmp = vs1[i];
                vs1[i] = new double[9];
                for (int j = 0; j < 9; j++)
                {
                    vs1[i][j] = tmp[j] / tmp[j + 1];
                }
            }
            NNModel model = new NNModel(array);
            //NNModel model = NNModel.Open("nnmodel.dat");
            Console.Beep();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BackPropagation backPropagation = new BackPropagation();
            backPropagation.Learn(model, vs1);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            string str1 = File.ReadAllText("TD2.txt");
            double[][] vs2 = JsonConvert.DeserializeObject<double[][]>(str1);
            for (int i = 0; i < vs2.Length; i++)
            {
                var inp = new double[9];
                var tmp = vs2[i];
                for (int j = 0; j < 9; j++)
                {
                    inp[j] = tmp[j] / tmp[j + 1];
                }
                model.Compute(inp).Neurons.ForEach(x =>
                {
                    Console.WriteLine(x.Value + "  ");
                });
                Console.WriteLine();
            }
            Console.WriteLine("_______");
            //for (int i = 90; i < vs1.Length; i += 90)
            //{

            //    for (int j = 0; j < 3; j++)
            //    {
            //        var inp = new double[9];
            //        var tmp = vs1[i + j];
            //        for (int k = 0; k < 9; k++)
            //        {
            //            inp[k] = tmp[k] / tmp[k + 1];
            //        }

            //        model.Compute(inp).Neurons.ForEach(x =>
            //        {
            //            Console.WriteLine(x.Value + "  ");
            //        });
            //        Console.WriteLine();
            //    }
            //}            
            model.Save();
            Console.Beep();
            Console.ReadKey();
        }
        static void TestingOpen1()
        {
            int[] array = { 9, 20, 1 };

            NNModel model = NNModel.Open("nnmodel.dat");
            string str1 = File.ReadAllText("TD2.txt");
            double[][] vs2 = JsonConvert.DeserializeObject<double[][]>(str1);
            for (int i = 0; i < vs2.Length; i++)
            {
                var inp = new double[9];
                var tmp = vs2[i];
                for (int j = 0; j < 9; j++)
                {
                    inp[j] = tmp[j] / tmp[j + 1];
                }
                model.Compute(inp).Neurons.ForEach(x =>
                {
                    Console.WriteLine(x.Value + "  ");
                });
                Console.WriteLine();
            }
            Console.ReadKey();
            //model.Save();
        }
        static void Testing()
        {
            int[] array = { 10, 20, 2 };

            string str = File.ReadAllText("TD1.txt");
            double[][] vs1 = JsonConvert.DeserializeObject<double[][]>(str);
            NNModel model = new NNModel(array);
            Console.Beep();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            BackPropagation backPropagation = new BackPropagation();
            backPropagation.Learn(model, vs1);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            string str1 = File.ReadAllText("TD2.txt");
            double[][] vs2 = JsonConvert.DeserializeObject<double[][]>(str1);
            for (int i = 0; i < vs2.Length; i++)
            {
                model.Compute(NNModel.ReculcArray(vs2[i])).Neurons.ForEach(x =>
               {
                   Console.WriteLine(x.Value + "  ");
               });
                Console.WriteLine();
            }
            Console.WriteLine("_______");
            model.Save();
            Console.Beep();
            Console.ReadKey();
        }
        static void TestingOpen()
        {
            int[] array = { 9, 20, 1 };

            NNModel model = NNModel.Open("nnmodel.dat");
            string str1 = File.ReadAllText("TD2.txt");
            double[][] vs2 = JsonConvert.DeserializeObject<double[][]>(str1);
            for (int i = 0; i < vs2.Length; i++)
            {
                model.Compute(NNModel.ReculcArray(vs2[i])).Neurons.ForEach(x =>
               {
                   Console.WriteLine(x.Value + "  ");
               });
                Console.WriteLine();
            }
            Console.WriteLine("_______");
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
