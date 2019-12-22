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
    public class FatboizProjectiles : Projectile2d
    {


        public FatboizProjectiles(Vector2 POS, Vector2 DIMS, bool ISCRITDMG, Vector2 OWNERPOS, Vector2 TARGET) : base("Fireball", POS, DIMS, ISCRITDMG, OWNERPOS, TARGET)
        {
            speed = 20;
            damage = 35;
        }

        public void Update(Vector2 OFFSET, Hero HERO)
        {
            if (!GameGlobal.triggerICEWORLD)
                pos = pos + realtarget * speed;
            else
                pos = pos + realtarget * 1;

            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }
            if (HitHero(HERO))
            {
                done = true;
            }
        }

        public bool HitHero(Hero HERO)
        {
            if (Global.GetDistance(pos, HERO.pos) < HERO.hitdist)
            {
                bool candamage = !GameGlobal.triggerinvinsible;  //phủ định của trigger
                if (candamage)
                    HERO.GetHit(damage);
                else
                    HERO.GetHit(0);
                return true;
            }
            return false;
        }


        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
