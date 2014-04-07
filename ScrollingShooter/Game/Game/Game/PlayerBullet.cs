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
    class PlayerBullet : Weapon
    {

        public PlayerBullet(LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(levelManager, texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            Initializer();
        }

        public PlayerBullet(LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(levelManager, texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            Initializer();
        }

        public override void Initializer()
        {

            speed = 8;
            damage = 10;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            position.Y -= speed;

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}

