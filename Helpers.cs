using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game_Dev_Practice
{
    public class GridKey : IEquatable<GridKey>
    {
        public int x;
        public int y;

        public GridKey(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override int GetHashCode()
        {
            return x + y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GridKey);
        }

        public bool Equals(GridKey other) 
        {
            if (other != null)
            {
                if (other.x == this.x && other.y == this.y)
                {
                    return true;
                }
            }

            return false;
        }
    }

    internal static class Helpers
    {

        public static Vector2 CalculateWorldCoordinates(Vector2 position, GameObject gameObject)
        {
            float xPosition =  (gameObject.Width / 2) + position.X - GameInstance.mainCamera.WorldPosition.X;
            float yPosition = (gameObject.Height / 2) + position.Y - GameInstance.mainCamera.WorldPosition.Y;
            return new Vector2(xPosition, yPosition);
        }

        public static Texture2D ResizeTile(SpriteBatch spriteBatch, GraphicsDevice graphics, Texture2D originalTile, int newWidth, int newHeight)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(graphics, newWidth, newHeight);

            graphics.SetRenderTarget(renderTarget);
            graphics.Clear(Color.Transparent);

            spriteBatch.Begin();
            spriteBatch.Draw(originalTile, new Rectangle(0, 0, newWidth, newHeight), Color.White);
            spriteBatch.End();

            graphics.SetRenderTarget(null);

            return renderTarget;
        }

        public static void DrawChunksNearPlayer(GameWorld world, SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics, Camera camera, int viewDistance, int chunkSize, int tileSize)
        {

            for (int dx = (int)(camera.WorldPosition.X) - viewDistance; dx < (int)camera.WorldPosition.X + viewDistance; dx += chunkSize * tileSize)
            {
                for (int dy = (int)camera.WorldPosition.Y - viewDistance; dy < (int)camera.WorldPosition.Y + viewDistance; dy += chunkSize * tileSize)
                {
                    GridKey chunkGridKey = GetGridKey(new Vector2(dx, dy), chunkSize, tileSize);
                    Chunk chunk;
                    
                    if (world.ChunkGrid.TryGetValue(chunkGridKey, out chunk))
                    {
                        
                        chunk.DrawChunk(_spriteBatch, _graphics);
                    }
                }
            }
        }

        public static GridKey GetGridKey(Vector2 position, int chunkSize, int tileSize)
        {
            return new GridKey(-((int)position.X / chunkSize) / tileSize, -((int)position.Y / chunkSize) / tileSize);
        }
    }
}
