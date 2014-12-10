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

        public Tiles(Sprite sprite)
        {
            this.collideablie = false;
            this.tower = "None";
            this.sprite = sprite;
        }
        public void Generate(int x,int y)
        {
            
        }
    }
}
