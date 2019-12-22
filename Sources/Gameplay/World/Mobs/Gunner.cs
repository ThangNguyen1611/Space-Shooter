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
    public class Gunner : Mob
    {
        public float bulletdelay;
        public Gunner(Vector2 POS) : base("Gunner", POS, new Vector2(44, 34))
        {
            typemob = "gunner";
            speed = 3.5f;
            mobdmg = 90;
            currenthealth = 300;
            maxhealth = currenthealth;

            bulletdelay = 1;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);  //Gọi ra sau vì isdead đc thay đổi trong Mob.Update
            //if (isdead)
            //    GameGlobal.highscrore += 150;
            if (3000 < GameGlobal.gametimepassed)
            {
                speed = 3.75f;
            }
            if (7500 < GameGlobal.gametimepassed)
            {
                speed = 4;
            }
            SprayAndPray(HERO);
        }

        public void SprayAndPray(Hero HERO) //Chuột chít chíu chíu
        {
            if (Global.GetDistance(HERO.pos, pos) < 700)
            {
                speed = 2;
                GameGlobal.GunnerPassProjectiles(this, new GunnerProjectiles(pos, new Vector2(35, 35), false, pos, HERO.pos));
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
