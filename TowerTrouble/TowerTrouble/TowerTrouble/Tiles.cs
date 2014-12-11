using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Tiles(Sprite sprite,String tower,bool collide)
        {
            this.collideablie = collide;
            this.tower = tower;
            this.sprite = sprite;
            this.nextX = 0;
            this.nextY = 0;
        }
        public void changeto(int x, int y)
        {
            this.nextX = x;
            this.nextY = y;
        }
    }
}
