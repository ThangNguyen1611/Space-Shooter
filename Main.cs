#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;
#endregion

namespace PROJECT_SpaceShooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        World world;

        Camera cam;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.HardwareModeSwitch = false;
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
            Global.screenwidth = 1366; //1366
            Global.screenheight = 768; //768

            graphics.PreferredBackBufferWidth = Global.screenwidth;
            graphics.PreferredBackBufferHeight = Global.screenheight;

            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();

            cam = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Global.content = this.Content;
            Global.spriteBatch = new SpriteBatch(GraphicsDevice);

            
            // TODO: use this.Content to load your game content here
            
            //Global.keyboard = new MyKeyboard();
            Global.mouse = new Mousecontrol();

            world = new World();

            Global.soundcontrol = new SoundControl("StarWarMainMusic");
            Global.soundcontrol.AddSound("Respawn", "GameStart", 1f);
            Global.soundcontrol.AddSound("Shooting", "Shoot1", .1f);
            Global.soundcontrol.AddSound("SkillSwiftSound", "Swift", 1f);
            Global.soundcontrol.AddSound("BulletHit", "BulletCollision", 1f);
            Global.soundcontrol.AddSound("HitImp", "HitImp", 1f);
            Global.soundcontrol.AddSound("TriggerBuff", "TriggerBuff", .5f);
            Global.soundcontrol.AddSound("FREEZE", "Freeze", 1f);
            Global.soundcontrol.AddSound("GameOver", "GameOver", 1f);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            Global.gametime = gameTime;
            //Global.keyboard.Update();
            Global.mouse.Update();

            world.Update();

            //Global.keyboard.UpdateOld();
            Global.mouse.UpdateOld();

            cam.Update(gameTime, world.Ship);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            Global.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.transform);

            
            world.Draw(Vector2.Zero);
            Global.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
}
