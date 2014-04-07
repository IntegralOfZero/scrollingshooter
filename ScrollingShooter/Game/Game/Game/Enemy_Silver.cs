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
    class Enemy_Silver : Enemy
    {
        int time = 0;
        Random rnd = new Random();

        public Enemy_Silver(LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(levelManager, texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            Initializer();
        }

        public Enemy_Silver(LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(levelManager, texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            Initializer();
        }

        public override void Initializer()
        {
            HP = 30;
            speed = 4;
            scorePoints = 100;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;

            //movement pattern
            if (time >= 0 && time <= 1500)
            {
                position.Y += speed;
            }
            else if (time >= 1501 && time <= 2500)
            {
                position.X += speed;
            }
            else if (time >= 2501 && time <= 3000)
            {
                position.Y += speed;
            }
            else if (time >= 3001 && time <= 4000)
            {
                position.X -= speed;
            }
            else
            {
                time = 0;
            }

            if (rnd.Next(50) < 1) levelManager.enemyWeapons.Add(new BulletRed(levelManager, levelManager.Game.Content.Load<Texture2D>(@"EnemyWeapons/BulletRed"),
                 new Vector2(position.X + (((position.X + frameSize.X) - position.X) / 2), position.Y + (((position.Y + frameSize.Y) - position.Y) / 2)), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 0));

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
