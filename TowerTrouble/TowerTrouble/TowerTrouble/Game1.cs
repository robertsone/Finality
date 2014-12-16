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

        public Tiles[,] grid = new Tiles[17, 17];
        public List<enemies> enemies = new List<enemies>();

        int tileWidth=17;
        int tileHeight=17;
        int tileSize=32;
        Texture2D Tiles;
        bool Canclick=false;
        bool leftMouseClicked = false;
        Rectangle mouserect;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public Tiles[,] CalculatePath(Tiles[,] tiles)
        {

            Tiles[,] New = new Tiles[17, 17];
            New[8, 7] = new Tiles(tiles[8,7].sprite, tiles[8,7].tower, tiles[8,7].collideablie,false);

            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (tiles[i, j].collideablie == true)
                    {
                        New[i,j]=new Tiles(tiles[i,j].sprite,tiles[i,j].tower,tiles[i,j].collideablie,true);
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
                                        New[i + 1, j] = new Tiles(tiles[i + 1, j].sprite, tiles[i + 1, j].tower, tiles[i + 1, j].collideablie, false);
                                        New[i + 1, j].changeto(i, j);
                                    }
                            }
                            if (i - 1 >= 0)
                            {
                               
                                    if (New[i - 1, j] == null)
                                    {
                                        New[i - 1, j] = new Tiles(tiles[i - 1, j].sprite, tiles[i - 1, j].tower, tiles[i - 1, j].collideablie, false);
                                        New[i - 1, j].changeto(i, j);
                                    }
                            }
                            if (j + 1 <= 16)
                            {
                                
                                    if (New[i, j + 1] == null)
                                    {
                                        New[i, j + 1] = new Tiles(tiles[i, j + 1].sprite, tiles[i, j + 1].tower, tiles[i, j + 1].collideablie, false);
                                        New[i, j + 1].changeto(i, j);
                                    }
                                
                            }
                            if (j - 1 >= 0)
                            {
                                
                                    if (New[i, j - 1] == null && j - 1 >= 0)
                                    {
                                        New[i, j - 1] = new Tiles(tiles[i, j - 1].sprite, tiles[i, j - 1].tower, tiles[i, j - 1].collideablie, false);
                                        New[i, j - 1].changeto(i, j);
                                    }
                                
                            }
                            
                        }
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
            Tiles = Content.Load<Texture2D>(@"Textures\grid");

            for (int i = 0; i < tileWidth; i++)
            {

                for (int j = 0; j < tileHeight; j++)
                {
                    grid[i,j] = new Tiles(new Sprite(new Vector2(i * tileSize, j * tileSize), Tiles, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)),"none",false,false);
                }

            }

            EffectManager.Initialize(graphics, Content);
            EffectManager.LoadContent();

            grid[0, 3] = new Tiles(new Sprite(new Vector2(0, 32), Tiles, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), "none", true, true);
            grid=CalculatePath(grid);
            
            enemies.Add(new enemies(new Sprite(new Vector2(0, 0), Tiles, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)),0,0));
            enemies[0].sprite.TintColor = Color.Gray;
            
        }

        public Tiles[,] placedTower(Tiles[,] grids, Rectangle mouserect)
        {
            for (int i = 0; i < tileWidth; i++)
            {
                for (int j = 0; j < tileHeight; j++)
                {
                    if (grid[i, j].sprite.IsBoxColliding(mouserect))
                    {
                        grid[i, j] = new Tiles(new Sprite(new Vector2(0, 32), Tiles, new Rectangle(0, 0, 32, 32), new Vector2(0, 0)), "none", true, true);
                    }
                }
            }
            return CalculatePath(grid);
        }
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {

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

            if (leftMouseClicked)
            {
                grid = placedTower(grid, mouserect);  
            }


            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update(grid);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].sprite.Update(gameTime);
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
                    grid[i,j].sprite.Draw(spriteBatch);
                }
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].sprite.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
