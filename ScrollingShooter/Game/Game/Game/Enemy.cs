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
    public abstract class Enemy : Sprite
    {
        public LevelManager levelManager;
        public int HP;
        public float speed;
        public int scorePoints;

        public Enemy(LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            this.levelManager = levelManager;
        }

        public Enemy(LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            this.levelManager = levelManager;
        }

        //implement in subclasses and call in their constructors!
        //initialize the variables declared at top o fclass
        public abstract void Initializer();

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
