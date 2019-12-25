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
    public class Cable : Boss
    {
        public float healthmarker;
        public Vector2 fakeheropos;

        public Cable(Vector2 POS) : base("Cable", POS, new Vector2(500, 500))
        {
            bossname = "cable";
            speed = 3.5f;
            bossdmg = 5;
            currenthealth = 7000;
            maxhealth = currenthealth;

            healthmarker = 0;
            fakeheropos = pos;
        }

        public override void Update(Vector2 OFFSET, Hero HERO)
        {
            base.Update(OFFSET, HERO);  //Gọi ra sau vì isdead đc thay đổi trong Mob.Update
            lifecycle++;
            if (lifecycle == 2000)
                lifecycle = 0;
            if (isdead)
            {
                lifecycle = 0;
            }

            Immortal(HERO);
            BodySlam(HERO);
        }

        public void Immortal(Hero HERO)
        {
            if (250 == lifecycle || 1000 == lifecycle || 1750 == lifecycle)
            {
                healthmarker = currenthealth;
                Global.soundcontrol.PLaySound("ShieldUpSound");
                
            }
            if ((250 <= lifecycle && lifecycle <= 500) ||
                (1000 <= lifecycle && lifecycle <= 1250) ||
                (1750 <= lifecycle && lifecycle <= 2000))
            {
                color = Color.Red;
                myModel = Global.content.Load<Texture2D>("CableImmortal");
                if (healthmarker > currenthealth)
                {
                    if (!GameGlobal.triggerinvinsible)
                        HERO.GetHit(healthmarker - currenthealth);
                    healthmarker = currenthealth;
                }
            }
            if (500 == lifecycle || 1250 == lifecycle || 2000 == lifecycle)
            {
                Global.soundcontrol.PLaySound("ShieldDownSound");
            }
            else
            {
                myModel = Global.content.Load<Texture2D>("Cable");
                color = Color.White;
            }
        }

        public void BodySlam(Hero HERO)
        {
            if (150 == lifecycle || 450 == lifecycle || 750 == lifecycle || 1050 == lifecycle || 1350 == lifecycle || 1650 == lifecycle)
            {
                fakeheropos = HERO.pos;
            }
            if ((150 <= lifecycle && lifecycle <= 300) ||
                (450 <= lifecycle && lifecycle <= 600) ||
                (750 <= lifecycle && lifecycle <= 900) ||
                (1050 <= lifecycle && lifecycle <= 1200) ||
                (1350 <= lifecycle && lifecycle <= 1500) ||
                (1650 <= lifecycle && lifecycle <= 1800) )
            {
                pos += Global.RadialMovement(fakeheropos, pos, 20);
                bossdmg = 80;
            }
            else
            {
                bossdmg = 5;
            }
        }
        
        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }


    }
}
