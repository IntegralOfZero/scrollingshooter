using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Shooter
{
    public class PowerUp : Sprite
    {
        LevelManager levelManager;
        public int index;
        int time = 0;

        public PowerUp(LevelManager levelManager, int index, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame)
            : base(texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            this.levelManager = levelManager;
            this.index = index;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;

            if (time >= 0 && time <= 2000)
            {
                position.Y += 5;
            }
            if (time >= 2000 && time <= 4000)
            {
                position.Y -= 5;
            }
            else
            {
                time = 0;
            }

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
