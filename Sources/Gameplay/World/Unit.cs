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
    public class Unit : Basic2d
    {
        public bool isdead;

        public float speed, hitdist, currenthealth, maxhealth;

        public int life;


        public bool isstunned;

        public Unit(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            isdead = false;
            hitdist = 35.0f;
            speed = 1f;
            life = 1;

            currenthealth = 1;
            maxhealth = currenthealth;

            isstunned = false;
        }

        public virtual void GetHit(float DMG)
        {
            if (currenthealth >= DMG)
                currenthealth -= DMG;
            else
                currenthealth = 0;
        }

        public virtual bool IsDeadYet(float HEALTH)    //Chỉ hỏi chứ kh quyết định chết thật hay kh
        {
            if(HEALTH < 1)
                return true;
            return false;
        }

        public virtual void Dead()
        {
            life -= 1;
            if (life <= 0)
                isdead = true;
        }                   //Mỗi khi chết 1 lần

        public override void Update(Vector2 OFFSET)
        {

            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
