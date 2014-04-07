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
    class BulletRed : Weapon
    {
        float speedX = 0;

        public BulletRed(LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(levelManager, texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            Initializer();
        }

        public BulletRed(LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(levelManager, texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            Initializer();
        }

        public override void Initializer()
        {
            damage = 5;

            if (levelManager.player.HP > 0)
            {
                float distance = levelManager.player.position.X - position.X;

                //positive means player is to the right of this sprite
                if (distance >= 200)
                {
                    speedX = 6;
                    speed = 5.29f;
                }
                else if (distance >= 100 && distance < 200)
                {
                    speedX = 3;
                    speed = 7.41f;
                }
                //in front of this sprite
                else if (distance > -100 && distance < 100)
                {
                    speedX = 0;
                    speed = 8;
                }
                //negative means player is to the right of this sprite
                else if (distance <= -200)
                {
                    speedX = -6;
                    speed = 5.29f;
                }
                else if (distance <= -100 && distance > -200)
                {
                    speedX = -3;
                    speed = 7.41f;
                }
            }

        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            position.Y += speed;
            position.X += speedX;

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}

