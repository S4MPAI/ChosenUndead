using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChosenUndead
{
    public class Map
    {
        private Tile[,] tiles;

        public int TileSize { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public void Generate(int[,] tiles, int size)
        {
            this.tiles = new Tile[tiles.GetLength(0), tiles.GetLength(1)];

            for (int i = 0; i < tiles.GetLength(0); i++)
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    var number = tiles[i, j];

                    if (number > 0)
                        this.tiles[i, j] = new Tile(number, new Rectangle(j * size, i * size, size, size), Collision.Impassable);
                    else
                        this.tiles[i, j] = new Tile(0, new Rectangle(j * size, i * size, size, size), Collision.Passable);
                        
                }

            Height = tiles.GetLength(0);
            Width = tiles.GetLength(1);
            TileSize = size;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
                tile.Draw(gameTime, spriteBatch);
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
