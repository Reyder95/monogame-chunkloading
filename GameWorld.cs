using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game_Dev_Practice
{
    internal class GameWorld
    {
        private GameObject[,] tiles;
        private List<Chunk> chunks;
        private Dictionary<GridKey, Chunk> chunkGrid;
        private int rowCount;
        private int columnCount;
        private float tileSize = 256.0f;

        public GameWorld(int rows, int cols, int tileSize)
        {
            this.tiles = new GameObject[rows, cols];
            this.rowCount = rows;
            this.columnCount = cols;
            this.tileSize = tileSize;
            this.chunks = new List<Chunk>();
            this.chunkGrid = new Dictionary<GridKey, Chunk>();
        }

        public GameWorld(GameObject[,] tiles)
        {
            this.tiles = tiles;
            this.rowCount = tiles.GetLength(0);
            this.columnCount = tiles.GetLength(1);
            
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    if (this.tiles[i,j] != null)
                    {
                        this.tileSize = this.tiles[i, j].Width;
                        break;
                    }
                }
            }

            this.chunks = new List<Chunk>();
            this.chunkGrid = new Dictionary<GridKey, Chunk>();
        }

        public void AddTile(int x, int y, GameObject gameObject)
        {
            if (x < this.rowCount && y < this.columnCount)
            {
                float xValue = x * this.tileSize;
                float yValue = y * this.tileSize;
                gameObject.Position = new Vector2(xValue, yValue);
                tiles[x, y] = gameObject;
            }
        }

        public void AddChunk(Chunk chunk)
        {
            this.chunks.Add(chunk);
        }

        public void DrawWorld(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
            for (int i = 0; i < this.rowCount; i++)
            {
                for (int j = 0; j < this.columnCount; j++)
                {
                    if (this.tiles[i,j] != null)
                    {
                        this.tiles[i, j].Draw(_spriteBatch, _graphics);
                    }
                }
            }
        }

        public Dictionary<GridKey, Chunk> ChunkGrid
        {
            set
            {
                this.chunkGrid = value;
            }

            get
            {
                return this.chunkGrid;
            }
        }

        public List<Chunk> Chunks
        {
            get
            {
                return this.chunks;
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

        public float TileSize
        {
            get
            {
                return this.tileSize;
            }
        }
    }
}
