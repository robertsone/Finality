using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.speed = 2;
            this.sprite = sprite;
        }
    }
}
