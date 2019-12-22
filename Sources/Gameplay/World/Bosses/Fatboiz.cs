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
    public class Fatboiz : Boss
    {
        public float bulletdelay;
        public float sodiersspawndelay;

        public Fatboiz(Vector2 POS) : base("Gunner", POS, new Vector2(500, 500))
        {
            bossname = "fatboiz";
            speed = 3.5f;
            bossdmg = 5;
            currenthealth = 5000;
            maxhealth = currenthealth;

            bulletdelay = 1;
            sodiersspawndelay = 1;
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

            GatlingGun(HERO);
            ForTheKING();
        }

        public void GatlingGun(Hero HERO) 
        {
            if ((250 <= lifecycle && lifecycle <= 500) || 
                (750 <= lifecycle && lifecycle <= 1000) || 
                (1250 <= lifecycle && lifecycle <= 1500) ||
                (1750 <= lifecycle && lifecycle <= 2000))
            {
                Random rand = new Random();
                GameGlobal.FatboizPassProjectiles(this, new FatboizProjectiles(pos, new Vector2(50, 50), false, pos, HERO.pos + new Vector2(rand.Next(-350, 350), rand.Next(-350, 350))));
            }
        }

        public void ForTheKING()
        {
            Random rand = new Random();
            if (150 <= lifecycle && lifecycle <= 250) 
            {
                GameGlobal.FatboizPassMob(this, new Imp(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
            }
            if (550 <= lifecycle && lifecycle <= 650)
            {
                GameGlobal.FatboizPassMob(this, new Elector(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
            }
            if (1050 <= lifecycle && lifecycle <= 1150)
            {
                int gunnerchance = rand.Next(100);
                if(gunnerchance <= 25) //1 cú nerf :v
                    GameGlobal.FatboizPassMob(this, new Gunner(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
            }
            if (1550 <= lifecycle && lifecycle <= 1600)
            {
                GameGlobal.FatboizPassMob(this, new Elector(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
                GameGlobal.FatboizPassMob(this, new Imp(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
                int gunnerchance = rand.Next(100);
                if (gunnerchance <= 25) //1 cú nerf :v
                    GameGlobal.FatboizPassMob(this, new Gunner(new Vector2(rand.Next((int)(pos.X - 300), (int)(pos.X + 300)), rand.Next((int)(pos.Y - 300), (int)(pos.Y + 300)))));
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
