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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>

    public class LevelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
  
        public Player player;
        SpriteBatch spriteBatch;

        int level = 1;
        bool loadContentFlag = true;
        Random rnd = new Random();
        int drop = 100; //powe rup drop chance

        public List<Enemy> enemies = new List<Enemy>();
        public List<Weapon> playerWeapons = new List<Weapon>();
        public List<Weapon> enemyWeapons = new List<Weapon>();
        public List<PowerUp> powerUps = new List<PowerUp>();

        //HUD related info
        public int score = 0;
        SpriteFont TimesNewRoman;

        //background
        Sprite background;
        Sprite background2;

        //constants for enemies--------
        
        //Enemy_Silver
        Texture2D Enemy_SilverTexture;
        Vector2 Enemy_SilverOrigin= Vector2.Zero;
        SpriteEffects Enemy_SilverSprEff = SpriteEffects.None;
        Vector2 Enemy_SilverSheetSize = new Vector2(1,1);
        Vector2 Enemy_SilverFrameSize = new Vector2(70,70);
        int Enemy_SilverMillisecondsPerFrame = 60;

        //Enemy_Sphere
        Texture2D Enemy_SphereTexture;
        Vector2 Enemy_SphereOrigin = Vector2.Zero;
        SpriteEffects Enemy_SphereSprEff = SpriteEffects.None;
        Vector2 Enemy_SphereSheetSize = new Vector2(1, 1);
        Vector2 Enemy_SphereFrameSize = new Vector2(70, 70);
        int Enemy_SphereMillisecondsPerFrame = 60;

        //constants for power ups
        Texture2D PowerUpX_BlasterTexture;
        Texture2D PowerUpScreen_CannonTexture;

        Vector2 PowerUpOrigin = Vector2.Zero;
        SpriteEffects PowerUpSprEff = SpriteEffects.None;

        Vector2 PowerUpX_BlasterSheetSize = new Vector2(1, 1);
        Vector2 PowerUpX_BlasterFrameSize = new Vector2(40, 40);
        Vector2 PowerUpScreen_CannonFrameSize = new Vector2(35, 35);
        Vector2 PowerUpScreen_CannonSheetSize = new Vector2(1, 1);

        int PowerUpX_BlasterMillisecondsPerFrame = 60;
        int PowerUpScreen_CannonMillisecondsPerFrame = 60;

        //-----------------------------
        public LevelManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
       

            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            TimesNewRoman = Game.Content.Load<SpriteFont>(@"Fonts/TimesNewRoman");

            //background
            background = new Sprite(Game.Content.Load<Texture2D>(@"SpaceBackground"), Vector2.Zero, Vector2.Zero, SpriteEffects.None, new Vector2(1280, 1171),
                new Vector2(1, 1), 0);
            background2 = new Sprite(Game.Content.Load<Texture2D>(@"SpaceBackground"), new Vector2(0, -1171), Vector2.Zero, SpriteEffects.None, new Vector2(1280, 1171),
                new Vector2(1, 1), 0);

            //loading textures of enemies---------------
            Enemy_SilverTexture = Game.Content.Load<Texture2D>(@"Enemies/Enemy_Silver");
            Enemy_SphereTexture = Game.Content.Load<Texture2D>(@"Enemies/Enemy_Sphere");

            //loading textures of power ups
            PowerUpX_BlasterTexture = Game.Content.Load<Texture2D>(@"PowerUps/X_Blaster");
            PowerUpScreen_CannonTexture = Game.Content.Load <Texture2D>(@"PowerUps/Screen_Cannon"); 

            //------------------------------------------

            player = new Player(this, Game.Content.Load<Texture2D>(@"player"), new Vector2(475, 700), Vector2.Zero, SpriteEffects.None, new Vector2(70, 70),
            new Vector2(1, 1), 60);

            

            
            base.LoadContent();
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //update player
            player.Update(gameTime, Game.Window.ClientBounds);

            //background
            background.position.Y += 3;
            background2.position.Y += 3;
            if (background.position.Y >= Game.Window.ClientBounds.Height)
            {
                background.position.Y = -1171;
            }
            if (background2.position.Y >= Game.Window.ClientBounds.Height)
            {
                background2.position.Y = -1171;
            }

            //background.Update(gameTime, Game.Window.ClientBounds);

            //generate enemies: Silver
            if (rnd.Next(75) < 1)
            {
                enemies.Add(new Enemy_Silver(this, Enemy_SilverTexture, new Vector2(rnd.Next(50, 950), -100), Enemy_SilverOrigin, Enemy_SilverSprEff, Enemy_SilverFrameSize,
               Enemy_SilverSheetSize, Enemy_SilverMillisecondsPerFrame));
            }
            //generate enemies:SPhere
            if (rnd.Next(100) < 1)
            {
                enemies.Add(new Enemy_Sphere(this, Enemy_SphereTexture, new Vector2(rnd.Next(50, 950), -100), Enemy_SphereOrigin, Enemy_SphereSprEff, Enemy_SphereFrameSize,
               Enemy_SphereSheetSize, Enemy_SphereMillisecondsPerFrame));
            }

            //update enemies weapons 
            //the try/catch block is to prevent an exception from "line X"
            try
            {
                foreach (Weapon enemyWep in enemyWeapons)
                {
                    enemyWep.Update(gameTime, Game.Window.ClientBounds);
                    if (enemyWep.Collide(player.collisionRect))
                    {
                        player.HP -= enemyWep.damage;
                        enemyWeapons.Remove(enemyWep); //"line X"
                    }
                }
                
            }
            catch
            {

            }

            //update enemies and player weapons 
            //the try/catch block is to prevent an exception from "line X"
            try
            {

                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Update(gameTime, Game.Window.ClientBounds);

                        foreach (Weapon weapon in playerWeapons)
                        {
                           if (enemy.Collide(weapon.collisionRect))
                            {
                                enemy.HP -= weapon.damage;
                                playerWeapons.Remove(weapon); //line X
                            }
                        }

                        if (enemy.Collide(player.collisionRect))
                        {
                            enemy.HP -= 20;
                            player.HP -= 20;
                        }

                        if (enemy.HP <= 0)
                        {
                            score += enemy.scorePoints;
                            //when enemy is removed, possibility of dropping power up
                            drop = rnd.Next(10);
                            if (drop < 2)
                            {
                                powerUps.Add(new PowerUp(this, 0, PowerUpX_BlasterTexture, enemy.position, PowerUpOrigin, PowerUpSprEff, PowerUpX_BlasterFrameSize,
                                    PowerUpX_BlasterSheetSize, PowerUpX_BlasterMillisecondsPerFrame));
                            }
                            else if(drop >= 2 && drop < 4)
                            {
                                powerUps.Add(new PowerUp(this, 1, PowerUpScreen_CannonTexture, enemy.position, PowerUpOrigin, PowerUpSprEff, PowerUpScreen_CannonFrameSize,
                                    PowerUpScreen_CannonSheetSize, PowerUpScreen_CannonMillisecondsPerFrame));
                            }
                            enemies.Remove(enemy); //"line X"
                        }
                    }
     

                foreach (Weapon weapon in playerWeapons)
                {
                   weapon.Update(gameTime, Game.Window.ClientBounds);
                }
            }
            catch
            {

            }

            //check for player collision with power up
            try
            {
                foreach (PowerUp powerUp in powerUps)
                {
                    powerUp.Update(gameTime, Game.Window.ClientBounds);

                    if (player.Collide(powerUp.collisionRect))
                    {
                        if (powerUp.index == 0)
                        {
                            if (player.currentWeapon == Player.Weapon.X_Blaster)
                            {
                                player.powerLevel++;
                            }
                            else
                            {
                                player.powerLevel = 1;
                                player.currentWeapon = Player.Weapon.X_Blaster;
                            }
                        }
                        else if (powerUp.index == 1)
                        {
                            if (player.currentWeapon == Player.Weapon.Screen_Cannon)
                            {
                                player.powerLevel++;
                            }
                            else
                            {
                                player.powerLevel = 1;
                                player.currentWeapon = Player.Weapon.Screen_Cannon;
                            }
                        }

                        powerUps.Remove(powerUp); //line X
                    }
                }
            }
            catch { }

            if (player.lives <= 0)
            {
                ((Game1)Game).restartGame = true;
                DisplayHighScores();
                ((Game1)Game).gameState = Game1.GameState.GameOver;
            }
              

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add your update code here

            spriteBatch.Begin();

            //background
            background.Draw(gameTime, spriteBatch);
            background2.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);


            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            foreach (Sprite weapon in playerWeapons)
            {
                weapon.Draw(gameTime, spriteBatch);
            }

            foreach (Sprite weapon in enemyWeapons)
            {
                weapon.Draw(gameTime, spriteBatch);
            }

            foreach(PowerUp powerUp in powerUps)
            {
                powerUp.Draw(gameTime, spriteBatch);
            }

            //draw HUD stuff
            spriteBatch.DrawString(TimesNewRoman, "Score: " + score.ToString(), new Vector2(50, 725), Color.White);
            spriteBatch.DrawString(TimesNewRoman, "Shields: " + player.HP.ToString(), new Vector2(50, 750), Color.White);
            spriteBatch.DrawString(TimesNewRoman, "Shields: " + player.lives.ToString(), new Vector2(50, 775), Color.White);
            spriteBatch.DrawString(TimesNewRoman, "Drop: " + drop, new Vector2(50, 700), Color.White);

            spriteBatch.End();

            base.Update(gameTime);
        }

        public void DisplayHighScores()
        {
                    HighScores highScores = new HighScores(score);
                    highScores.Write();
                    highScores.Read();
                    highScores.CloseStream();
        }
    }
}
