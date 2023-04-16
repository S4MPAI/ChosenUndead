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
        private CollisionTiles[,] _collisionTiles;

        public int TileSize { get; private set; }

        private List<CollisionTiles> _collisionTilesList { get; set; } = new();

        public int Width { get; private set; }

        public int Height { get; private set; }

        public void Generate(int[,] collisionTiles, int size)
        {
            _collisionTiles = new CollisionTiles[collisionTiles.GetLength(0), collisionTiles.GetLength(1)];

            for (int i = 0; i < collisionTiles.GetLength(0); i++)
                for (int j = 0; j < collisionTiles.GetLength(1); j++)
                {
                    var number = collisionTiles[i, j];

                    if (number > 0)
                    {
                        var tile = new CollisionTiles(number, new Rectangle(j * size, i * size, size, size));
                        _collisionTiles[i, j] = tile;
                        _collisionTilesList.Add(tile);
                    }
                        
                }

            Height = collisionTiles.GetLength(0);
            Width = collisionTiles.GetLength(1);
            TileSize = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in _collisionTilesList)
                tile.Draw(spriteBatch);
        }

        public bool IsHaveCollision(int x, int y) 
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return true;

            return _collisionTiles[y, x] != null;
        } 

        public Rectangle GetBounds(int x, int y) =>
            new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);
    }
}
