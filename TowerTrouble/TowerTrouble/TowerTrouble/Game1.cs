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

namespace TowerTrouble
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Tiles[,] grid = new Tiles[17, 15];
        public List<enemies> enemies = new List<enemies>();
        SpriteFont Font1;
        Random rand = new Random();
        int tileWidth=17;
        int tileHeight=15;
        int tileSize=32;
        Texture2D Tiles;
        Texture2D Gemcraft;
        Texture2D Orbs;
        Texture2D wood;
        Texture2D sprites;
        Texture2D grass2;
        Texture2D range;
        Texture2D bannana;
        Texture2D brick;
        Texture2D lazor;
        Texture2D bullet;
        Texture2D red;
        Vector2 isHovering;
        Texture2D Enemy, grass;
        bool Canclick=false;
        bool leftMouseClicked = false;
        Rectangle mouserect;
        public int money;
        public int lives;
        public int delay=-400;
        Sprite machineGun;
        Sprite machineGun2;
        Sprite wall;
        Sprite bana;
        string isplacing;
        int machineGunPrice = 10;
        int machineGun2Price = 50;
        int health = 1;
        int healthtimer = 0;
        public bool done=false;
        KeyboardState keyState;
        List<bullet> bullets=new List<bullet>();
        int gun1cost = 10;
        int gun2cost = 50;
        int banacost = 1000;
        int wallcost = 1;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public Tiles[,] CalculatePath(Tiles[,] tiles)
        {

            Tiles[,] New = new Tiles[17, 15];
            New[8, 7] = new Tiles(new Sprite(new Vector2(8*tileSize,7*tileSize), Orbs, new Rectangle(32, 64, 32, 32), new Vector2(0, 0)), "orb", false, false,0,0,0);

            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (tiles[i, j].collideablie == true)
                    {
                        New[i, j] = new Tiles(tiles[i, j].sprite, tiles[i, j].tower, tiles[i, j].collideablie, true, grid[i, j].range, grid[i, j].reset, grid[i, j].damage);
                    }
                }
            }
            int w = 0;
            while (w<=240)
            {
                w++;
                for (int i = 0; i < tileWidth; i++)
                {
                    for (int j = 0; j < tileHeight; j++)
                    {
                        if (New[i, j] != null && New[i, j].render == false)
                        {

                            if (i + 1 <= 16)
                            {
                                    if (New[i + 1, j] == null)
                                    {
                                        New[i + 1, j] = new Tiles(tiles[i + 1, j].sprite, tiles[i + 1, j].tower, tiles[i + 1, j].collideablie, false, 0, 0, 0);
                                        New[i + 1, j].changeto(i, j);
                                    }
                            }
                            if (i - 1 >= 0)
                            {
                               
                                    if (New[i - 1, j] == null)
                                    {
                                        New[i - 1, j] = new Tiles(tiles[i - 1, j].sprite, tiles[i - 1, j].tower, tiles[i - 1, j].collideablie, false, 0, 0, 0);
                                        New[i - 1, j].changeto(i, j);
                                    }
                            }
                            if (j + 1 <= 14)
                            {
                                
                                    if (New[i, j + 1] == null)
                                    {
                                        New[i, j + 1] = new Tiles(tiles[i, j + 1].sprite, tiles[i, j + 1].tower, tiles[i, j + 1].collideablie, false, 0, 0, 0);
                                        New[i, j + 1].changeto(i, j);
                                    }
                                
                            }
                            if (j - 1 >= 0)
                            {
                                
                                    if (New[i, j - 1] == null && j - 1 >= 0)
                                    {
                                        New[i, j - 1] = new Tiles(tiles[i, j - 1].sprite, tiles[i, j - 1].tower, tiles[i, j - 1].collideablie, false, 0, 0, 0);
                                        New[i, j - 1].changeto(i, j);
                                    }
                                
                            }
                            
                        }
                    }

                }
            }
            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (grid[i, j].render==null)
                    {
                        New[0, 0].fail = true;
                    }
                }
            }
            return New;
        }
        protected override void Initialize()
        {
            

            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            range = Content.Load<Texture2D>(@"this game\range");
            bullet = Content.Load<Texture2D>(@"this game\bulley");
            Tiles = Content.Load<Texture2D>(@"Textures\grid");
            Font1 = Content.Load<SpriteFont>(@"this game\SpriteFont1");
            Orbs = Content.Load<Texture2D>(@"this game\orbs");
            Enemy = Content.Load<Texture2D>(@"this game\spritesheet");
            grass = Content.Load<Texture2D>(@"this game\grass");
            grass2 = Content.Load<Texture2D>(@"this game\grass2");
            wood = Content.Load<Texture2D>(@"this game\wood");
            sprites = Content.Load<Texture2D>(@"this game\paths_and_money");
            Gemcraft = Content.Load<Texture2D>(@"this game\Gemcraft");
            red = Content.Load<Texture2D>(@"this game\red");
            brick = Content.Load<Texture2D>(@"this game\sprite_bricks_tutorial_1");
            lazor = Content.Load<Texture2D>(@"this game\lazor");
            bannana = Content.Load<Texture2D>(@"this game\banana-clip-art-2");
            money = 200;
            lives = 20;
            isplacing="none";
            for (int i = 0; i < tileWidth; i++)
            {

                for (int j = 0; j < tileHeight; j++)
                {
                    grid[i, j] = new Tiles(new Sprite(new Vector2(i * tileSize, j * tileSize), grass, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), "none", false, false, 0, 0, 0);
                }

            }

            EffectManager.Initialize(graphics, Content);
            EffectManager.LoadContent();

            bana = new Sprite(new Vector2(545, 250), bannana, new Rectangle(0, 0, 32, 32), new Vector2(0, 0));
            wall = new Sprite(new Vector2(545, 200), brick, new Rectangle(0, 0, 32, 32), new Vector2(0, 0));
            machineGun = new Sprite(new Vector2(545, 100), sprites, new Rectangle(90, 287, 32, 32), new Vector2(0, 0));
            machineGun2 = new Sprite(new Vector2(545, 150), sprites, new Rectangle(156, 287, 32, 32), new Vector2(0, 0));
            grid = CalculatePath(grid);
            //enemies.Add(new enemies(new Sprite(new Vector2(0, 0), Enemy, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), 0, 0));
            //enemies[0].sprite.TintColor = Color.Gray;
            
        }
        public int findClosest(Tiles tower, List<enemies> enemies)
        {
            int num=-1;
            int distance = 1000;
            for (int i = 0; i < enemies.Count(); i++)
            {
                if (enemies[i].sprite.IsCircleColliding(tower.sprite.Center,tower.range/2))
                {
                    int first=Convert.ToInt32(tower.sprite.Center.X-enemies[i].sprite.Center.X);
                    first=first*first;
                    int secound=Convert.ToInt32(tower.sprite.Center.Y-enemies[i].sprite.Center.Y);
                    if (Math.Sqrt(first + secound) <= distance)
                    {
                        distance = Convert.ToInt32(Math.Sqrt(first + secound));
                        num = i;
                    }
                }
            }
            return num;
        }
        public Tiles[,] placedTower(Tiles[,] grids, Rectangle mouserect,string Tower)
        {
            Tiles[,] yellow = new Tiles[tileWidth,tileHeight];

            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    yellow[i, j] = grids[i, j];

                    if (grids[i, j].sprite.IsBoxColliding(mouserect))
                    {
                        if (Tower == "MachineGun")
                        {
                            grids[i, j] = new Tiles(new Sprite(new Vector2(i * 32, j * 32), sprites, new Rectangle(90, 287, 32, 32), new Vector2(0, 0)), Tower, true, true, 150, 20, 18);
                        }
                        if (Tower == "MachineGun2")
                        {
                            grids[i, j] = new Tiles(new Sprite(new Vector2(i * 32, j * 32), sprites, new Rectangle(156, 287, 32, 32), new Vector2(0, 0)), Tower, true, true, 300, 6, 10);
                        }
                        if (Tower == "wall")
                        {
                            grids[i, j] = new Tiles(new Sprite(new Vector2(i * 32, j * 32), brick, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), Tower, true, true, 0, 6, 10);
                        }
                        if (Tower == "bana")
                        {
                            grids[i, j] = new Tiles(new Sprite(new Vector2(i * 32, j * 32), bannana, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), Tower, true, true, 650, 180, 500);
                        }
                    }
                }
            }
            grids = CalculatePath(grid);
            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (grids[i,j]==null)
                    {
                        return yellow;
                    }
                }
            }
            return grids; 
        }
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (!done)
            {
                delay++;
                healthtimer++;
                if (healthtimer >= 18)
                {
                    healthtimer = 0;
                    health++;
                }
                if (delay >= 50)
                {
                    enemies.Add(new enemies(new Sprite(new Vector2(0, 0), Enemy, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), 0, 0,health));
                    delay = rand.Next(150);
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                MouseState ms = Mouse.GetState();

                leftMouseClicked = false;
                if (ms.LeftButton != ButtonState.Pressed)
                    Canclick = true;
                if (ms.LeftButton == ButtonState.Pressed && Canclick == true)
                {
                    leftMouseClicked = true;
                    Canclick = false;
                }
                mouserect = new Rectangle(ms.X, ms.Y, 1, 1);
                IsMouseVisible = true;

                isHovering = new Vector2(-1, -1);

                for (int i = 0; i < tileWidth; i++)
                {

                    for (int j = 0; j < tileHeight; j++)
                    {
                        if (grid[i, j].sprite.IsBoxColliding(mouserect))
                        {
                            isHovering = new Vector2(i, j);
                        }
                    }
                }

                if (isplacing != "none" && leftMouseClicked)
                {
                    if (isplacing == "MachineGun")
                    {

                        grid = placedTower(grid, mouserect, "MachineGun");
                        isplacing = "none";
                        money -= gun1cost;
                        gun1cost += gun1cost / 10;

                    } if (isplacing == "bana")
                    {

                        grid = placedTower(grid, mouserect, "bana");
                        isplacing = "none";
                        money -= banacost;
                        banacost += banacost / 10;

                    }
                    if (isplacing == "wall")
                    {

                        grid = placedTower(grid, mouserect, "wall");
                        KeyboardState keyState = Keyboard.GetState();
                        if (keyState.IsKeyDown(Keys.LeftShift)&&money>=wallcost)
                        {

                        }
                        else
                        {
                            isplacing = "none";
                        }
                        money -= wallcost;
                        wallcost += wallcost / 10;

                    }
                    if (isplacing == "MachineGun2")
                    {
                        grid = placedTower(grid, mouserect, "MachineGun2");
                        money -= gun2cost;
                        gun2cost += gun2cost / 10;
                        isplacing = "none";

                    }
                }

                if (leftMouseClicked)
                {
                    if (machineGun.IsBoxColliding(mouserect) && money >= gun1cost)
                    {
                        isplacing = "MachineGun";
                    }
                    if (machineGun2.IsBoxColliding(mouserect) && money >= gun2cost)
                    {
                        isplacing = "MachineGun2";
                    }
                    if (wall.IsBoxColliding(mouserect) && money >= wallcost)
                    {
                        isplacing = "wall";
                    }
                    if (bana.IsBoxColliding(mouserect) && money >= banacost)
                    {
                        isplacing = "bana";
                    }
                }


                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].update(grid, Enemy);
                    if (enemies[i].Remove)
                    {
                        lives -= 1;
                        if (lives <= 0)
                        {
                            done = true;
                        }
                        enemies.Remove(enemies[i]);
                    }
                }

                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].sprite.Update(gameTime);
                }

                for (int i = 0; i < tileWidth; i++)
                {

                    for (int j = 0; j < tileHeight; j++)
                    {
                        if (grid[i, j].tower != "none" && grid[i, j].tower != "wall" && grid[i, j].tower != "orb")
                        {
                            if (grid[i, j].shottimer <= 0)
                            {
                                int close = findClosest(grid[i, j], enemies);
                                if (close != -1)
                                {
                                    Vector2 vec = grid[i, j].sprite.Center - enemies[close].sprite.Center;
                                    float rot = (float)(Math.Atan2(vec.Y, vec.X)) + MathHelper.PiOver2;

                                    grid[i, j].sprite.Rotation = rot;
                                    if (grid[i, j].shottimer <= 0)
                                    {
                                        Vector2 vel = enemies[close].sprite.Center - grid[i, j].sprite.Center;
                                        vel.Normalize();
                                        vel *= new Vector2(500, 500);
                                        bullets.Add(new bullet(new Sprite(grid[i, j].sprite.Center, bullet, new Rectangle(0, 0, 8, 8), vel), grid[i, j].damage));
                                        grid[i, j].shottimer = grid[i, j].reset;
                                    }
                                }
                            }
                            if (grid[i, j].shottimer > 0)
                            {
                                grid[i, j].shottimer--;
                            }

                        }
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].bulletsprite.BoundingBoxRect.X >= 1000 || bullets[i].bulletsprite.BoundingBoxRect.X <= -100 || bullets[i].bulletsprite.BoundingBoxRect.Y >= 1000 || bullets[i].bulletsprite.BoundingBoxRect.Y <= -100)
                    {
                        bullets[i].dead = true;
                    }
                    bullets[i].bulletsprite.Update(gameTime);
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if (bullets[i].damage == 500)
                        {
                            EffectManager.Effect("ShieldsUp").Trigger(bullets[i].bulletsprite.Center); EffectManager.Effect("PulseTracker").Trigger(bullets[i].bulletsprite.Center); EffectManager.Effect("PulseTracker").Trigger(bullets[i].bulletsprite.Center); EffectManager.Effect("PulseTracker").Trigger(bullets[i].bulletsprite.Center);
                        }
                        if (bullets[i].bulletsprite.IsBoxColliding(enemies[j].sprite.BoundingBoxRect))
                        {
                            if (bullets[i].damage != 500)
                            {
                                bullets[i].dead = true;
                            }
                            enemies[j].health -= bullets[i].damage;
                            if (enemies[j].health <= 0)
                            {

                                enemies[j].dead = true;
                                if (bullets[i].damage == 500)
                                {
                                    EffectManager.Effect("BasicExplosion").Trigger(bullets[i].bulletsprite.Center);
                                }
                            }
                            
                        }
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].dead == true)
                    {
                        bullets.Remove(bullets[i]);
                    }
                }
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (enemies[j].dead == true)
                    {

                        EffectManager.Effect("Ship Cannon Fire").Trigger(enemies[j].sprite.Center); EffectManager.Effect("Ship Cannon Fire").Trigger(enemies[j].sprite.Center);
                        enemies.Remove(enemies[j]);
                        money += (health / 100);
                    }
                }

                EffectManager.Update(gameTime);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (isplacing != "none" && grid[i,j].tower=="none")
                    {
                        spriteBatch.Draw(grass2, new Rectangle(i * 32, j * 32, 32, 32), Color.White);
                    }
                    else if (grid[i, j].tower != "none" && isplacing != "none")
                    {
                        spriteBatch.Draw(red, new Rectangle(i * 32, j * 32, 32, 32), Color.White);
                        grid[i, j].sprite.Draw(spriteBatch);
                    }
                    else
                    {
                        spriteBatch.Draw(grass, new Rectangle(i * 32, j * 32, 32, 32), Color.White);
                        grid[i, j].sprite.Draw(spriteBatch);
                    }
                    
                }
            }
            new Sprite(new Vector2(8 * 32, 7 * 32), grass, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)).Draw(spriteBatch);
            new Sprite(new Vector2(540, 0), wood, new Rectangle(0, 0, 260, 480), new Vector2(0, 0)).Draw(spriteBatch); //wood
            new Sprite(new Vector2(545, 5), sprites, new Rectangle(73, 600, 50, 60), new Vector2(0, 0)).Draw(spriteBatch); //money
            new Sprite(new Vector2(700, 30), sprites, new Rectangle(133, 152, 50, 20), new Vector2(0, 0)).Draw(spriteBatch); //lives
            spriteBatch.DrawString(Font1, Convert.ToString(money), new Vector2(600, 30), Color.Gold);//cash
            spriteBatch.DrawString(Font1, Convert.ToString(gun1cost), new Vector2(600, 100), Color.Gold);//1cost
            spriteBatch.DrawString(Font1, Convert.ToString(gun2cost), new Vector2(600, 150), Color.Gold);//2cost
            spriteBatch.DrawString(Font1, Convert.ToString(wallcost), new Vector2(600, 200), Color.Gold);//wallcost
            spriteBatch.DrawString(Font1, Convert.ToString(banacost), new Vector2(600, 250), Color.Gold);//bana
            machineGun.Draw(spriteBatch);//machinegun
            machineGun2.Draw(spriteBatch);//dualgun
            wall.Draw(spriteBatch);//wall
            bana.Draw(spriteBatch);
            spriteBatch.DrawString(Font1, Convert.ToString(lives), new Vector2(755, 30), Color.Gold);//lives
            spriteBatch.DrawString(Font1, "Health of each enemy:  "+Convert.ToString(health), new Vector2(550, 70), Color.Gold);//health

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].sprite.Draw(spriteBatch);
            }
            grid[8, 7].sprite.Draw(spriteBatch);
            if (isHovering.X != -1)
            {
                int x = Convert.ToInt32(isHovering.X);
                int y = Convert.ToInt32(isHovering.Y);
                int xcord = Convert.ToInt32( grid[x, y].sprite.Center.X);
                int ycord = Convert.ToInt32(grid[x, y].sprite.Center.Y);

                spriteBatch.Draw(range, new Rectangle(xcord - (grid[x, y].range / 2), ycord - (grid[x, y].range / 2),grid[x,y].range,grid[x,y].range), Color.White);
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].bulletsprite.Draw(spriteBatch);
            }
            if (isplacing == "wall")
            {
                new Sprite(new Vector2(mouserect.X - 10, mouserect.Y - 10), brick, new Rectangle(0,0,32,32), new Vector2(0, 0)).Draw(spriteBatch);
            }
            if (isplacing == "MachineGun")
            {
                new Sprite(new Vector2(mouserect.X-10,mouserect.Y-10), sprites, new Rectangle(90, 287, 32, 32), new Vector2(0, 0)).Draw(spriteBatch);
            }
            if (isplacing == "MachineGun2")
            {
                new Sprite(new Vector2(mouserect.X - 20, mouserect.Y - 20), sprites, new Rectangle(156, 287, 32, 32), new Vector2(0, 0)).Draw(spriteBatch);
            }
            if (isplacing == "bana")
            {
                new Sprite(new Vector2(mouserect.X - 20, mouserect.Y - 20), bannana, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)).Draw(spriteBatch);
            }

            spriteBatch.End();
            EffectManager.Draw();
            base.Draw(gameTime);
        }
    }
}
