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
    class bullet
    {
        public Sprite bulletsprite;
        public int damage;
        public bool dead;
        public bullet(Sprite bullet,int damage){
            this.bulletsprite = bullet;
            this.damage = damage;
            this.dead = false;
        }
    }
}
