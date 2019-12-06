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

        public float speed , currenthealth, maxhealth;
        public int life;

        public Unit(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            isdead = false;
            speed = .1f;
            life = 1;

            currenthealth = 1;
            maxhealth = currenthealth;
        }

        public virtual void GetHit(float DMG)
        {
            currenthealth -= DMG;
            IsWeDeadYet(currenthealth);
        }

        public virtual bool IsWeDeadYet(float OURHEALTH)    //Chỉ hỏi chứ kh quyết định chết thật hay kh
        {
            if(OURHEALTH <= 0)
                return true;
            return false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
