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

        public Vector2 vectordirections;

        public Vector2 realtarget;

        public McTimer timer;
        
        public Projectile2d(string PATH, Vector2 POS, Vector2 DIMS, Vector2 OWNERPOS, Vector2 TARGET) : base(PATH, POS, DIMS)
        {
            speed = 35;
            done = false;
            vectordirections = TARGET - OWNERPOS; 
            realtarget.X = TARGET.X + vectordirections.X * 1000;
            realtarget.Y = TARGET.Y + vectordirections.Y * 1000;
            realtarget.Normalize();
            vectordirections.Normalize();

            rot = Global.RotateTowards(POS, new Vector2(TARGET.X, TARGET.Y));

            timer = new McTimer(5000);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            pos = pos + realtarget * speed;

            timer.UpdateTimer();
            if (timer.Test())
            {
                done = true;
            }

            if (HitSomething(UNITS))
            {
                done = true;
            }
        }

        public virtual bool HitSomething(List<Unit> UNITS)
        {
            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
