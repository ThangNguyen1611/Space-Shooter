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
    public class MysteryItem : Basic2d
    {
        public bool istaken;

        public Random rand = new Random();

        public int timerbuff;

        public MysteryItem(Vector2 POS) : base("MysteryOrb", POS, new Vector2(50,50))
        {
            timerbuff = 1;
        }

        public void Update(Hero HERO)
        {
            HeroHit(HERO);
            if (Global.GetDistance(pos, HERO.pos) == 0)
            {
                Global.soundcontrol.PLaySound("TriggerBuff");
                RandomBuff();
                istaken = true;
            }
        }

        public void HeroHit(Hero HERO)
        {
            if (Global.GetDistance(pos, HERO.pos) < HERO.gravityradius)
            {
                if (0 < GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 2000)
                    pos += Global.RadialMovement(HERO.pos, pos, 10f);
                if (2000 < GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 7500)
                    pos += Global.RadialMovement(HERO.pos, pos, 15f);
                if (7500 < GameGlobal.gametimepassed)
                    pos += Global.RadialMovement(HERO.pos, pos, 20f);
            }
        }

        public void RandomBuff()
        {
            int number = rand.Next(100);
            if (0 <= number && number <= 20)
            {
                GameGlobal.whattrigger = "___+100 pts___";
                GameGlobal.highscrore += 100;
            }
            if (20 < number && number <= 45)
            {
                GameGlobal.whattrigger = "___Healing___";
                GameGlobal.triggerhealing = true;
            }
            if (45 < number && number <= 50)
            {
                GameGlobal.whattrigger = "___ExtraHealth___";
                GameGlobal.triggerextrahealth = true;
            }
            if (50 < number && number <= 55)
            {
                GameGlobal.whattrigger = "___ExtraDamage___";
                GameGlobal.extradamage += 25;
            }
            if (55 < number && number <= 60)
            {
                GameGlobal.whattrigger = "___ExtraCrit___";
                GameGlobal.triggerextracrit = true;
            }
            if (60 < number && number <= 62)
            {
                GameGlobal.whattrigger = "___ReLife___";
                GameGlobal.triggerextralife = true;
            }
            if (62 < number && number <= 64)
            {
                GameGlobal.whattrigger = "      Press F to \n  ___nuCLEAR___";
                GameGlobal.obtainnuclearbomb = true;
            }
            if (64 < number && number <= 76)
            {
                GameGlobal.whattrigger = "___FROZEN___";
                GameGlobal.triggerICEWORLD = true;
            }
            if (76 < number && number <= 88)
            {
                GameGlobal.whattrigger = "___INVINCIBLE___";
                GameGlobal.triggerinvinsible = true;
            }
            if (88 < number && number <= 100)
            {
                GameGlobal.whattrigger = "___MAKE IT BURN___";
                GameGlobal.triggerboostatkspeed = true;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

    }
}
