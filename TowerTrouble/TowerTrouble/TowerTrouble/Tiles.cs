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
    public class Tiles
    {
        public bool collideablie;
        public string tower;
        public Sprite sprite;
        public int x;
        public int y;
        public int nextX=0;
        public int nextY = 0;
        public int thisX = 0;
        public int thisY = 0;
        public bool render;
        public bool fail;
        public int range;
        public int shottimer;
        public bool isfireing;
        public Vector2 at;
        public Tiles(Sprite sprite,String tower,bool collide,bool render,int range)
        {
            this.collideablie = collide;
            this.tower = tower;
            this.sprite = sprite;
            this.nextX = 0;
            this.nextY = 0;
            this.render = render;
            this.fail = false;
            this.range = range;
            this.shottimer = 0;
            this.isfireing = false;
            this.at=new Vector2(0,0);
        }
        public void changeto(int x, int y)
        {
            this.nextX = x;
            this.nextY = y;
        }
    }
}
