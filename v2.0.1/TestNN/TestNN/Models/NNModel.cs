using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestNN.Models
{
    [Serializable]
    class NNModel
    {

        public List<Layer> Layers;
        public NNModel(int[] countLayers)
        {
            //инициализация всех слоев
            Layers = new List<Layer>();
            foreach (int count in countLayers)
            {
                Layers.Add(new Layer(count));
            }
            //установка Synapse для всех слоёв
            for (int i = 1; i < Layers.Count; i++)
            {
                foreach (var postNeuron in Layers[i].Neurons)
                {
                    foreach (var preNeuron in Layers[i - 1].Neurons)
                    {
                        Synapse synapse = new Synapse
                        {
                            InputNeuron = preNeuron,
                            OutputNeuron = postNeuron
                        };
                        postNeuron.InputSynapses.Add(synapse);
                        preNeuron.OutputSynapses.Add(synapse);
                    }
                }
            }
        }
        public Layer Compute(params double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Layers[0].Neurons[i].Value = input[i];
            }

            for (int i = 1; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];
                foreach (Neuron neuron in layer.Neurons)//беру нейрон
                {
                    neuron.SetValue = neuron.InputSynapses.Sum(x => { return x.InputNeuron.Value * x.Value; }) + 1;//плюс нейрон смещения
                }
            }
            //Layers.Last().Neurons.ForEach(x=>{
            //    Console.WriteLine( x.Value + "  ");
            //});
            return Layers.Last();
        }
        public static double[] ReculcArray(params double[] input)
        {
            double[] result = new double[input.Length];
            for(int i = 0; i< input.Length; i++)
            {
                result[i] = (input[i] - input.Min()) / (input.Max() - input.Min());
            }
            return result;
        }
        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream($"nnmodel.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, this);

                Console.WriteLine("Объект сериализован");
            }
        }
        public static NNModel Open(string name)
        {
            NNModel model;
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(name, FileMode.OpenOrCreate))
            {
                model = (NNModel)formatter.Deserialize(fs);
            }
            return model;
        }
    }
}
