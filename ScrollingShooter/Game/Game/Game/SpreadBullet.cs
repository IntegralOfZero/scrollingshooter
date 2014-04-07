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
    class SpreadBullet : Weapon
    {
        public enum Direction
        {
            N,
            NE,
            E,
            NW,
            W
        }

        Direction direction;

        int time = 0;

        public SpreadBullet(Direction direction, LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(levelManager, texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            this.direction = direction;
            Initializer();
        }

        public SpreadBullet(Direction direction, LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(levelManager, texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            this.direction = direction;
            Initializer();
        }

        public override void Initializer()
        {
            damage = 7;

            switch (direction)
            {
                case Direction.N:
                    speed = 8;
                    speedX = 0;
                    break;
                case Direction.NE:
                    speed = 7.39f;
                    speedX = 3.06f;
                    break;
                case Direction.E:
                    speed = 3.06f;
                    speedX = 7.39f;
                    break;
                case Direction.NW:
                    speed = 7.39f;
                    speedX = -3.06f;
                    break;
                case Direction.W:
                    speed = 3.06f;
                    speedX = -7.39f;
                    break;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;

            position.Y -= speed;
            position.X += speedX;

            if (time > 1000)
            {
                levelManager.playerWeapons.Remove(this);
            }

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}

