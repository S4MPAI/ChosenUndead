using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChosenUndead
{
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
        public int[] layers { get; }

        private float[][] neurons;

        private float[][] biases;

        private float[][][] weights;

        public string name;

        private Random random;
        private const float maxValue = 0.5f;
        private const float minValue = -0.5f;

        public float fitness;

        public NeuralNetwork(int[] layers, string name)
        {
            this.name = name;

            this.layers = new int[layers.Length];
            random = new();

            for (int i = 0; i < layers.Length; i++)
                this.layers[i] = layers[i];

            InitNeurons();
            InitBiases();
            InitWeights();

            Load();
        }

        private float GetRandomWeight() => (float)random.NextDouble() * (maxValue - minValue) - minValue;

        private float GetRandomFloat(float min, float max) => (float)random.NextDouble() * (max - min) - min;

        private void InitNeurons() =>
            neurons = layers.Select(layer => new float[layer]).ToArray();

        private void InitBiases()
        {
            (var maxValue, var minValue) = (0.5f, -0.5f);

            biases = layers
                .Select(layer => new float[layer].Select(bias => GetRandomWeight()).ToArray())
                .ToArray();
        }
            
        private void InitWeights()
        {
            weights = new float[layers.Length - 1][][];

            for (int i = 1; i < layers.Length; i++)
            {
                var layerWeights = new float[neurons[i].Length][];
                var previousLayerNeurons = layers[i - 1]; 

                for (int j = 0; j < layers[i]; j++)
                {
                    var neuronWeights = new float[previousLayerNeurons];

                    for (int k = 0; k < previousLayerNeurons; k++)
                        neuronWeights[k] = GetRandomWeight();

                    layerWeights[j] = neuronWeights;
                }

                weights[i - 1] = layerWeights;
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
                neurons[0][i] = inputs[i];

            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < layers[i]; j++)
                {
                    var value = 0f;
                    for (int k = 0; k < layers[i - 1]; k++)
                        value += weights[i - 1][j][k] * neurons[i - 1][k];

                    neurons[i][j] = Activate(value + biases[i][j]);
                }
            }

            return neurons[neurons.Length - 1];
        }

        public float Activate(float value) => (float)Math.Tanh(value);

        public void Mutate(int chance, float val)
        {
            for (int i = 0; i < biases.Length; i++)
                for (int j = 0; j < biases[i].Length; j++)
                    biases[i][j] += (GetRandomFloat(0f, chance) <= 5) ? GetRandomFloat(-val, val) : 0;

            for (int i = 0; i < weights.Length; i++)
                for (int j = 0; j < weights[i].Length; j++)
                    for (int k = 0; k < weights[i][j].Length; k++)
                        weights[i][j][k] += (GetRandomFloat(0f, chance) <= 5) ? GetRandomFloat(-val, val) : 0;
        }

        public NeuralNetwork Copy()
        {
            var nn = new NeuralNetwork(layers, name);

            for (int i = 0; i < biases.Length; i++)
                for (int j = 0; j < biases[i].Length; j++)
                    nn.biases[i][j] = biases[i][j];

            for (int i = 0; i < weights.Length; i++)
                for (int j = 0; j < weights[i].Length; j++)
                    for (int k = 0; k < weights[i][j].Length; k++)
                        nn.weights[i][j][k] = weights[i][j][k];

            return nn;
        }
        public void Load()
        {
            var path = name + ".txt";
            if (!File.Exists(path)) return;

            var listLines = File.ReadAllLines(path);
            var index = 1;

            if (new FileInfo(path).Length > 0)
                for (int i = 0; i < biases.Length; i++)
                    for (int j = 0; j < biases[i].Length; j++)
                    {
                        biases[i][j] = float.Parse(listLines[index]);
                        index++;
                    }

                for (int i = 0; i < weights.Length; i++)
                    for (int j = 0; j < weights[i].Length; j++)
                        for (int k = 0; k < weights[i][j].Length; k++)
                        {
                            weights[i][j][k] = float.Parse(listLines[index]); ;
                            index++;
                        }
        }
        public void Save()
        {
            var path = name + ".txt";
            File.Create(path).Close();
            StreamWriter writer = new StreamWriter(path, true);

            for (int i = 0; i < biases.Length; i++)
                for (int j = 0; j < biases[i].Length; j++)
                    writer.WriteLine(biases[i][j]);

            for (int i = 0; i < weights.Length; i++)
                for (int j = 0; j < weights[i].Length; j++)
                    for (int k = 0; k < weights[i][j].Length; k++)
                        writer.WriteLine(weights[i][j][k]);
            
            writer.Close();
        }

        public int CompareTo(NeuralNetwork other)
        {
            if (other == null || fitness > other.fitness) return 1;

            if (fitness < other.fitness)
                return -1;

            return 0;
        }
    }
}
