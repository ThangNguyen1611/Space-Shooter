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
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        World world;

        GamePlay gameplay;

        Camera cam;



        //danhthem
        public enum GameState
        {
            MainMenu,
            Options,
            Playing,
            IntroVideo,
        }
        GameState CurrentGameState = GameState.IntroVideo;
        cButton btnPlay;
        cButton btnExit;
        cButton btnAbout;
        cButton btnAboutexit;





        Texture2D videotexture;
        Video video;
        VideoPlayer videoPlayer;
        int dem = 0;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.HardwareModeSwitch = false;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Global.screenwidth = 1366; //1366
            Global.screenheight = 768; //768

            //Global.screenwidth = 1920; //1366
            //Global.screenheight = 1080; //768

            graphics.PreferredBackBufferWidth = Global.screenwidth;
            graphics.PreferredBackBufferHeight = Global.screenheight;

            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();

            cam = new Camera(GraphicsDevice.Viewport);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            Global.content = this.Content;

            Global.spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Global.mouse = new Mousecontrol();

            IsMouseVisible = true;
            btnExit = new cButton(Content.Load<Texture2D>("quit4"), graphics.GraphicsDevice);
            btnAbout = new cButton(Content.Load<Texture2D>("about8"), graphics.GraphicsDevice);
            btnAboutexit = new cButton(Content.Load<Texture2D>("back4"), graphics.GraphicsDevice);
            btnPlay = new cButton(Content.Load<Texture2D>("play5"), graphics.GraphicsDevice);

            btnPlay.setPosition(new Vector2(250, 200));
            btnAbout.setPosition(new Vector2(250, 300));
            btnExit.setPosition(new Vector2(250, 400));
            btnAboutexit.setPosition(new Vector2(550, 550));

            gameplay = new GamePlay();

            //Global.soundcontrol = new SoundControl("");
            Global.soundcontrol = new SoundControl("BreaktheSwordofJustice");
            #region AddSound
            Global.soundcontrol.AddSound("Respawn", "GameStart", 1f);
            Global.soundcontrol.AddSound("Shooting", "Zap", 1f);
            Global.soundcontrol.AddSound("SkillSwiftSound", "SwiftingSkill", 1f);
            Global.soundcontrol.AddSound("BulletHit", "LaserHit", 0.5f);
            Global.soundcontrol.AddSound("MobHit", "AlienCough", 1f);
            Global.soundcontrol.AddSound("TriggerBuff", "TriggerBuff", .5f);
            Global.soundcontrol.AddSound("FREEZE", "Freeze", 1f);
            Global.soundcontrol.AddSound("GameOver", "GameOver", 1f);
            Global.soundcontrol.AddSound("NuclearBombSound", "NuclearBombSound", 1f);
            Global.soundcontrol.AddSound("BlitzHook", "BlitzHook", 0.5f);
            Global.soundcontrol.AddSound("GatlingGun", "MultiShooting", 0.5f);
            Global.soundcontrol.AddSound("LightBoltSound", "LightBoltSkill", 1f);
            Global.soundcontrol.AddSound("ClickSound", "Click", 1f);
            Global.soundcontrol.AddSound("DeadSound", "LowDown", 1f);
            Global.soundcontrol.AddSound("ShimmerSound", "Shimmering", 1f);
            Global.soundcontrol.AddSound("BossWarningSound", "Warning", 0.7f);
            Global.soundcontrol.AddSound("FatboizScreamSound", "AlienScream", 0.7f);
            Global.soundcontrol.AddSound("BlitzScreamSound", "AlienScream2", 0.7f);
            Global.soundcontrol.AddSound("CableScreamSound", "AlienScream3", 0.7f);
            Global.soundcontrol.AddSound("ShieldUpSound", "MagicPositive", 1f);
            Global.soundcontrol.AddSound("ShieldDownSound", "MagicNegative", 1f);
            #endregion

            Global.soundcontrol.bgmusic.instance.Stop();

            video = Content.Load<Video>("Intro");
            videoPlayer = new VideoPlayer();
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (dem == 0)
            {
                videoPlayer.Play(video);
                dem = 1;
            }


            if (dem == 1)
            {
                switch (CurrentGameState)
                {

                    case GameState.MainMenu:
                        MouseState mouse = Mouse.GetState();
                        if (btnPlay.isclicked == true)
                            CurrentGameState = GameState.Playing;

                        if (btnExit.isclicked == true)
                            Exit();
                        if (btnAbout.isclicked == true)
                            CurrentGameState = GameState.Options;
                        btnPlay.Update(mouse);
                        btnExit.Update(mouse);
                        btnAbout.Update(mouse);
                        break;
                    case GameState.Playing:
                        {
                            Global.soundcontrol.bgmusic.instance.Play();
                            //graphics.IsFullScreen = true;
                            //graphics.ApplyChanges();

                            IsMouseVisible = false;
                            Global.gametime = gameTime;

                            Global.mouse.Update();

                            gameplay.Update();

                            Global.mouse.UpdateOld();

                            cam.Update(gameTime, gameplay.world.Ship);
                            break;
                        }
                    case GameState.Options:
                        MouseState mouse1 = Mouse.GetState();
                        if (btnAboutexit.isclicked == true)
                            CurrentGameState = GameState.MainMenu;
                        btnAboutexit.Update(mouse1);
                        break;
                    case GameState.IntroVideo:
                        {
                            //if (dem != 0)
                            //    CurrentGameState = GameState.MainMenu;
                            //if (videoPlayer.State == MediaState.Stopped&&dem==0)
                            //{
                            //    videoPlayer.Play(video);
                            //    dem++;
                            //}
                            if (videoPlayer.State == MediaState.Stopped)
                                CurrentGameState = GameState.MainMenu;
                            break;
                        }
                }
            }





            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            if (videoPlayer.State != MediaState.Stopped)
                videotexture = videoPlayer.GetTexture();
            //Global.spriteBatch.Begin();
            //if (videotexture != null)
            //    Global.spriteBatch.Draw(videotexture, GraphicsDevice.Viewport.Bounds, Color.White);
            //Global.spriteBatch.End();



            Global.spriteBatch.Begin();
            //Global.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.transform);

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    Global.spriteBatch.Draw(Content.Load<Texture2D>("imageddsa"), new Rectangle(0, 0, Global.screenwidth, Global.screenheight), Color.White);
                    btnPlay.Draw(Global.spriteBatch);
                    btnAbout.Draw(Global.spriteBatch);
                    btnExit.Draw(Global.spriteBatch);
                    break;
                case GameState.Playing:
                    {
                        Global.spriteBatch.End();
                        Global.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.transform);
                        GraphicsDevice.Clear(Color.Black);
                        gameplay.Draw();
                        //Global.spriteBatch.End();
                        break;
                    }
                case GameState.Options:
                    GraphicsDevice.Clear(Color.Gray);

                    Global.spriteBatch.Draw(Content.Load<Texture2D>("gioithieu"), new Rectangle(0, 0, Global.screenwidth, Global.screenheight), Color.White);
                    btnAboutexit.Draw(Global.spriteBatch);

                    break;
                case GameState.IntroVideo:
                    if (videotexture != null)
                        Global.spriteBatch.Draw(videotexture, GraphicsDevice.Viewport.Bounds, Color.White);

                    break;
            }

            Global.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
#if WINDOWS || LINUX

    public static class Program
    {

        static void Main()
        {
            using (var game = new Main())
                game.Run();
        }
    }
#endif
}
