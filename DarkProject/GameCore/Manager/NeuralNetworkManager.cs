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

        public static void SortNetworks(List<Enemy> enemies) =>
            SortNetworks(enemies.Select(x => x.brain).ToList());

        public static void SortNetworks(List<NeuralNetwork> brains)
        {
            var populationSize = brains.Count;
            brains.Sort();
            brains[^1].Save();
            for (int i = 0; i < populationSize / 2; i++)
            {
                brains[i] = brains[i + populationSize / 2].Copy();
                brains[i].Mutate((int)(1 / mutationChance), mutationStrength); 
            }
        }
     
    }
}
