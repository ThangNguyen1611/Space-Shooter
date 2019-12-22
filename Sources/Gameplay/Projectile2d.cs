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
    public class Projectile2d : Basic2d
    {
        public bool done;
        public float speed;
        public float damage;
        public bool iscritdmg;

        public Vector2 vectordirections;

        public Vector2 realtarget;

        public McTimer timer;
        
        public Projectile2d(string PATH, Vector2 POS, Vector2 DIMS, bool ISCRITDMG, Vector2 OWNERPOS, Vector2 TARGET) : base(PATH, POS, DIMS)
        {
            done = false;
            iscritdmg = ISCRITDMG;
            vectordirections = TARGET - OWNERPOS; 
            realtarget.X = TARGET.X + vectordirections.X * 1000;
            realtarget.Y = TARGET.Y + vectordirections.Y * 1000;
            realtarget.Normalize();
            vectordirections.Normalize();

            rot = Global.RotateTowards(POS, new Vector2(TARGET.X, TARGET.Y));

            timer = new McTimer(5000);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS, List<Boss> BOSS)
        {
            pos = pos + realtarget * speed;
            
            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }
            if (HitMobs(UNITS, iscritdmg)) 
            {
                done = true;
            }
            if(HitBoss(BOSS, iscritdmg))
            {
                done = true;
            }
        }

        public virtual bool HitMobs(List<Unit> UNITS, bool ISCRIT)
        {
            for (int i = 0; i < UNITS.Count; i++)
            {
                if (Global.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitdist)
                {
                    if(!ISCRIT)
                        UNITS[i].GetHit(damage);
                    else
                        UNITS[i].GetHit(damage * 2);
                    return true;
                }
            }
            return false;
        }

        public virtual bool HitBoss(List<Boss> BOSS, bool ISCRIT)
        {
            if(BOSS.Count > 0)
                if (Global.GetDistance(pos, BOSS[0].pos) < BOSS[0].hitdist)
                {
                    if (!ISCRIT)
                        BOSS[0].GetHit(damage);
                    else
                        BOSS[0].GetHit(damage * 2);
                    return true;
                }
            return false;
        }

        public bool HitSomething(List<Unit> UNITS, List<Boss> BOSS)
        {
            for (int i = 0; i < UNITS.Count; i++)
            {
                if (Global.GetDistance(pos, UNITS[i].pos) < UNITS[i].hitdist)
                {
                    return true;
                }
            }
            if (BOSS.Count > 0)
                if (Global.GetDistance(pos, BOSS[0].pos) < BOSS[0].hitdist)
                {
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
