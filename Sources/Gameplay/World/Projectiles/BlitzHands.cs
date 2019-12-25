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
using Microsoft.Xna.Framework.Design;
#endregion

namespace PROJECT_SpaceShooter
{
    public class BlitzHands : Projectile2d
    {
        public float hitbox;
        public float pullspeed;
        public float pulldelay;

        public bool ishit;

        public BlitzHands(Vector2 POS, Vector2 DIMS, bool ISCRITDMG, Vector2 OWNERPOS, Vector2 TARGET) : base("BlitzHands", POS, DIMS, ISCRITDMG, OWNERPOS, TARGET)
        {
            speed = 20;
            damage = 0;

            hitbox = 75;
            pullspeed = 1;
            pulldelay = 0;

            ishit = false;
        }

        public void Update(Vector2 OFFSET, Hero HERO, List<Boss> BOSS)
        {
            if (!ishit)
            {
                if (!GameGlobal.triggerICEWORLD)
                    pos = pos + realtarget * speed;
                else
                    pos = pos + realtarget * 1;
            }
            else
            {
                if (!GameGlobal.triggerICEWORLD)
                    pos = pos - realtarget * 15;
                else
                    pos = pos - realtarget * 1;
            }

            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }
            if (HitHero(HERO, BOSS))
            {
                done = true;
            }
        }

        public bool HitHero(Hero HERO, List<Boss> BOSS)
        {
            if (Global.GetDistance(pos, HERO.pos) < hitbox)
            {
                ishit = true;
                if (Global.GetDistance(BOSS[0].pos, HERO.pos) <= BOSS[0].hitdist)
                {
                    HERO.isstunned = false;
                    ishit = false;
                    return true;
                }

            }
            return false;
        }


        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
