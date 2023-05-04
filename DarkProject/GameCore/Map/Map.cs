using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
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

        private List<Entity> entities;

        private List<Decoration> decorations;

        public Player Player { get; private set; }

        public int TileSize { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public void Generate(Stream fileStream, int size)
        {
            mapEntities = new();
            TileSize = size;
            EntityManager.SetMap(this);
            
            using(var reader = new StreamReader(fileStream))
            {
                var mapInfo = ReadMap(reader);
                ConvertSymbolsToObjects(reader, mapInfo.tiles, ConvertTiles, TileSize);
                ConvertSymbolsToObjects(reader, mapInfo.entities, ConvertEntities, TileSize);
                //ConvertSymbolsToObjects(reader, mapInfo.decorations, ConvertDecorations, TileSize);
            }
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
            entities = new();
            decorations = new();


            return (map[0], map[1], map[2]);
        }

        private void ConvertSymbolsToObjects(StreamReader reader, string[][] map, Action<string, int, int, int> converter, int size)
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

            switch (number)
            {
                case <= 0:
                    tiles[y, x] = new Tile(number, new Rectangle(x * size, y * size, size, size), Collision.Passable);
                    break;
                case > 0:
                    tiles[y, x] = new Tile(number, new Rectangle(x * size, y * size, size, size), Collision.Impassable);
                    break;
            }
            mapEntities.Add(tiles[y, x]);
        }

        private void ConvertEntities(string symbol, int x, int y, int size)
        {
            switch (symbol)
            {
                case "P":
                    Player = EntityManager.GetPlayer();
                    entities.Add(Player);
                    break;
                default:
                    return;
                    break;
            }

            SetEntityPosition(entities[^1], x, y);
            mapEntities.Add(entities[^1]);
        }

        private void ConvertDecorations(string symbol, int x, int y, int size)
        {
            var number = int.Parse(symbol);

            switch (number)
            {
                case <= 0:
                    tiles[y, x] = new Tile(number, new Rectangle(x * size, y * size, size, size), Collision.Passable);
                    break;
                case > 0:
                    tiles[y, x] = new Tile(number, new Rectangle(x * size, y * size, size, size), Collision.Impassable);
                    break;
            }

            mapEntities.Add(tiles[y, x]);
        }

        #endregion

        public void Update(GameTime gameTime)
        {
            foreach (var entity in entities)
                entity.Update(gameTime);

            foreach (var decoration in decorations)
                decoration.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in mapEntities)
                component.Draw(gameTime, spriteBatch);
        }

        private void SetEntityPosition(Entity entity, int x, int y)
        {
            entity.Position = new Vector2(x * TileSize + TileSize / 2 - entity.Center.X, (y + 1) * TileSize - entity.Center.Y * 2);
        }

        public bool IsHaveCollision(int x, int y) 
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return true;

            return tiles[y, x].Collision == Collision.Impassable;
        } 

        public Rectangle GetBounds(int x, int y) =>
            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);

    }
}
