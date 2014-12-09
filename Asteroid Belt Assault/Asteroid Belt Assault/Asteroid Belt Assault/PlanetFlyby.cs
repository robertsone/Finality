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

namespace Asteroid_Belt_Assault
{
    class PlanetFlyby
    {
        public List<Sprite> planetsfly = new List<Sprite>();
        private int screenWidth = 800;
        private Random rand = new Random();
        public int delay;
        private Texture2D texture;

        public void update(GameTime gametime)
        {
            delay--;
            if (delay <= 0)
            {
                delay = rand.Next(1, 400);
                int startyoe = rand.Next(0,3);
                int tempx, tempy;
                if (startyoe == 0)
                {
                    planetsfly.Add(new Sprite(new Vector2(rand.Next(0, screenWidth), rand.Next(-300, -200)), texture, new Rectangle(660,19,200,200), new Vector2(rand.Next(-4, 4), rand.Next(30, 100))));
                }
                else if (startyoe == 1)
                {
                    planetsfly.Add(new Sprite(new Vector2(rand.Next(0, screenWidth), rand.Next(-300, -200)), texture, new Rectangle(341, 710, 80, 80), new Vector2(rand.Next(-4, 4), rand.Next(30, 100))));
                }
                else
                {
                    planetsfly.Add(new Sprite(new Vector2(rand.Next(0, screenWidth), rand.Next(-300, -200)), texture, new Rectangle(332, 612, 80, 80), new Vector2(rand.Next(-4, 4), rand.Next(30, 100))));
                }
                
            }
            foreach (Sprite star in planetsfly)
            {
                star.Update(gametime);
                
            }
            for (int i = 0; i < planetsfly.Count;i++)
            {
                if (planetsfly[i].Location.Y >= 1000)
                {
                    planetsfly.Remove(planetsfly[i]);
                }
            }
        }
        public PlanetFlyby(Texture2D texture)
        {
            this.texture = texture;
            this.planetsfly = new List<Sprite>();
            this.delay = rand.Next(1, 100);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite star in planetsfly)
            {
                star.Draw(spriteBatch);
            }
        }

    }
}
