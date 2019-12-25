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
    public class World
    {
        public Vector2 offset;
        public Backgrounds bg;
        public Hero Ship;
        public List<Projectile2d> projectiles = new List<Projectile2d>();
        public List<GunnerProjectiles> gunnerProjectiles = new List<GunnerProjectiles>();
        public List<FatboizProjectiles> fatboizProjectiles = new List<FatboizProjectiles>();
        public List<BlitzHands> blitzHands = new List<BlitzHands>();

        public Bar healthbar;
        public Bar manabar;
        public Bar bosshealthbar;
        public Basic2d scoreimg;

        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public List<Mob> mobs = new List<Mob>();

        public List<Boss> bosses = new List<Boss>();
        public bool istimeofboss;
        public int delayspawnboss;

        public List<MysteryItem> orbs = new List<MysteryItem>();
        public bool orbspawn;
        public int orbspawntimer;

        public List<EXPLOSIVE> listBOOM = new List<EXPLOSIVE>();
        public List<int> explosivetimer = new List<int>();

        public int iceworldtimer;
        public int lbltrigger;
        
        public long timescore;


        PassObject ResetWorld;
        bool checkpause = false;
        KeyboardState pastKey;
        cButton btnPause;
        Basic2d imagePause;
        Basic2d imageGameOver;


        public World(PassObject RESETWORLD)
        {
            ResetWorld = RESETWORLD;



            offset = new Vector2(0, 0);

            Global.gamefont = Global.content.Load<SpriteFont>("FONT");

            bg = new Backgrounds("Background", new Vector2(0, 0), new Vector2(4000, 4000));

            Ship = new Hero("Ship", new Vector2(bg.dims.X / 2, bg.dims.Y / 2), new Vector2(28, 44));

            GameGlobal.PassProjectiles = Addprojectile;

            GameGlobal.PassMob = AddMob;


            istimeofboss = false;
            delayspawnboss = 300;

            GameGlobal.triggerNuclear = TriggerNuclearBomb;

            GameGlobal.GunnerPassProjectiles = GunnerProjectiles;
            GameGlobal.FatboizPassProjectiles = FatboizProjectiles;
            GameGlobal.FatboizPassMob = FatboizSummon;
            GameGlobal.BlitzHook = BlitzHook;
            GameGlobal.BlitzHookEffect = BlitzHookEffect;

            healthbar = new Bar("HB", new Vector2(600, 35));

            manabar = new Bar("MB", new Vector2(500, 15));

            bosshealthbar = new Bar("BossHP", new Vector2(800, 55));

            scoreimg = new Basic2d("Score", new Vector2(0, 0), new Vector2(150, 75));

            spawnPoints.Add(new SpawnPoint("UFO3", new Vector2(100, 100), new Vector2(200, 200)));

            spawnPoints.Add(new SpawnPoint("UFO3", new Vector2(3800, 100), new Vector2(200, 200)));

            spawnPoints.Add(new SpawnPoint("UFO3", new Vector2(100, 3800), new Vector2(200, 200)));

            spawnPoints.Add(new SpawnPoint("UFO3", new Vector2(3800, 3800), new Vector2(200, 200)));

            orbspawn = false;
            orbspawntimer = 100;

            iceworldtimer = 300;
            lbltrigger = 50;
            
            timescore = 0;




            imagePause = new Basic2d("pause2", new Vector2(Ship.pos.X, Ship.pos.Y), new Vector2(500, 500));

            imageGameOver = new Basic2d("gameover4", new Vector2(Ship.pos.X, Ship.pos.Y), new Vector2(836, 183));
        }

        public virtual void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (!Ship.isdead && !checkpause)
            {
                bg.Update(offset);
                Ship.Update(offset);
                #region Xử lí máu
                if (Ship.isdead && Ship.currenthealth >= 0)
                {
                    Ship.GetHit(1f);
                    Ship.UseMana(1f);
                }
                else
                {
                    if (Ship.currenthealth <= Ship.maxhealth)
                        Ship.GetHit(-0.005f);    //0.005f
                    if (Ship.currentmana <= Ship.maxmana)
                        Ship.UseMana(-0.25f); //0.25f 
                }
                healthbar.Update(Ship.currenthealth, Ship.maxhealth);
                manabar.Update(Ship.currentmana, Ship.maxmana);
                if (bosses.Count > 0)
                    bosshealthbar.Update(bosses[0].currenthealth, bosses[0].maxhealth);
                #endregion
                #region Xử lí đạn
                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, mobs.ToList<Unit>(), bosses);
                    if (projectiles[i].HitSomething(mobs.ToList<Unit>(), bosses))
                    {
                        Global.soundcontrol.PLaySound("BulletHit");
                        listBOOM.Add(new EXPLOSIVE(projectiles[i].pos));
                        explosivetimer.Add(0);
                    }
                    if (projectiles[i].done)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < gunnerProjectiles.Count; i++)
                {
                    gunnerProjectiles[i].Update(offset, Ship);
                    if (gunnerProjectiles[i].done)
                    {
                        gunnerProjectiles.RemoveAt(i);
                        i--;
                    }
                }
                #endregion
                #region Xử lí nổ
                for (int i = 0; i < explosivetimer.Count; i++)
                {
                    if (explosivetimer[i] >= 0)
                        explosivetimer[i]++;
                    if (explosivetimer[i] > 100)
                    {
                        listBOOM.RemoveAt(i);           //Siêu trí tuệ VN ((:
                        explosivetimer.RemoveAt(i);
                    }
                }
                #endregion
                #region Xử lí boss
                if (GameGlobal.gametimepassed % 3000 == 0)
                {
                    TimeofBoss();
                    if (GameGlobal.gametimepassed == 3000)
                        SpawnFatboiz();
                    else if (GameGlobal.gametimepassed == 6000)
                        SpawnBlitz();
                    else if (GameGlobal.gametimepassed == 500)
                        SpawnCable();
                    else
                    {   //sau khi spawn đủ 3 con thì sẽ spawn ngẫu nhiên
                        Random rand = new Random();
                        int whatboss = rand.Next(100);
                        if (0 <= whatboss && whatboss < 33)
                            SpawnFatboiz();
                        if (33 <= whatboss && whatboss < 66)
                            SpawnBlitz();
                        if (66 <= whatboss && whatboss < 100)
                            SpawnCable();
                    }
                    if (bosses.Count > 0)
                    {
                        if (bosses[0].isappear)
                        {
                            bosses[0].Update(offset, Ship);
                            if (bosses[0].bossname == "fatboiz")
                            {
                                for (int i = 0; i < fatboizProjectiles.Count; i++)
                                {
                                    fatboizProjectiles[i].Update(offset, Ship);
                                    if (fatboizProjectiles[i].done)
                                    {
                                        fatboizProjectiles.RemoveAt(i);
                                        i--;
                                    }
                                }
                                if (bosses[0].isdead)
                                {
                                    GameGlobal.highscrore += 1000;
                                    Global.soundcontrol.PLaySound("ShimmerSound");
                                    for (int i = 0; i < mobs.Count; i++)
                                    {
                                        if (mobs[i].typemob == "imp")
                                            GameGlobal.highscrore += 50;
                                        if (mobs[i].typemob == "elector")
                                            GameGlobal.highscrore += 50;
                                        if (mobs[i].typemob == "gunner")
                                            GameGlobal.highscrore += 150;
                                        mobs.RemoveAt(i);
                                        i--;
                                    }
                                    bosses.RemoveAt(0);
                                    delayspawnboss = 300;
                                    istimeofboss = false;
                                }
                            }
                            else if (bosses[0].bossname == "blitz")
                            {
                                for (int i = 0; i < blitzHands.Count; i++)
                                {
                                    blitzHands[i].Update(offset, Ship, bosses);
                                    BlitzHookEffect(blitzHands[i], Ship, bosses[0]);
                                    if (blitzHands[i].done)
                                    {
                                        blitzHands.RemoveAt(i);
                                        i--;
                                    }
                                }
                                if (bosses[0].isdead)
                                {
                                    GameGlobal.highscrore += 3000;
                                    Global.soundcontrol.PLaySound("ShimmerSound");
                                    bosses.RemoveAt(0);
                                    delayspawnboss = 300;
                                    istimeofboss = false;
                                    Ship.isstunned = false;
                                }
                            }
                            else if (bosses[0].bossname == "cable")
                            {
                                if (bosses[0].isdead)
                                {
                                    GameGlobal.highscrore += 7000;
                                    Global.soundcontrol.PLaySound("ShimmerSound");
                                    bosses.RemoveAt(0);
                                    delayspawnboss = 300;
                                    istimeofboss = false;
                                }
                            }
                        }
                    }
                }
                #endregion
                if (!istimeofboss)
                {
                    #region Xử lí Spawnpoint
                    for (int i = 0; i < spawnPoints.Count; i++)
                    {
                        spawnPoints[i].Update(offset);
                    }
                    #endregion
                    GameGlobal.gametimepassed++;
                }
                #region Xử lí mobs
                for (int i = 0; i < mobs.Count; i++)
                {
                    mobs[i].Update(offset, Ship);
                    if (mobs[i].isdead || mobs[i].iscollision)
                    {
                        if (mobs[i].typemob == "imp")
                            GameGlobal.highscrore += 50;
                        if (mobs[i].typemob == "elector")
                            GameGlobal.highscrore += 50;
                        if (mobs[i].typemob == "gunner")
                            GameGlobal.highscrore += 150;
                        mobs.RemoveAt(i);
                        i--;
                    }
                }
                #endregion
                #region Xử lí orbs
                if (!orbspawn)
                    orbspawntimer--;
                if (orbspawntimer == 0)
                {
                    orbspawn = true;
                    orbspawntimer = 750;
                }
                for (int i = 0; i < orbs.Count; i++)
                {
                    orbs[i].Update(Ship);
                    if (orbs[i].istaken)
                    {
                        orbs.RemoveAt(i);
                        i--;
                    }
                }
                #endregion
                #region Event orbs
                TriggerBoostAttackSpeed();
                TriggerICEWORLD();
                TriggerInvincible();
                if (GameGlobal.whattrigger == "")
                    lbltrigger = 120;
                #endregion
                HighScoreUp();


                imagePause.Update1(Ship.pos);
                imageGameOver.Update1(new Vector2(Ship.pos.X - 150, Ship.pos.Y + 30));
            }
            else
            {

                if (keystate.IsKeyDown(Keys.R))
                {
                    GameGlobal.highscrore = 0;
                    GameGlobal.gametimepassed = 1;

                    GameGlobal.whattrigger = "";

                    GameGlobal.triggerhealing = false;
                    GameGlobal.triggerextrahealth = false;
                    GameGlobal.extradamage = 0;
                    GameGlobal.triggerextracrit = false;
                    GameGlobal.triggerextralife = false;
                    GameGlobal.triggerICEWORLD = false;
                    GameGlobal.triggerinvinsible = false;
                    GameGlobal.invincibletimer = 300;
                    GameGlobal.triggerboostatkspeed = false;
                    GameGlobal.boostatkspeedtimer = 1000;
                    GameGlobal.obtainnuclearbomb = false;
                    ResetWorld(null);
                }


            }

            if (Keyboard.GetState().IsKeyDown(Keys.P) && pastKey.IsKeyUp(Keys.P))
            {
                checkpause = !checkpause;

            }
            pastKey = Keyboard.GetState();






           
        }

        public virtual void AddMob(object INFO)
        {
            if (spawnPoints[0].mobdellay >= 0)
                spawnPoints[0].mobdellay--;
            if (spawnPoints[0].mobdellay <= 0)
            {
                if (mobs.Count() < 1000)
                    mobs.Add((Mob)INFO);
            }
            if (spawnPoints[0].mobdellay == 0)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 2000)
                    spawnPoints[0].mobdellay = 500; //500
                if (2000 < GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 7500)
                    spawnPoints[0].mobdellay = 400; //450
                if (7500 < GameGlobal.gametimepassed)
                    spawnPoints[0].mobdellay = 325; //400
            }
        }

        public virtual void Addprojectile(object INFO)
        {
            if (Ship.bulletdelay >= 0)
                Ship.bulletdelay--;
            if (Ship.bulletdelay <= 0)
            {
                if (projectiles.Count() < 1000)
                {
                    Global.soundcontrol.PLaySound("Shooting");
                    projectiles.Add((Projectile2d)INFO);
                }
            }
            if (Ship.bulletdelay == 0)
            {
                if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 2000)
                    Ship.bulletdelay = 20;
                if (2000 < GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 7500)
                    Ship.bulletdelay = 17;
                if (7500 < GameGlobal.gametimepassed)
                    Ship.bulletdelay = 13; //min = 11
            }
        }

        public void TimeofBoss()
        {
            istimeofboss = true;
            if (delayspawnboss > 0)
                delayspawnboss--;
            if (delayspawnboss == 299)
            {
                for (int i = 0; i < mobs.Count; i++)
                {
                    mobs.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SpawnFatboiz()
        {
            if (delayspawnboss == 0)
            {
                Global.soundcontrol.PLaySound("FatboizScreamSound");
                Random rand = new Random();
                bosses.Add(new Fatboiz(new Vector2(rand.Next(1500, 2500), rand.Next(1500, 2500))));
                bosses[0].isappear = true;
                delayspawnboss--;   //cho delay xuống -1 để list boss không spawn thêm nữa -> chỉ duy nhất 1 con boss có thể xuất hiện trên bàn
            }
        }

        public void SpawnBlitz()
        {
            if (delayspawnboss == 0)
            {
                Global.soundcontrol.PLaySound("BlitzScreamSound");
                Random rand = new Random();
                bosses.Add(new Blitz(new Vector2(rand.Next(1500, 2500), rand.Next(1500, 2500))));
                bosses[0].isappear = true;
                delayspawnboss--;   //cho delay xuống -1 để list boss không spawn thêm nữa -> chỉ duy nhất 1 con boss có thể xuất hiện trên bàn
            }
        }

        public void SpawnCable()
        {
            if (delayspawnboss == 0)
            {
                Global.soundcontrol.PLaySound("CableScreamSound");
                Random rand = new Random();
                bosses.Add(new Cable(new Vector2(rand.Next(1500, 2500), rand.Next(1500, 2500))));
                bosses[0].isappear = true;
                delayspawnboss--;   //cho delay xuống -1 để list boss không spawn thêm nữa -> chỉ duy nhất 1 con boss có thể xuất hiện trên bàn
            }
        }

        public void FatboizProjectiles(object FATBOIZ, object PROJECTILE)
        {
            Fatboiz fatboiz = (Fatboiz)FATBOIZ;
            if (fatboiz.bulletdelay >= 0)
                fatboiz.bulletdelay--;
            if (fatboiz.bulletdelay <= 0)
            {
                if (fatboizProjectiles.Count() < 1000)
                {
                    fatboizProjectiles.Add((FatboizProjectiles)PROJECTILE);
                }
            }
            if (fatboiz.bulletdelay == 0)
            {
                fatboiz.bulletdelay = 10;
            }
        }

        public void FatboizSummon(object FATBOIZ, object INFO)
        {
            Fatboiz fatboiz = (Fatboiz)FATBOIZ;
            if (bosses.Count > 0)
            {
                if (fatboiz.sodiersspawndelay >= 0)
                    fatboiz.sodiersspawndelay--;
                if (fatboiz.sodiersspawndelay <= 0)
                {
                    if (mobs.Count() < 1000)
                        mobs.Add((Mob)INFO);
                }
                if (fatboiz.sodiersspawndelay == 0)
                {
                    fatboiz.sodiersspawndelay = 25;
                }
            }
        }

        public void BlitzHook(object BLITZ, object PROJECTILE)
        {
            Blitz blitz = (Blitz)BLITZ;
            if (blitz.handdelay >= 0)
                blitz.handdelay--;
            if (blitz.handdelay <= 0)
            {
                if (blitzHands.Count() < 1000)
                {
                    Global.soundcontrol.PLaySound("BlitzHook");
                    blitzHands.Add((BlitzHands)PROJECTILE);
                }
            }
            if (blitz.handdelay == 0)
            {
                blitz.handdelay = 40;
            }
        }

        public void BlitzHookEffect(object BLITZHAND, object HERO, object BOSS)
        {
            BlitzHands blitzHands = (BlitzHands)BLITZHAND;
            Hero hero = (Hero)HERO;
            Blitz boss = (Blitz)BOSS;

            if (blitzHands.ishit && !blitzHands.done)
            {
                if (Global.GetDistance(hero.pos, boss.pos) >= boss.hitdist)
                {
                    hero.pos += Global.RadialMovement(hero.pos, boss.pos, -15);
                    hero.cursor.pos += Global.RadialMovement(hero.pos, boss.pos, -15);
                    hero.isstunned = true;
                }
                if (Global.GetDistance(boss.pos, hero.pos) < boss.hitdist)
                {
                    hero.isstunned = false;
                    blitzHands.ishit = false;
                }
            }
        }

        public void GunnerProjectiles(object GUNNER, object PROJECTILE)
        {
            Gunner mobgunner = (Gunner)GUNNER;
            if (mobgunner.bulletdelay >= 0)
                mobgunner.bulletdelay--;
            if (mobgunner.bulletdelay <= 0)
            {
                if (gunnerProjectiles.Count() < 1000)
                {
                    Global.soundcontrol.PLaySound("Shooting");
                    gunnerProjectiles.Add((GunnerProjectiles)PROJECTILE);
                }
            }
            if (mobgunner.bulletdelay == 0)
            {
                mobgunner.bulletdelay = 40; 
            }
        }

        public void TriggerICEWORLD()
        {
            if (GameGlobal.triggerICEWORLD)
            {
                if (iceworldtimer == 300)
                    Global.soundcontrol.PLaySound("FREEZE");
                bg.myModel = Global.content.Load<Texture2D>("FreezedWorld");
                iceworldtimer--;
                for (int i = 0; i < mobs.Count; i++)
                {
                    mobs[i].speed = 0.1f;
                }
                if (iceworldtimer <= 0)
                {
                    for (int i = 0; i < mobs.Count; i++)
                    {
                        if (0 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 2000)
                        {
                            if (mobs[i].typemob == "imp")
                                mobs[i].speed = 6.5f;
                            if (mobs[i].typemob == "elector")
                                mobs[i].speed = 8.5f;
                            if (mobs[i].typemob == "gunner")
                                mobs[i].speed = 2f;
                        }
                        if (2000 < GameGlobal.gametimepassed && GameGlobal.gametimepassed <= 7500)
                        {
                            if (mobs[i].typemob == "imp")
                                mobs[i].speed = 7.5f;
                            if (mobs[i].typemob == "elector")
                                mobs[i].speed = 9.25f;
                            if (mobs[i].typemob == "gunner")
                                mobs[i].speed = 2.25f;
                        }
                        if (7500 < GameGlobal.gametimepassed)
                        {
                            if (mobs[i].typemob == "imp")
                                mobs[i].speed = 9.5f;
                            if (mobs[i].typemob == "elector")
                                mobs[i].speed = 10f;
                            if (mobs[i].typemob == "gunner")
                                mobs[i].speed = 2.5f;
                        }
                    }
                    GameGlobal.triggerICEWORLD = false;
                    iceworldtimer = 300;
                    bg.myModel = Global.content.Load<Texture2D>("Background");
                }
            }
        }

        public void TriggerBoostAttackSpeed()
        {
            if (GameGlobal.triggerboostatkspeed)
            {
                GameGlobal.boostatkspeedtimer--;
                Ship.color = Color.PaleVioletRed;
                if(GameGlobal.boostatkspeedtimer == 0)
                {
                    GameGlobal.boostatkspeedtimer = 1500;
                    GameGlobal.triggerboostatkspeed = false;
                    Ship.color = Color.White;
                }
            }
        }

        public void TriggerInvincible() 
        {
            if (GameGlobal.triggerinvinsible)
            {
                if (GameGlobal.invincibletimer > 0)
                {
                    GameGlobal.invincibletimer--;
                }
                if (GameGlobal.invincibletimer <= 0)
                {
                    GameGlobal.invincibletimer = 300;
                    GameGlobal.triggerinvinsible = false;
                }
            }
        }

        public void TriggerNuclearBomb()
        {
            if (GameGlobal.obtainnuclearbomb)
            {
                Global.soundcontrol.PLaySound("NuclearBombSound");
                for(int i = 0; i < mobs.Count; i++)
                {
                    if (mobs[i].typemob == "imp")
                        GameGlobal.highscrore += 50;
                    if (mobs[i].typemob == "elector")
                        GameGlobal.highscrore += 50;
                    if (mobs[i].typemob == "gunner")
                        GameGlobal.highscrore += 150;
                    listBOOM.Add(new EXPLOSIVE(mobs[i].pos));
                    explosivetimer.Add(0);
                    mobs.RemoveAt(i);
                    i--;
                }
                GameGlobal.obtainnuclearbomb = false;
            }
        }

        public void HighScoreUp()
        {
            if (!Ship.isdead)
            {
                timescore += 1;
                if (timescore == 100)
                {
                    GameGlobal.highscrore++;
                    timescore = 0;
                }
            }
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            bg.Draw();
            Ship.Draw(offset);

            if (delayspawnboss == 299)
                Global.soundcontrol.PLaySound("BossWarningSound");

            if (delayspawnboss <= 299 && delayspawnboss >= 10)
                Global.spriteBatch.DrawString(Global.gamefont, "            WARNING     \n Get Out Of The Center" , new Vector2(Ship.pos.X - 250, Ship.pos.Y - 250), Color.Red, 0, new Vector2(0, 0), 3.2f, new SpriteEffects(), 0);
            
            if (orbspawn)
            {
                Random rand = new Random();
                int orbposx = rand.Next(300, 3700); //300 3700
                int orbposy = rand.Next(300, 3700); //300 3700
                orbs.Add(new MysteryItem(new Vector2(orbposx, orbposy)));
                orbspawn = false;
            }

            if (Ship.currenthealth > 0)
            {
                healthbar.Draw(new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth / 2 + 50, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight / 2 + 20));
                manabar.Draw(new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth / 2 + 50, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight / 2 + 55));
            }
            
            #region Draw Loops
            for (int i = 0; i < orbs.Count; i++)
            {
                orbs[i].Draw();
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);

            }
            for (int i = 0; i < gunnerProjectiles.Count; i++)
            {
                gunnerProjectiles[i].Draw(offset);
            }
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(offset);
            }
            for (int i = 0; i < mobs.Count; i++)
            {
                mobs[i].Draw(offset);
            }
            for (int i = 0; i < listBOOM.Count; i++)
            {
                if (explosivetimer[i] <= 15)
                {
                    //Global.spriteBatch.DrawString(Global.gamefont, Ship.UnrealAttack.ToString(), listBOOM[i].pos + new Vector2(0,50), Color.Red);
                    listBOOM[i].Draw();
                }
            }
            #endregion
            #region Draw boss
            if (bosses.Count > 0)
            {
                if (bosses[0].isappear && !bosses[0].isdead)
                    bosses[0].Draw(offset);
                if (bosses[0].bossname == "fatboiz")
                    for (int i = 0; i < fatboizProjectiles.Count; i++)
                    {
                        fatboizProjectiles[i].Draw(offset);
                    }
                else if (bosses[0].bossname == "blitz")
                    for (int i = 0; i < blitzHands.Count; i++)
                    {
                        blitzHands[i].Draw(offset);
                    }
                else if (bosses[0].bossname == "cable")
                {
                }
                if (bosses[0].currenthealth > 0)
                    bosshealthbar.Draw(new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth / 2 + 300, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight / 2 + 680));               
            }
            #endregion

            scoreimg.Draw(new Vector2(Ship.pos.X + 450, Ship.pos.Y - 300));
            Global.spriteBatch.DrawString(Global.gamefont, GameGlobal.highscrore.ToString(), new Vector2(Ship.pos.X + 535, Ship.pos.Y - 325), Color.CadetBlue, 0, new Vector2(0, 0), 4, new SpriteEffects(), 0);
            Global.spriteBatch.DrawString(Global.gamefont, ((int)Ship.currenthealth).ToString() + "/" + Ship.maxhealth.ToString(), new Vector2(Ship.pos.X , Ship.pos.Y - 337), Color.CadetBlue, 0, new Vector2(0, 0), 2, new SpriteEffects(), 0);
            //Global.spriteBatch.DrawString(Global.gamefont, "GameTime: " + GameGlobal.gametimepassed.ToString(), new Vector2(Ship.pos.X + 550, Ship.pos.Y - 250), Color.CadetBlue);

            Global.spriteBatch.DrawString(Global.gamefont, "Speed: " + Ship.speed.ToString(), new Vector2(Ship.pos.X - 620, Ship.pos.Y - 235), Color.CadetBlue);
            Global.spriteBatch.DrawString(Global.gamefont, "Attack: " + Ship.UnrealAttack.ToString(), new Vector2(Ship.pos.X - 620, Ship.pos.Y - 215), Color.CadetBlue);
            Global.spriteBatch.DrawString(Global.gamefont, "Crit Chance: " + Ship.critchance.ToString(), new Vector2(Ship.pos.X - 620, Ship.pos.Y - 195), Color.CadetBlue);
            
            if (GameGlobal.whattrigger != "")
            {
                Global.spriteBatch.DrawString(Global.gamefont, GameGlobal.whattrigger, new Vector2(Ship.pos.X - 50, Ship.pos.Y - 100), Color.White);
                lbltrigger--;
            }
            if (lbltrigger == 0)
                GameGlobal.whattrigger = "";



            if (Ship.isdead)
            {
                imageGameOver.Draw();
                Global.spriteBatch.DrawString(Global.gamefont, "                         \n PRESS R TO PLAY AGAIN  \n            ESC TO EXIT", new Vector2(Ship.pos.X - 250, Ship.pos.Y), Color.Red, 0, new Vector2(0, 0), 3.2f, new SpriteEffects(), 0);
            }

            if (checkpause && !Ship.isdead)
            {
                imagePause.Draw();
            }
        }
    }
}