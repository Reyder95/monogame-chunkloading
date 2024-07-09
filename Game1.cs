using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2D_Game_Dev_Practice
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ballTexture;  // Texture of a ball
        Texture2D whiteTileTexture;
        Texture2D blackTileTexture;
        GameObject ballGameObject;  // The game object that contains the ball texture
        GameObject whiteTile;

        static int worldSize = 1000;
        static int tileSize = 30;
        static int chunkSize = 16;
        static int viewDistance = 2000;

        GameObject[,] tileMap = new GameObject[worldSize,worldSize];
        Dictionary<GridKey, Chunk> gridChunk = new Dictionary<GridKey, Chunk>();

        GameWorld world;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameInstance.mainCamera = new Camera(new Vector2(0, 0));    // Initializes camera at world position (0, 0)
            _graphics.PreferredBackBufferWidth = 2560;
            _graphics.PreferredBackBufferHeight = 1440;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");  // Creates the Texture2D of the ball
            ballGameObject = new GameObject(ballTexture, new Vector2(200, 200));    // Creates the GameObject for the ball, and places it at world position (100, 100)

            whiteTileTexture = Helpers.ResizeTile(_spriteBatch, GraphicsDevice, Content.Load<Texture2D>("white_tile"), tileSize, tileSize);
            blackTileTexture = Helpers.ResizeTile(_spriteBatch, GraphicsDevice, Content.Load<Texture2D>("black_tile"), tileSize, tileSize);

            for (int i = 0; i < worldSize; i++)
            {
                for (int j = 0; j < worldSize; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        tileMap[i, j] = new GameObject(whiteTileTexture, new Vector2(i * tileSize, j * tileSize));
                    } else
                    {
                        tileMap[i, j] = new GameObject(blackTileTexture, new Vector2(i * tileSize, j * tileSize));
                    }
                }
            }

            world = new GameWorld(tileMap);

            try
            {
                for (int startI = 0; startI < world.RowCount; startI += chunkSize)
                {
                    for (int startJ = 0; startJ < world.ColumnCount; startJ += chunkSize)
                    {
                        GameObject[,] tileChunk = new GameObject[chunkSize, chunkSize];
                        for (int i = startI; i < (startI + chunkSize); i++)
                        {
                            for (int j = startJ; j < (startJ + chunkSize); j++)
                            {
                                if (i < worldSize && j < worldSize)
                                    tileChunk[i % chunkSize, j % chunkSize] = tileMap[i, j];
                            }
                        }

                        Chunk newChunk = new Chunk(tileChunk);

                        world.AddChunk(newChunk);

                        gridChunk.Add(new GridKey(((int)newChunk.Position.X / chunkSize) / tileSize, ((int)newChunk.Position.Y / chunkSize) / tileSize), newChunk);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            world.ChunkGrid = gridChunk;
            
        }

        protected override void Update(GameTime gameTime)
        {
            // Movement with the arrow keys
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                GameInstance.mainCamera.Translate(new Vector2(0, 7));
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                GameInstance.mainCamera.Translate(new Vector2(0, -7));
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                GameInstance.mainCamera.Translate(new Vector2(7, 0));
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                GameInstance.mainCamera.Translate(new Vector2(-7, 0));
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //world.DrawWorld(_spriteBatch, _graphics);
            Helpers.DrawChunksNearPlayer(world, _spriteBatch, _graphics, GameInstance.mainCamera, viewDistance, chunkSize, tileSize);
            //world.ChunkGrid[new GridKey(0, 0)].DrawChunk(_spriteBatch, _graphics);
            //world.ChunkGrid[new GridKey(0, 1)].DrawChunk(_spriteBatch, _graphics);
            //world.Chunks[0].DrawChunk(_spriteBatch, _graphics);
            //world.DrawWorld(_spriteBatch, _graphics); 
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}