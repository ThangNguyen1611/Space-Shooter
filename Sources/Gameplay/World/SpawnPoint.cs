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
    public class SpawnPoint : Basic2d
    {

        public bool dead;

        public float hitdist;   

        public float mobdellay;

        public McTimer spawnTimer = new McTimer(20000, false);



        public SpawnPoint(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            dead = false;

            mobdellay = 1;

            hitdist = 35.0f;
        }


        public void GetHit()
        {
            dead = true;
        }

        public override void Update(Vector2 OFFSET)
        {
            SpawnMob();
            spawnTimer.UpdateTimer();
            if (spawnTimer.Test())
            {
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET);
        }

        public virtual void SpawnMob() 
        {
            Random rand = new Random();
            int spawnplace = rand.Next(0, 100);
            int whatmob = rand.Next(0, 100);
            if (0 <= spawnplace && spawnplace < 25)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed < 1000)
                    GameGlobal.PassMob(new Imp(new Vector2(100, 100)));
                if(1000 <= GameGlobal.gametimepassed)
                {
                    if(0 <= whatmob && whatmob < 40)
                        GameGlobal.PassMob(new Elector(new Vector2(100, 100)));
                    if (40 <= whatmob && whatmob < 55)
                        GameGlobal.PassMob(new Gunner (new Vector2(100, 100)));
                    if (55 <= whatmob && whatmob < 100)
                        GameGlobal.PassMob(new Imp(new Vector2(100, 100)));
                }
            }
            if (25 <= spawnplace && spawnplace < 50)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed < 1000)
                    GameGlobal.PassMob(new Imp(new Vector2(3800, 100)));
                if (1000 < GameGlobal.gametimepassed)
                {
                    if (0 <= whatmob && whatmob < 40)
                        GameGlobal.PassMob(new Elector(new Vector2(3800, 100)));
                    if (40 <= whatmob && whatmob < 55)
                        GameGlobal.PassMob(new Gunner(new Vector2(3800, 100)));
                    if (55 <= whatmob && whatmob < 100)
                        GameGlobal.PassMob(new Imp(new Vector2(3800, 100)));
                }
            }
            if (50 <= spawnplace && spawnplace < 75)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed < 1000)
                    GameGlobal.PassMob(new Imp(new Vector2(100, 3800)));
                if (1000 < GameGlobal.gametimepassed)
                {
                    if (0 <= whatmob && whatmob < 40)
                        GameGlobal.PassMob(new Elector(new Vector2(100, 3800)));
                    if (40 <= whatmob && whatmob < 55)
                        GameGlobal.PassMob(new Gunner(new Vector2(100, 3800)));
                    if (55 <= whatmob && whatmob < 100)
                        GameGlobal.PassMob(new Imp(new Vector2(100, 3800)));
                }
            }
            if (75 <= spawnplace && spawnplace < 100)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed < 1000)
                    GameGlobal.PassMob(new Imp(new Vector2(3800, 3800)));
                if (1000 < GameGlobal.gametimepassed)
                {
                    if (0 <= whatmob && whatmob < 40)
                        GameGlobal.PassMob(new Elector(new Vector2(3800, 3800)));
                    if (40 <= whatmob && whatmob < 55)
                        GameGlobal.PassMob(new Gunner(new Vector2(3800, 3800)));
                    if (55 <= whatmob && whatmob < 100)
                        GameGlobal.PassMob(new Imp(new Vector2(3800, 3800)));
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
