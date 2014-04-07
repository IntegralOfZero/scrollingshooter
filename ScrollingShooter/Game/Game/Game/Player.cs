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
    public class Player : Sprite
    {
        LevelManager levelManager; 
        KeyboardState keyboardState;
        
        float speed = 6;
        bool fireFlag = true;
        public int HP;
        public int initialHP = 200;
        public int lives = 3;

        public int powerLevel = 1;
        public Weapon currentWeapon = Weapon.X_Blaster;

        public enum Weapon
        {
            X_Blaster,
            Screen_Cannon
        };

        public Player(LevelManager levelManager, Texture2D texture, Vector2 position, Vector2 origin, SpriteEffects spriteEffects, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame) :
            base(texture, position, origin, spriteEffects, frameSize, sheetSize, millisecondsPerFrame)
        {
            this.levelManager = levelManager;
            HP = initialHP;
        }

        public Player(LevelManager levelManager, Texture2D texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects,
            float layerDepth, Vector2 frameSize, Vector2 sheetSize, int millisecondsPerFrame, Vector2 currentFrame) :
            base(texture, position, color, rotation, origin, scale, spriteEffects,
            layerDepth, frameSize, sheetSize, millisecondsPerFrame, currentFrame)
        {
            this.levelManager = levelManager;
            HP = initialHP;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Rectangle clientBounds)
        {
            keyboardState = Keyboard.GetState();
            
            //move player based on keyboard input
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                position.X += speed;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position.X -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                position.Y -= speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                position.Y += speed;
            }

            //shoot weapon
            //make it so that one press = one fire
            if (fireFlag == true)
            {

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (currentWeapon == Weapon.X_Blaster)
                    {
                        if(powerLevel == 1)
                        {
                             levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));
                        }
                        else if (powerLevel == 2)
                        {
                            levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 13, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 38, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));
                        }
                        else if (powerLevel >= 3)
                        {
                            levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 5, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new PlayerBullet(levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/PlayerBullet"), new Vector2(
                                position.X + 45, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(15, 25), new Vector2(1, 1), 1));
                        }
                    }
                    else if (currentWeapon == Weapon.Screen_Cannon)
                    {
                        if (powerLevel == 1)
                        {
                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.N, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));
                        }
                        else if (powerLevel == 2)
                        {
                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.N, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.NW, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.NE, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));
                        }
                        else if (powerLevel >= 3)
                        {
                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.N, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                                position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.NW, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.NE, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                               position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.E, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                                position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));

                            levelManager.playerWeapons.Add(new SpreadBullet(SpreadBullet.Direction.W, levelManager, levelManager.Game.Content.Load<Texture2D>(@"PlayerWeapons/SpreadBullet"), new Vector2(
                                position.X + 25, position.Y + 27), Vector2.Zero, SpriteEffects.None, new Vector2(9, 9), new Vector2(1, 1), 1));
                        }
                    }
                }

                fireFlag = false;
            }
            if (keyboardState.IsKeyUp(Keys.Space))
            {
                fireFlag = true;
            }

            if (HP <= 0)
            {
                lives -= 1;
                HP = initialHP;
            }

            
            base.Update(gameTime, clientBounds);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        
    }
}
