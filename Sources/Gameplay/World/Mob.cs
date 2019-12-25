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
    public class Mob : Unit
    {
        public bool iscollision;
        public float mobdmg;
        public string typemob;

        public Mob(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            iscollision = false;
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
            pos += Global.RadialMovement(HERO.pos, pos, speed);
            rot = Global.RotateTowards(pos, HERO.pos);
            if (Global.GetDistance(HERO.pos, pos) < HERO.hitdist)
            {
                Global.soundcontrol.PLaySound("MobHit");
                iscollision = true;
                if(candamage)
                    HERO.GetHit(mobdmg);
                else
                    HERO.GetHit(0);
            }
        }

        

        public override void Draw(Vector2 OFFSET)
        {

            base.Draw(OFFSET);
        }


    }
}
