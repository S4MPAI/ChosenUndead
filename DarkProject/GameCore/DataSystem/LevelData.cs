using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class LevelData
    {
        public List<ChestData> Chests { get; set; }

        public List<NpcData> Npcs { get; set; }

        public LevelData() { }

        [JsonConstructor]
        public LevelData(List<ChestData> chests, List<NpcData> npcDatas)
        {
            Chests = chests;
            Npcs = npcDatas;
        }
    }

    public class NpcData
    {
        public float X {get; set; }

        public float Y { get; set; }

        public string Name { get; set; }

        public string[] Phrases { get; set; }

        public NpcData(float x, float y, string name, string[] phrases)
        {
            X = x;
            Y = y;
            Name = name;
            Phrases = phrases;
        }
    }

    public class ChestData
    {
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsOpen;

        public ChestData() { }

        public ChestData(float x, float y, bool isOpen)
        {
            X = x;
            Y = y;
            IsOpen = isOpen;
        }
    }
}
