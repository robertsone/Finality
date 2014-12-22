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
 
    public class enemies
    {
        public Sprite sprite;
        public int tilex;
        public int tiley;
        public int speed;
        public int framecounter;
        public int frame;
        public String der;
        public bool Remove;
        public int health;
        public bool dead;
        public enemies(Sprite sprite,int tilex,int tiley,int health)
        {
            this.tilex = tilex;
            this.tiley = tiley;
            this.speed = 120;
            this.sprite = sprite;
            this.framecounter = 0;
            this.frame = 0;
            this.der = "up";
            this.Remove = false;
            this.health = health;
            this.dead = false;
        }
        public void changeImage(String der, Texture2D enemies)
        {
            if (der == "down")
            {
                sprite = new Sprite(sprite.Location, sprite.Texture, new Rectangle(frame * 32, 96, 32, 32), sprite.Velocity);
            }
            else if (der == "up")
            {
                sprite = new Sprite(sprite.Location, sprite.Texture, new Rectangle(frame * 32, 0, 32, 32), sprite.Velocity);
            }
            else if (der == "right")
            {
                sprite = new Sprite(sprite.Location, sprite.Texture, new Rectangle(frame * 32, 32, 32, 32), sprite.Velocity);
            }
            else 
            {
                sprite = new Sprite(sprite.Location, sprite.Texture, new Rectangle(frame * 32, 64, 32, 32), sprite.Velocity);
            }
        }
        public void update(Tiles[,] grids,Texture2D enemy)
        {
            
            framecounter++;
            bool willUpdate=false;
            if (framecounter >= 5)
            {
                framecounter = 0;
                willUpdate = true;
                frame += 1;
                if (frame > 2)
                {
                    frame = 0;
                }
            }

            if (grids[tilex, tiley].nextX > tilex)
            {
                sprite.Velocity = new Vector2(speed, 0);
                changeImage("right",enemy);
                if (sprite.Center.X >= grids[grids[tilex, tiley].nextX, grids[tilex, tiley].nextY].sprite.Center.X)
                {
                    int tempy = grids[tilex, tiley].nextY;
                    int tempx = grids[tilex, tiley].nextX;
                    tilex = tempx;
                    tiley = tempy;
                    return;
                }
            }
            if (grids[tilex, tiley].nextX < tilex)
            {
                sprite.Velocity = new Vector2(speed *-1, 0);
                changeImage("left", enemy);
                if (sprite.Center.X <= grids[grids[tilex, tiley].nextX, grids[tilex, tiley].nextY].sprite.Center.X)
                {
                    int tempy = grids[tilex, tiley].nextY;
                    int tempx = grids[tilex, tiley].nextX;
                    tilex = tempx;
                    tiley = tempy; return;
                }
            }
            if (grids[tilex, tiley].nextY > tiley)
            {
                sprite.Velocity = new Vector2(0,speed);
                changeImage("down", enemy);
                if (sprite.Center.Y>= grids[grids[tilex, tiley].nextX, grids[tilex, tiley].nextY].sprite.Center.Y)
                {
                    int tempy = grids[tilex, tiley].nextY;
                    int tempx = grids[tilex, tiley].nextX;
                    tilex = tempx;
                    tiley = tempy; 
                    return;
                }
            }
            if (grids[tilex, tiley].nextY < tiley)
            {
                sprite.Velocity = new Vector2(0, speed*-1);
                changeImage("up", enemy);
                if (sprite.Center.Y <= grids[grids[tilex, tiley].nextX, grids[tilex, tiley].nextY].sprite.Center.Y)
                {
                    int tempy = grids[tilex, tiley].nextY;
                    int tempx = grids[tilex, tiley].nextX;
                    tilex = tempx;
                    tiley = tempy; return;
                }
            }
            if (tilex == 8 && tiley == 7)
            {
                this.Remove = true;   
            }
        }
    }
}
