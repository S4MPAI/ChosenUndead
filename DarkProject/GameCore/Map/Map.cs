using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Map
    {
        private Tile[,] tiles;

        private List<Component> mapEntities;

        public List<Decoration> Decorations { get; private set; }

        public EntityManager entityManager;

        private int spawnpointNumber;

        public Player Player { get => Player.GetInstance(this); }

        public int TileSize { get; private set; }

        public Point MapSize;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public void Generate(StreamReader reader, int size, int spawnpointNumber)
        {
            mapEntities = new();
            TileSize = size;
            this.spawnpointNumber = spawnpointNumber;
            
            entityManager = new();
            

            var mapInfo = ReadMap(reader);
            MapSize = new(mapInfo.tiles[0].Length * size, mapInfo.tiles.Length * size);
            ConvertSymbolsToObjects(mapInfo.tiles, ConvertTiles, TileSize);
            ConvertSymbolsToObjects(mapInfo.entities, ConvertEntities, TileSize);
            ConvertSymbolsToObjects(mapInfo.decorations, ConvertDecorations, TileSize);

        }

        public static void SetLevelChanged(Action<LevelTransition> levelChanged)
        {
            LevelTransition.LevelChanged += levelChanged;
        }

        public static void SetSaveCompleted(Action<BonfireSave> saveMaked)
        {
            BonfireSave.PlayerSaved += saveMaked;
        }

        #region Loading

        private (string[][] tiles, string[][] entities, string[][] decorations) ReadMap(StreamReader reader)
        {
            var map = new List<string[][]>();
            for (int i = 0; i < 3; i++)
                map.Add(ReadToEmptyLine(reader).Select(x => x.Split(',').ToArray()).ToArray());

            Height = map[0].Length;
            Width = map[0][0].Length;
            tiles = new Tile[Height, Width];
            Decorations = new();


            return (map[0], map[1], map[2]);
        }

        private void ConvertSymbolsToObjects(string[][] map, Action<string, int, int, int> converter, int size)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    converter(map[y][x], x, y, size);
        }

        private List<string> ReadToEmptyLine(StreamReader reader)
        {
            var line = reader.ReadLine();
            var lines = new List<string>();

            while (line != null && line != String.Empty)
            {
                lines.Add(line);
                line = reader.ReadLine();
            }

            return lines;
        }

        private void ConvertTiles(string symbol, int x, int y, int size)
        {
            var number = int.Parse(symbol);
            var rectangle = new Rectangle(x * size, y * size, size, size);

            switch (number)
            {
                case <= 0:
                    tiles[y, x] = new Tile(number, rectangle, Collision.Passable);
                    break;
                case > 0:
                    tiles[y, x] = new Tile(number, rectangle, Collision.Impassable);
                    break;
            }
            mapEntities.Add(tiles[y, x]);
        }

        private void ConvertEntities(string symbol, int x, int y, int size)
        {
            Entity currentEntity;

            switch (symbol)
            {
                case "S":
                    currentEntity = entityManager.AddEnemy(this, EnemyType.Sceleton);
                    break;
                case "G":
                    currentEntity = entityManager.AddEnemy(this, EnemyType.Goblin);
                    break;
                default:
                    return;
                    break;
            }

            SetEntityPosition(currentEntity, x, y);
        }

        private void ConvertDecorations(string symbol, int x, int y, int size)
        {
            Decoration decoration = null;
            var rectangle = new Rectangle(x * size, y * size, size, size);

            if (int.TryParse(symbol, out var numberTransition) && numberTransition != 0)
            {
                decoration = new LevelTransition(rectangle, numberTransition);

                if (spawnpointNumber == numberTransition)
                    SetEntityPosition(Player, x, y);
            }
            else
            {
                switch (symbol)
                {
                    case "S":
                        decoration = new BonfireSave(Art.GetBonfireSaveAnimation(), rectangle);
                        break;
                    default:
                        return;
                        break;
                }
            }
            Decorations.Add(decoration);
            mapEntities.Add(decoration);
        }

        #endregion

        public void Update()
        {
            entityManager.Update();

            foreach (var decoration in Decorations)
                decoration.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in mapEntities)
                component.Draw(spriteBatch);

            entityManager.Draw(spriteBatch);
        }

        private void SetEntityPosition(Entity entity, int x, int y)
        {
            var pos = new Vector2(x * TileSize + TileSize / 2 - entity.TextureSize.X / 2, (y + 1) * TileSize - entity.TextureSize.Y);
            if (entity is Enemy enemy)
                enemy.SetStartPosition(pos);

            entity.Position = pos;
        }

        public bool IsHaveCollision(int x, int y) 
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return true;

            return tiles[y, x].Collision == Collision.Impassable;
        } 

        public Rectangle GetBounds(int x, int y) =>
            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);

        public void AddNPCs(params NPC[] npcs)
        {
            foreach (var npc in npcs)
                entityManager.AddNPC(npc);
        }

        public void SetChestsStates(List<ChestData> chestsData)
        {
            var chests = Decorations.OfType<Chest>();

            foreach (var chestData in chestsData)
            {
                var chest = chests.FirstOrDefault(x => x.Position.X == chestData.X && x.Position.Y == chestData.Y);
                if (chest != null)
                    chest.IsOpen = chestData.IsOpen;
            }
        }
    }
}
