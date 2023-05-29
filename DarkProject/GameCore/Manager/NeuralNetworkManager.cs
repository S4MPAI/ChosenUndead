using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public static class NeuralNetworkManager
    {
        private const float mutationChance = 0.01f;

        private const float mutationStrength = 0.5f;

        public static NeuralNetwork GetGoblinNeuralNetwork() => new NeuralNetwork(new int[] { 4, 3, 2 }, "goblinNN");

        public static NeuralNetwork GetSceletonNeuralNetwork() => new NeuralNetwork(new int[] { 4, 4, 3 }, "sceletonNN");

        public static List<NeuralNetwork> SortNetworks(List<Enemy> enemies) =>
            SortNetworks(enemies.Select(x => x.brain).ToList());

        public static List<NeuralNetwork> SortNetworks(List<NeuralNetwork> brains)
        {
            var populationSize = brains.Count;

            brains.Sort();

            brains[^1].Save();
            for (int i = 0; i < populationSize; i++)
            {
                brains[i] = brains[i].Copy();
                brains[i].Mutate(mutationChance * ((60 - i)/10), mutationStrength * ((60 - i) / 10)); 
            }

            return brains;
        }
     
    }
}
