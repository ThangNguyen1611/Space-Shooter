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
    public class Elector : Mob
    {
        public Elector(Vector2 POS) : base("Elector", POS, new Vector2(44, 34))
        {
            typemob = "elector";
            speed = 8.5f;
            mobdmg = 20;
            currenthealth = 35;
            maxhealth = currenthealth;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);  //Gọi ra sau vì isdead đc thay đổi trong Mob.Update
            //if (isdead)
            //    GameGlobal.highscrore += 50;
            if (3000 < GameGlobal.gametimepassed)
            {
                speed = 9.25f; //9.25f
            }
            if (7500 < GameGlobal.gametimepassed)
            {
                speed = 10f; //10f
            }
            LightBolt(HERO);
        }

        public void LightBolt(Hero HERO)
        {
            //thêm sound ở đây
            if (Global.GetDistance(HERO.pos, pos) < 300)
            {
                myModel = Global.content.Load<Texture2D>("RedElector");
                speed = 14.025f; //14.025f
                if (3000 < GameGlobal.gametimepassed)
                {
                    speed = 15.2625f; //15.2625f
                }
                if (7500 < GameGlobal.gametimepassed)
                {
                    speed = 16.5f; //16.5f
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
