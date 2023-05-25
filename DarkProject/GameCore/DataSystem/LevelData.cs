using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChosenUndead.GameCore.DataSystem
{
    public class LevelData
    {
        private List<ScrollingBackground> Backgrounds;

        private Dictionary<Point, bool> chests;

        private List<NPC> entities;

        private static XmlSerializer serializer = new XmlSerializer(typeof(LevelData));

        public LevelData() { }

        public LevelData(List<ScrollingBackground> backgrounds, Dictionary<Point, bool> chests, List<NPC> entities)
        {
            Backgrounds = backgrounds;
            this.chests = chests;
            this.entities = entities;
        }

        public static void Serialize(string path, LevelData levelData)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, new Point(5, 4));
            }
        }

        public static LevelData Deserialize(string path)
        {
            Point b;

            using (var fs = new FileStream("Point.xml", FileMode.OpenOrCreate))
            {
                b = (Point)serializer.Deserialize(fs);
            }

            return null;
        }
    }
}
