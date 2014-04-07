using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shooter
{
    public class Sprite
    {
        //variables used for drawing
        protected Texture2D texture;
        public Vector2 position = new Vector2(0, 0);
        protected Color color = Color.White;
        protected float rotation = 0;
        protected Vector2 origin = Vector2.Zero;
        protected float scale = 1;
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected float layerDepth = 0;

        //variables used for animating
        public Vector2 frameSize;
        public Vector2 sheetSize;
        Vector2 currentFrame = new Vector2(0,0);
        int millisecondsPerFrame = 60;
        int timeSinceLastFrame = 0;

        public Sprite(Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame)
        {
            this.texture = texture;
            this.position = position;
            this.origin = origin;
            this.spriteEffects = spriteEffects;

            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public Sprite(Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.scale = scale;
            this.spriteEffects = spriteEffects;
            this.layerDepth = layerDepth;

            this.frameSize = frameSize;
            this.sheetSize = sheetSize;
            this.currentFrame = currentFrame;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //animation algorithm
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    currentFrame.Y++;
                    if (currentFrame.Y >= sheetSize.Y)
                    {
                        currentFrame.Y = 0;
                    }
                }
            }


        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle((int)(currentFrame.X * frameSize.X), (int)(currentFrame.Y * frameSize.Y), (int)frameSize.X, 
                (int)frameSize.Y), color, rotation, origin, scale, spriteEffects, layerDepth);
        }

        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)frameSize.X, (int)frameSize.Y);
            }
        }

        public bool Collide(Rectangle otherSprite)
        {
            return collisionRect.Intersects(otherSprite);
        }
    }
}
