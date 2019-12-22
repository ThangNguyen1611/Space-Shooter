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
    public class GameGlobal
    {
        public static PassObject PassProjectiles, PassMob;
        public static Pass2Object GunnerPassProjectiles, FatboizPassProjectiles, FatboizPassMob, BlitzHook;
        public static Pass3Object BlitzHookEffect;
        public static isPassObject herotouchscreen;
        public static Trigger triggerNuclear;

        public static long highscrore = 0;
        public static long gametimepassed = 1;

        public static string whattrigger = "";

        public static bool triggerhealing = false;
        public static bool triggerextrahealth = false;
        public static float extradamage = 0;            //Vì projectile hay fireball new ra liên tục nên update thẳng vào constructor được
        public static bool triggerextracrit = false;
        public static bool triggerextralife = false;
        public static bool triggerICEWORLD = false;
        public static bool triggerinvinsible = false;
        public static int invincibletimer = 300;
        public static bool triggerboostatkspeed = false;
        public static int boostatkspeedtimer = 1000;
        public static bool obtainnuclearbomb = false;

    }
}
