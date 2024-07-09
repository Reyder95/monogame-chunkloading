using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game_Dev_Practice
{
    // Generic GameObject class. This will be the main item that contains data for each game object. Such as the image it will draw, the width and height, etc. 
    // TODO: Add DataContainer which will be a way to store data within this game object
    internal class GameObject
    {
        Texture2D texture;  // Texture of the game object, what will be drawn to the screen.
        Vector2 position;   // The position in the world of the game object.
        float width;        // The width of the game object
        float height;       // The height of the game object

        // Default constructor
        public GameObject()
        {
            this.texture = null;
            this.position = new Vector2(0, 0);
            this.width = 0;
            this.height = 0;
        }

        // Constructor which requires all parameters
        public GameObject(Texture2D texture, Vector2 position, float width, float height)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
        }

        // Constructor which automatically generates the width and the height
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.width = texture.Width;
            this.height = texture.Height;
        }

        // Draws the object to the screen provided a sprite batch and graphics driver. Will always generate the object within the game world, not just the screen. This abstracts all
        // the complexity of drawing entirely
        public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
            _spriteBatch.Draw(
                texture,
                new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2),
                null,
                Color.White,
                0f,
                Helpers.CalculateWorldCoordinates(new Vector2(-position.X, -position.Y), this),
                Vector2.One,
                SpriteEffects.None,
                0f
                );
        }

        public Texture2D Texture
        {
            set
            {
                this.texture = value;
            }

            get
            {
                return this.texture;
            }
        }
        public Vector2 Position
        {
            set
            {
                this.position = value;
            }

            get
            {
                return this.position;
            }
        }

        public float Width
        {
            set
            {
                this.width = value;
            }

            get
            {
                return this.width;
            }
        }

        public float Height
        {
            set
            {
                this.height = value;
            }

            get
            {
                return this.height;
            }
        }

    }
}
