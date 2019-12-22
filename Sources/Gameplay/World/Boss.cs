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
    public class Boss : Unit
    {
        public float bossdmg;
        public string bossname;
        public bool isappear;
        public bool iscollision;
        public int lifecycle;

        public Boss(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            isappear = false;
            hitdist = 260;
            iscollision = false;
            lifecycle = 0;
        }

        public virtual void Update(Vector2 OFFSET, Hero HERO)
        {
            AI(HERO);
            if (IsDeadYet(currenthealth))
                Dead();

            base.Update(OFFSET);
        }

        public virtual void AI(Hero HERO)
        {
            bool candamage = !GameGlobal.triggerinvinsible;  //phủ định của trigger
            if (!iscollision)
                pos += Global.RadialMovement(HERO.pos, pos, speed);
            if (Global.GetDistance(HERO.pos, pos) < hitdist)
            {
                Global.soundcontrol.PLaySound("HitImp");
                if (candamage)
                {
                    HERO.GetHit(bossdmg);
                }
                else
                {
                    HERO.GetHit(0);
                }
                HERO.pos += Global.RadialMovement(pos, HERO.pos, -HERO.speed);
                HERO.cursor.pos += Global.RadialMovement(pos, HERO.pos, -HERO.speed);
                iscollision = true;
            }
            if (Global.GetDistance(HERO.pos, pos) > hitdist * 1.05) 
            {
                iscollision = false;
            }
        }



        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
