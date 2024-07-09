using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game_Dev_Practice
{
    internal class Chunk
    {
        private GameObject[,] tiles;
        private int rowCount;
        private int columnCount;
        private Vector2 position;

        public Chunk(GameObject[,] tiles)
        {
            this.tiles = tiles;
            this.rowCount = tiles.GetLength(0);
            this.columnCount = tiles.GetLength(1);
            this.position = tiles[0, 0].Position;
        }

        public void DrawChunk(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (tiles[i,j] != null)
                    {
                        tiles[i, j].Draw(_spriteBatch, _graphics);
                    }
                }
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }

        public GameObject[,] Tiles
        {
            get
            {
                return this.tiles;
            }
        }

        public int RowCount
        {
            get
            {
                return this.rowCount;
            }
        }

        public int ColumnCount
        {
            get
            {
                return this.columnCount;
            }
        }
    }
}
