using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Game_Dev_Practice
{
    // Camera for the game world. Supports movement via Translate() function
    internal class Camera
    {
        private Vector2 worldPosition;  // The position within the game world (not the screen position)

        public Camera(Vector2 coordinates)
        {
            this.worldPosition = new Vector2(coordinates.X, coordinates.Y);
        }

        // Translates the camera a Vector2.X amount, and a Vector2.Y amount.
        public void Translate(Vector2 direction)
        {
            this.worldPosition.X += direction.X;
            this.worldPosition.Y += direction.Y;
        }

        public Vector2 WorldPosition 
        { 
            get
            {
                return this.worldPosition;
            }
        }


    }
}
