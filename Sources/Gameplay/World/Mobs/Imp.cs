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
    public class Imp : Mob
    {
        public Imp(Vector2 POS) : base("Imp", POS, new Vector2(44, 34))
        {
            typemob = "imp";
            speed = 6.5f;
            mobdmg = 50;
            currenthealth = 100;
            maxhealth = currenthealth;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);  //Gọi ra sau vì isdead đc thay đổi trong Mob.Update
            //if (isdead)
            //    GameGlobal.highscrore += 50;
            if (2000 < GameGlobal.gametimepassed)
            {
                speed = 7.5f; //7.5f
            }
            if (7500 < GameGlobal.gametimepassed)
            {
                speed = 9.5f; //9.5f
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
