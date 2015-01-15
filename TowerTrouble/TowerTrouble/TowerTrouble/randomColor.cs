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
    class randomColor
    {
        public int delay;
        public Random rand = new Random();
        public Color normal = Color.White;
        public randomColor()
        {
            this.normal=Color.White;
        }
        public Color getcolor()
        {
            delay--;
            if (delay <= 0)
            {
                delay = 5;
                int num = rand.Next(25);
                if (num == 0) { normal=Color.Pink;};
                if (num == 1) { normal=Color.HotPink;};
                if (num == 2) { normal=Color.Honeydew;};
                if (num == 3) { normal=Color.Red;};
                if (num == 4) { normal=Color.Moccasin;};
                if (num == 5) { normal=Color.Gold;};
                if (num == 6) { normal=Color.CornflowerBlue;};
                if (num == 7) { normal=Color.Yellow;};
                if (num == 8) { normal=Color.Blue;};
                if (num == 9) { normal=Color.Green;};
                if (num == 10) { normal=Color.LightGreen;};
                if (num == 11) { normal=Color.LightCoral;};
                if (num == 12) { normal=Color.Cyan;};
                if (num == 13) { normal=Color.DarkCyan;};
                if (num == 14) { normal=Color.BurlyWood;};
                if (num == 15) { normal=Color.DarkKhaki;};
                if (num == 16) { normal=Color.DarkGoldenrod;};
                if (num == 17) { normal=Color.DarkGray;};
                if (num == 18) { normal=Color.Black;};
                if (num == 19) { normal=Color.Beige;};
                if (num == 20) { normal=Color.Azure;};
                if (num == 21) { normal=Color.Magenta;};
                if (num == 22) { normal=Color.Lime;};
                if (num == 23) { normal=Color.OldLace;};
                if (num == 24) { normal = Color.WhiteSmoke; };
            }
            return normal;
        }
    }
}
