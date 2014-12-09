using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid_Belt_Assault
{
    /*todo
     * 
     * powerups
     * particles
     * boss
     * shop
     * LESS enemies
     * 
     */
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameStates { TitleScreen, Playing, PlayerDead, GameOver,Levels,LoadLevel,Win };
        enum Planets { SUN, EARTH, MARS, MERCURY, NEPTUNE, none };
        GameStates gameState = GameStates.TitleScreen;
        Planets Planet = Planets.none;
        Texture2D background;
        Texture2D spriteSheet;
        Texture2D planets;
        Texture2D WinImg;
        Texture2D Check;
        Texture2D Vial;
        Texture2D buutt;
        Texture2D HpBar;
        Texture2D Backgroundstars;
        Texture2D Healthpack;
        Texture2D Pause;
        Song song;

        Random rand = new Random();

        StarField starField;
        AsteroidManager asteroidManager;
        PlayerManager playerManager;
        EnemyManager enemyManager;
        ExplosionManager explosionManager;
        PlanetFlyby planetfly; 
        CollisionManager collisionManager;
        MouseState ms;

        SpriteFont pericles14;

        private float playerDeathDelayTime = 0f;
        private float playerDeathTimer = 0f;
        private float titleScreenTimer = 0f;
        private float titleScreenDelayTime = 1f;

        int Health = 5000;

        private int playerStartingLives = 3;
        private Vector2 playerStartLocation = new Vector2(390, 550);
        private Vector2 scoreLocation = new Vector2(20, 10);
        private Vector2 livesLocation = new Vector2(20, 25);

        bool leftMouseClicked;
        bool Canclick;

        Rectangle mouserect;

        Sprite sun, mercury, earth, mars, neptune;

        Rectangle buttbox = new Rectangle(150, 420, 500, 59 + 1);
        Rectangle Pausebox = new Rectangle(0, 0, 50, 50);

        bool suncheck = false;
        bool mercurycheck = false;
        bool earfcheck = false;
        bool marscheck = false;
        bool neptunecheck = false;

        List<Sprite> jizz=new List<Sprite>();
        int jizzCount=0;

        int jizzingtime = 0;

        List<Sprite> Healthpacks = new List<Sprite>();
        int Healthcount = 0;

        int Healthtime = 0;

        int wintimer = 120;
        int timeupdate = 0;
        int seconds = 0;
        int minutes = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            
            background = Content.Load<Texture2D>(@"Textures\background");
            spriteSheet = Content.Load<Texture2D>(@"Textures\spriteSheet");
            planets = Content.Load<Texture2D>(@"Textures\Planets");
            WinImg = Content.Load<Texture2D>(@"Textures\Win");
            Check = Content.Load<Texture2D>(@"Textures\check");
            Vial = Content.Load<Texture2D>(@"Textures\vial");
            buutt = Content.Load<Texture2D>(@"Textures\butt");
            Backgroundstars = Content.Load<Texture2D>(@"Textures\Custom.png");
            HpBar = Content.Load<Texture2D>(@"Textures\Bar");
            Healthpack = Content.Load<Texture2D>(@"Textures\hp");
            Pause = Content.Load<Texture2D>(@"Textures\Pause");
            song = Content.Load<Song>(@"Sounds\GSP_Xmas_intro_2014_Full_");
            MediaPlayer.Play(song);
            sun=new Sprite(new Vector2(10, 200), planets, new Rectangle(2 * 59, 3 * 59, 59, 59), new Vector2(0, 0));
            earth=new Sprite(new Vector2(280, 170), planets, new Rectangle(3 * 59, 3 * 59, 59, 59), new Vector2(0, 0));
            mercury = new Sprite(new Vector2(150, 200), planets, new Rectangle(1 * 59, 3 * 59, 59, 59), new Vector2(0, 0));
            neptune=new Sprite(new Vector2(700, 100), planets, new Rectangle(0 * 59, 3 * 59, 59, 59), new Vector2(0, 0));
            mars=new Sprite(new Vector2(500, 220), planets, new Rectangle(1 * 59, 2 * 59, 59, 59), new Vector2(0, 0));

            starField = new StarField(this.Window.ClientBounds.Width,this.Window.ClientBounds.Height,200,spriteSheet,new Rectangle(0, 450, 2, 2));
            planetfly = new PlanetFlyby(Backgroundstars);
            asteroidManager = new AsteroidManager(
                10,
                spriteSheet,
                new Rectangle(0, 0, 50, 50),
                20,
                this.Window.ClientBounds.Width,
                this.Window.ClientBounds.Height);

            playerManager = new PlayerManager(
                spriteSheet,    
                new Rectangle(0, 150, 50, 50),    
                3,
                new Rectangle(
                    0,
                    0,
                    this.Window.ClientBounds.Width,
                    this.Window.ClientBounds.Height));

            enemyManager = new EnemyManager(
                spriteSheet,
                new Rectangle(0, 200, 50, 50),
                6,
                playerManager,
                new Rectangle(
                    0,
                    0,
                    this.Window.ClientBounds.Width,
                    this.Window.ClientBounds.Height));

            enemyManager.game1 = this;

            explosionManager = new ExplosionManager(
                spriteSheet,
                new Rectangle(0, 100, 50, 50),
                3,
                new Rectangle(0, 450, 2, 2));

            collisionManager = new CollisionManager(
                this,
                asteroidManager,
                playerManager,
                enemyManager,
                explosionManager);

            SoundManager.Initialize(Content);

            pericles14 = Content.Load<SpriteFont>(@"Fonts\Pericles14");

            EffectManager.Initialize(graphics, Content);
            EffectManager.LoadContent();

            // TODO: use this.Content to load your game content here
        }
        public void gethealth()
        {
            Healthpacks.Add(new Sprite(new Vector2(rand.Next(100, 700), -100), Healthpack, new Rectangle(0, 0, 50, 50), new Vector2(rand.Next(-11,11), rand.Next(50, 100))));
        }
        public void getjizz()
        {
            jizz.Add(new Sprite(new Vector2(rand.Next(100,700), -100), Vial, new Rectangle(0, 0, 20, 20), new Vector2(0,rand.Next(50,200))));
        }
        public string getplanet()
        {
            string what="foo";

            if (Planet == Planets.MARS) what = "MARS";
            if (Planet == Planets.EARTH) what = "EARTH";
            if (Planet == Planets.SUN) what = "SUN";
            if (Planet == Planets.MERCURY) what = "MERCURY";
            if (Planet == Planets.NEPTUNE) what = "NEPTUNE";

            return what;
        }
        public void Unload()
        {
           
            jizz.Clear();
        }
        public void resetGame()
        {
            playerManager.playerSprite.Location = playerStartLocation;
            foreach (Sprite asteroid in asteroidManager.Asteroids)
            {
                asteroid.Location = new Vector2(-500, -500);
            }
            enemyManager.Enemies.Clear();
            enemyManager.Active = true;
            playerManager.PlayerShotManager.Shots.Clear();
            enemyManager.EnemyShotManager.Shots.Clear();
            playerManager.Destroyed = false;
            Health += 100;
            if (Planet != Planets.NEPTUNE || Planet != Planets.SUN ||Planet != Planets.EARTH)
            {
                minutes = 2;
                seconds = 0;
            }
            if (Planet == Planets.NEPTUNE)
            {
                minutes = 1;
                seconds = 30;
            }
            if (Planet == Planets.EARTH)
            {
                minutes = 1;
                seconds = 0;
            }
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            MouseState ms = Mouse.GetState();
            
            leftMouseClicked = false;
            //Canclick = true;
            if (ms.LeftButton != ButtonState.Pressed)
                Canclick = true;
            if (ms.LeftButton == ButtonState.Pressed && Canclick == true)
            {
                leftMouseClicked = true;
                Canclick = false;
            }

            mouserect = new Rectangle(ms.X, ms.Y, 1, 1);

            IsMouseVisible = false;
            switch (gameState)
            {
                
                case GameStates.TitleScreen:
                    titleScreenTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (titleScreenTimer >= titleScreenDelayTime)
                    {
                        if ((Keyboard.GetState().IsKeyDown(Keys.Space)) ||
                            (GamePad.GetState(PlayerIndex.One).Buttons.A ==
                            ButtonState.Pressed))
                        {
                            playerManager.LivesRemaining = playerStartingLives;
                            playerManager.PlayerScore = 0;
                            resetGame();
                            gameState = GameStates.Levels;
                        }
                    }
                    break;

                case GameStates.Playing:

                    starField.Update(gameTime);
                    asteroidManager.Update(gameTime);
                    playerManager.Update(gameTime);
                    enemyManager.Update(gameTime);
                    explosionManager.Update(gameTime);
                    collisionManager.CheckCollisions();

                    if (Planet == Planets.SUN)
                    {
                        enemyManager.ChangeTime(2);
                    }
                    if (Planet == Planets.NEPTUNE || Planet == Planets.MARS || Planet == Planets.EARTH  || Planet==Planets.MERCURY)
                    {
                        enemyManager.ChangeTime(5);
                    }
                    if (Planet == Planets.NEPTUNE || Planet == Planets.SUN || Planet==Planets.EARTH)
                    {
                        timeupdate++;
                        if (timeupdate >= 60)
                        {
                            timeupdate = 0;
                            if (seconds >= 1)
                                seconds--;
                            else if (minutes >= 1)
                            {
                                minutes--;
                                seconds = 59;
                            }
                            else
                                gameState = GameStates.Win;
                        }
                    }
                    

                    if (Planet == Planets.MERCURY)
                    {
                        if (collisionManager.killcounter >= 30)
                        {
                            gameState = GameStates.Win;
                        }
                    }
                    if (Planet == Planets.MARS)
                    {


                        jizzingtime--;
                        if (jizzingtime <= 0)
                        {
                            getjizz();
                            jizzingtime = rand.Next(0, 300);
                        }
                        for (int i = 0; i < jizz.Count; i++)
                        {
                            jizz[i].Update(gameTime);
                            if (jizz[i].IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                            {
                                jizz.Remove(jizz[i]);
                                jizzCount++;
                            }
                            
                        }
                        if (jizzCount >= 20)
                        {
                            gameState = GameStates.Win;
                            marscheck = true;
                        }
                    }
                    if (Planet == Planets.NEPTUNE)
                    {
                        Health -= 4;

                        if (Health >= 5000)
                        {
                            Health = 5000;
                        }
                        if (Health <= 0)
                        {
                            gameState = GameStates.GameOver;
                            Health = 5000;

                        }

                        Healthtime--;
                        if (Healthtime <= 0)
                        {
                            gethealth();
                            Healthtime = rand.Next(0, 300);
                        }
                        for (int i = 0; i < Healthpacks.Count; i++)
                        {
                            if (Healthpacks[i].IsCircleColliding(playerManager.playerSprite.Center, playerManager.playerSprite.CollisionRadius))
                            {
                                Healthpacks.Remove(Healthpacks[i]);
                                Healthcount++;
                                Health += 1000;
                            }

                        }

                    }
                    if (Planet == Planets.EARTH)
                    {

                        foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
                        {
                             if (shot.IsBoxColliding(buttbox))
                            {
                                Health -= 2;
                                EffectManager.Effect("ShieldBounce").Trigger(shot.Center);
                            }
                        }
                        if (Health <= 0)
                        {
                            gameState = GameStates.GameOver;
                        }
                    }
                    


                    if (playerManager.Destroyed)
                    {
                        playerDeathTimer = 0f;
                        enemyManager.Active = false;
                        playerManager.LivesRemaining--;
                        if (playerManager.LivesRemaining < 0)
                        {
                            gameState = GameStates.GameOver;
                        }
                        else
                        {
                            gameState = GameStates.PlayerDead;
                        }
                    }

                    break;
                case GameStates.Win:
                    if (Planet == Planets.SUN)
                        suncheck = true;
                    if (Planet == Planets.EARTH)
                        earfcheck = true;
                    if (Planet == Planets.MERCURY)
                        mercurycheck = true;
                    if (Planet == Planets.MARS)
                        marscheck = true;
                    if (Planet == Planets.NEPTUNE)
                        neptunecheck = true;
                    seconds = 0;
                    minutes = 2;
                    wintimer--;
                    if (wintimer <= 0)
                        gameState = GameStates.Levels;
                    break;
                case GameStates.PlayerDead:
                    Unload();
                    playerDeathTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;

                    starField.Update(gameTime);
                    asteroidManager.Update(gameTime);
                    enemyManager.Update(gameTime);
                    playerManager.PlayerShotManager.Update(gameTime);
                    explosionManager.Update(gameTime);
                    
                    

                    if (playerDeathTimer >= playerDeathDelayTime)
                    {
                        resetGame();
                        gameState = GameStates.Playing;
                    }
                    
                    break;

                case GameStates.GameOver:
                    playerDeathTimer +=
                        (float)gameTime.ElapsedGameTime.TotalSeconds;
                    starField.Update(gameTime);
                    asteroidManager.Update(gameTime);
                    enemyManager.Update(gameTime);
                    playerManager.PlayerShotManager.Update(gameTime);
                    explosionManager.Update(gameTime);
                    if (playerDeathTimer >= playerDeathDelayTime)
                    {
                        gameState = GameStates.TitleScreen;
                    }
                    break;
                case GameStates.Levels:

                    IsMouseVisible = true;

                    if (sun.IsBoxColliding(mouserect) && leftMouseClicked)
                    {
                        Planet = Planets.SUN; gameState = GameStates.LoadLevel;
                        minutes = 2;
                    }
                    if (earth.IsBoxColliding(mouserect) && leftMouseClicked)
                    {
                        Planet = Planets.EARTH; gameState = GameStates.LoadLevel;
                        Health = 5000;
                        minutes = 1;
                    }
                    if (mercury.IsBoxColliding(mouserect) && leftMouseClicked)
                    {
                        Planet = Planets.MERCURY; gameState = GameStates.LoadLevel;
                        collisionManager.killcounter = 0;
                        
                    }
                    if (mars.IsBoxColliding(mouserect) && leftMouseClicked)
                    {
                        Planet = Planets.MARS; gameState = GameStates.LoadLevel;
                    }
                    if (neptune.IsBoxColliding(mouserect) && leftMouseClicked)
                    {
                        Planet = Planets.NEPTUNE; gameState = GameStates.LoadLevel;
                        minutes = 1; seconds = 15;
                    }
                    
                    break;
                case GameStates.LoadLevel:
                    gameState = GameStates.Playing;
                    break;
                    
            }
            for (int i = 0; i < Healthpacks.Count; i++)
            {
                Healthpacks[i].Update(gameTime);
            }

            planetfly.update(gameTime);
            
            EffectManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            if (gameState == GameStates.TitleScreen)
            {
                spriteBatch.Draw(background, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);
            }
            if (gameState == GameStates.Levels)
            {
                starField.Draw(spriteBatch);

                (sun).Draw(spriteBatch);
                (earth).Draw(spriteBatch);
                (mercury).Draw(spriteBatch);
                (neptune).Draw(spriteBatch);
                (mars).Draw(spriteBatch);
                if (suncheck)
                    spriteBatch.Draw(Check, new Rectangle(sun.BoundingBoxRect.X, sun.BoundingBoxRect.Y, 59, 59), Color.White);
                if (earfcheck)
                    spriteBatch.Draw(Check, new Rectangle(earth.BoundingBoxRect.X, earth.BoundingBoxRect.Y, 59, 59), Color.White);
                if (mercurycheck)
                    spriteBatch.Draw(Check, new Rectangle(mercury.BoundingBoxRect.X, mercury.BoundingBoxRect.Y, 59, 59), Color.White);
                if (marscheck)
                    spriteBatch.Draw(Check, new Rectangle(mars.BoundingBoxRect.X, mars.BoundingBoxRect.Y, 59, 59), Color.White);
                if (neptunecheck)
                    spriteBatch.Draw(Check, new Rectangle(neptune.BoundingBoxRect.X, neptune.BoundingBoxRect.Y, 59, 59), Color.White);
                if (sun.IsBoxColliding(mouserect))
                {
                    spriteBatch.DrawString(pericles14, "SUN", new Vector2(18, 270), Color.White);//survive  -done
                }
                if (earth.IsBoxColliding(mouserect))
                {
                    spriteBatch.DrawString(pericles14, "EARTH", new Vector2(280, 240), Color.White);//defend
                }
                if (mercury.IsBoxColliding(mouserect))
                {
                    spriteBatch.DrawString(pericles14, "MERCURY", new Vector2(131, 270), Color.White);//kill  -done
                }
                if (mars.IsBoxColliding(mouserect))
                {
                    spriteBatch.DrawString(pericles14, "MARS", new Vector2(505, 290), Color.White);//collect   -done
                }
                if (neptune.IsBoxColliding(mouserect))
                {
                    spriteBatch.DrawString(pericles14, "NEPTUNE", new Vector2(685, 170), Color.White);//survive (harder) -- collect health packs
                }
            }

            if ((gameState == GameStates.Playing) ||
                (gameState == GameStates.PlayerDead) ||
                (gameState == GameStates.GameOver)||
                (gameState == GameStates.Win))
            {
                starField.Draw(spriteBatch);
                planetfly.Draw(spriteBatch);
                if (Planet == Planets.EARTH)
                {
                    spriteBatch.Draw(buutt, buttbox, Color.White);
                }
                asteroidManager.Draw(spriteBatch);
                playerManager.Draw(spriteBatch);
                enemyManager.Draw(spriteBatch);
                explosionManager.Draw(spriteBatch);
                
                if (Planet == Planets.MARS)
                {
                    spriteBatch.DrawString(pericles14, "JIZZ COLLECTED: "+jizzCount+"/20", new Vector2(550, 10), Color.White);//survive
                    for (int i = 0; i < jizz.Count; i++)
                    {
                        jizz[i].Draw(spriteBatch);
                    }
                }
                if (Planet == Planets.NEPTUNE)
                {
                    for (int i = 0; i < Healthpacks.Count; i++)
                    {
                        Healthpacks[i].Draw(spriteBatch);
                    }
                }
                if (Planet == Planets.SUN)
                {
                    if (seconds<=9)
                        spriteBatch.DrawString(pericles14, "TIME TO SURVIVE: " + minutes + ": 0" + seconds, new Vector2(550, 10), Color.White);//survive
                    else
                        spriteBatch.DrawString(pericles14, "TIME TO SURVIVE: " + minutes + ": " + seconds, new Vector2(550, 10), Color.White);//survive
                }
                if (Planet == Planets.EARTH || Planet == Planets.NEPTUNE)
                {
                    if (seconds <= 9)
                        spriteBatch.DrawString(pericles14, "TIME TO SURVIVE: " + minutes + ": 0" + seconds, new Vector2(550, 40), Color.White);//survive
                    else
                        spriteBatch.DrawString(pericles14, "TIME TO SURVIVE: " + minutes + ": " + seconds, new Vector2(550, 40), Color.White);//survive
                }
                if (Planet == Planets.MERCURY)
                {
                    spriteBatch.DrawString(pericles14, "KILLS: " +collisionManager.killcounter + "/30", new Vector2(650, 10), Color.White);//survive
                }
                spriteBatch.DrawString( pericles14,"Score: " + playerManager.PlayerScore.ToString(),scoreLocation,Color.White);

                if (Planet == Planets.EARTH)
                {
                    spriteBatch.Draw(HpBar, new Rectangle(250, 10, Health/10, 20), Color.Green);
                }
                if (Planet == Planets.NEPTUNE)
                {
                    spriteBatch.Draw(HpBar, new Rectangle(250, 10, Health / 10, 20), Color.Green);
                }
                if (playerManager.LivesRemaining >= 0)
                {
                    spriteBatch.DrawString(pericles14,"Ships Remaining: " +playerManager.LivesRemaining.ToString(),livesLocation,Color.White);
                }
            }

            if ((gameState == GameStates.GameOver))
                spriteBatch.DrawString(pericles14,"G A M E  O V E R !",new Vector2(this.Window.ClientBounds.Width / 2 -pericles14.MeasureString("G A M E  O V E R !").X / 2,50),Color.White);
            if (gameState==GameStates.Win)
                spriteBatch.Draw(WinImg, new Rectangle(300, 200,200, 100), Color.White);


            
            spriteBatch.End();

            EffectManager.Draw();

            base.Draw(gameTime);
        }

    }
}
