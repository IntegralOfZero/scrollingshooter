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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        
        SpriteFont TimesNewRoman;
        LevelManager levelManager;
        public GameState gameState;
  
        KeyboardState keyboardState;
        MouseState mouseState;

        Texture2D backgroundTexture;

        //game over screen stuff
        Texture2D buttonRestartTexture;
        Texture2D buttonMainMenuTexture;
        Sprite buttonRestart;
        Sprite buttonMainMenu;

        public bool restartGame = false;

        public enum GameState
        {
            Menu,
            Level,
            GameOver
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            levelManager = new LevelManager(this);
            Components.Add(levelManager);
            levelManager.Enabled = false;
            levelManager.Visible = false;

            gameState = GameState.Menu;

            IsMouseVisible = true;

            //high scores stuff
            if (!File.Exists("ShooterHiScores.txt"))
            {
                FileStream s = new FileStream("ShooterHiScores.txt", FileMode.CreateNew);
                s.Close();

            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1000;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            TimesNewRoman = Content.Load<SpriteFont>(@"Fonts/TimesNewRoman");

            backgroundTexture = Content.Load<Texture2D>(@"SpaceBackground");

            buttonRestartTexture = Content.Load<Texture2D>(@"Buttons/restart");
            buttonMainMenuTexture = Content.Load<Texture2D>(@"Buttons/mainmenu");

            buttonRestart = new Sprite(buttonRestartTexture, new Vector2(365, 300), Vector2.Zero, SpriteEffects.None, new Vector2(270, 59),
                new Vector2(1, 1), 0);
            buttonMainMenu = new Sprite(buttonMainMenuTexture, new Vector2(365, 450), Vector2.Zero, SpriteEffects.None, new Vector2(270, 59),
                new Vector2(1, 1), 0);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        gameState = GameState.Level;
                    }
                    break;

                case GameState.Level:
                    if (restartGame == true)
                    {
                        Components.Remove(levelManager);
                        levelManager = new LevelManager(this);
                        Components.Add(levelManager);

                        restartGame = false;
                    }

                    levelManager.Enabled = true;
                    levelManager.Visible = true;
                    break;

                case GameState.GameOver:
                    levelManager.Enabled = false;
                    levelManager.Visible = false;

                    

                    mouseState = Mouse.GetState();

                    //restart level
                    if((mouseState.X >= buttonRestart.position.X) && (mouseState.X <= buttonRestart.position.X + buttonRestart.frameSize.X))
                    {
                        if((mouseState.Y >= buttonRestart.position.Y) && (mouseState.Y <= buttonRestart.position.Y + buttonRestart.frameSize.Y))
                        {
                            if(mouseState.LeftButton == ButtonState.Pressed)
                            {
                                gameState = GameState.Level;
                            }
                        }
                    }

                    //go back to main menu
                    if((mouseState.X >= buttonMainMenu.position.X) && (mouseState.X <= buttonMainMenu.position.X + buttonMainMenu.frameSize.X))
                    {
                        if((mouseState.Y >= buttonMainMenu.position.Y) && (mouseState.Y <= buttonMainMenu.position.Y + buttonMainMenu.frameSize.Y))
                        {
                            if(mouseState.LeftButton == ButtonState.Pressed)
                            {
                                gameState = GameState.Menu;
                            }
                        }
                    }


                    break;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            switch(gameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
                    spriteBatch.DrawString(TimesNewRoman, "Press Enter To Begin", new Vector2(300,375), Color.White);
                    
                    break;
                
                case GameState.Level:
                    break;

                case GameState.GameOver:
                    spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
                    buttonRestart.Draw(gameTime, spriteBatch);
                    buttonMainMenu.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
