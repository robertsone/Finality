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
        public enemies(Sprite sprite,int tilex,int tiley)
        {
            this.tilex = tilex;
            this.tiley = tiley;
            this.speed = 120;
            this.sprite = sprite;
        }
        public void update(Tiles[,] grids)
        {
            if (grids[tilex, tiley].nextX > tilex)
            {
                sprite.Velocity = new Vector2(speed, 0);
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
                if (sprite.Center.Y <= grids[grids[tilex, tiley].nextX, grids[tilex, tiley].nextY].sprite.Center.Y)
                {
                    int tempy = grids[tilex, tiley].nextY;
                    int tempx = grids[tilex, tiley].nextX;
                    tilex = tempx;
                    tiley = tempy; return;
                }
            }
        }
    }
}
