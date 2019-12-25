#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace PROJECT_SpaceShooter
{
    public class Blitz : Boss
    {
        public float handdelay;

        public Blitz(Vector2 POS) : base("Blitz", POS, new Vector2(500, 500))
        {
            bossname = "blitz";
            speed = 1f;
            bossdmg = 51;
            currenthealth = 5000;
            maxhealth = currenthealth;

            handdelay = 1;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);  //Gọi ra sau vì isdead đc thay đổi trong Mob.Update
            lifecycle++;
            if (lifecycle == 2000)
                lifecycle = 0;
            if (isdead)
            {
                lifecycle = 0;
            }

            RocketGrab(HERO);
            Overdrive();
        }
        
        public void RocketGrab(Hero HERO)
        {
            if ((250 <= lifecycle && lifecycle <= 500) ||
                (750 <= lifecycle && lifecycle <= 1000) ||
                (1250 <= lifecycle && lifecycle <= 1500) ||
                (1750 <= lifecycle && lifecycle <= 2000))
            {
                Random rand = new Random();
                GameGlobal.BlitzHook(this, new BlitzHands(pos, new Vector2(150, 150), false, pos, HERO.pos + new Vector2(rand.Next(-350, 350), rand.Next(-350, 350))));
            }
        }

        public void Overdrive()
        {
            if ((200 <= lifecycle && lifecycle <= 250) ||
                (700 <= lifecycle && lifecycle <= 750) ||
                (1200 <= lifecycle && lifecycle <= 1250) ||
                (1700 <= lifecycle && lifecycle <= 1750))
            {
                speed = 10;
            }
            else
            {
                speed = 1;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
