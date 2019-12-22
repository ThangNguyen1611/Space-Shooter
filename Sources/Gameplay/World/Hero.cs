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
#endregion/

namespace PROJECT_SpaceShooter
{
    public class Hero : Unit
    {
        public Cursor cursor;
        public float bulletdelay;
        public float gravityradius;
        public SpawnCircle spawncircle;
        public List<HeroLife> herolife = new List<HeroLife>();
        public float currentmana, maxmana;
        public bool triggeredSwift;
        public float timeSwift;
        public Basic2d swiftaura;
        public float critchance;
        public Basic2d nuclearbomb;
        public float UnrealAttack;  //Fake dmg của Fireball
        public bool triggergotohell;
        public float timedeadeffect;
        public float spawndelay;
        
        public Hero(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            isdead = false;
            speed = 5.0f; //5f
            hitdist = 35;
            gravityradius = 200;
            bulletdelay = 1;
            currenthealth = 100;
            maxhealth = currenthealth;
            currentmana = 100;
            maxmana = currentmana;
            life = 3;
            triggeredSwift = false;
            triggergotohell = false;
            timeSwift = 100;
            timedeadeffect = 30;
            spawndelay = 100;
            critchance = 10;
            
            cursor = new Cursor("crosshair_red_large", new Vector2(2000 - Global.screenwidth / 2, 2000 - Global.screenheight / 2), new Vector2(30, 30));
            spawncircle = new SpawnCircle("SpawnCircle", new Vector2(0, 0), new Vector2(120, 100));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            swiftaura = new Basic2d("SwiftAura", new Vector2(0, 0), new Vector2(100, 100));
            nuclearbomb = new Basic2d("NuclearBomb", new Vector2(0, 0), new Vector2(75, 75));
            
        }
        
        public override void Update(Vector2 OFFSET)
        {
            KeyboardState keystate = Keyboard.GetState();
            MouseState mousestate = Mouse.GetState();
            #region Hero Movement & Skill
            if ((keystate.IsKeyDown(Keys.A) || keystate.IsKeyDown(Keys.Left)) && !isdead && !triggergotohell && isstunned == false)
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X - speed, pos.Y);
                    cursor.pos = new Vector2(cursor.pos.X - speed, cursor.pos.Y);
                }
                else
                {
                    if (life > 0)
                        triggergotohell = true;
                }
            }
            if ((keystate.IsKeyDown(Keys.D) || keystate.IsKeyDown(Keys.Right)) && !isdead && !triggergotohell && isstunned == false)
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X + speed, pos.Y);
                    cursor.pos = new Vector2(cursor.pos.X + speed, cursor.pos.Y);
                }
                else
                {
                    if (life > 0)
                        triggergotohell = true;
                }
            }
            if ((keystate.IsKeyDown(Keys.W) || keystate.IsKeyDown(Keys.Up)) && !isdead && !triggergotohell && isstunned == false)
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X, pos.Y - speed);
                    cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y - speed);
                }
                else
                {
                    if (life > 0)
                        triggergotohell = true;
                }
            }
            if ((keystate.IsKeyDown(Keys.S) || keystate.IsKeyDown(Keys.Down)) && !isdead && !triggergotohell && isstunned == false)
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X, pos.Y + speed);
                    cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y + speed);
                }
                else
                {
                    if (life > 0)
                        triggergotohell = true;
                }

            }
            if (keystate.IsKeyDown(Keys.Space) && !isdead && !triggergotohell && isstunned == false)
            {
                Swift();
            }
            if (triggeredSwift)
            {
                SwiftEffect();
                timeSwift -= 2.5f;
                if (timeSwift <= 0)
                {
                    triggeredSwift = false;
                    //  /=2 
                    if (GameGlobal.gametimepassed < 2000)
                        speed = 5;
                    if (2000 <= GameGlobal.gametimepassed && GameGlobal.gametimepassed < 7500)
                        speed = 6;
                    if (7500 <= GameGlobal.gametimepassed)
                        speed = 8;
                }
            }
            if (keystate.IsKeyDown(Keys.F))
            {
                GameGlobal.triggerNuclear();
            }
            #endregion
            if (keystate.IsKeyDown(Keys.L))
                currenthealth = 100;

            rot = Global.RotateTowards(pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y));

            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                if (isdead == false)
                {
                    if (!IsCrit(critchance))
                    {
                        if (GameGlobal.triggerboostatkspeed)
                        {
                            MultiBullet();
                        }
                        else
                            GameGlobal.PassProjectiles(new Fireball(pos, new Vector2(35, 35), false, this.pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y)));
                    }
                    else
                    {
                        GameGlobal.PassProjectiles(new Fireball(pos, new Vector2(105, 105), true, this.pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y)));
                    }
                }
            }

            #region Buff
            TriggerHealing();
            TriggerExtraHealth();
            TriggerExtraCrit();
            TriggerExtraLife();
            UnrealAttack = 35 + GameGlobal.extradamage;
            #endregion

            if (IsDeadYet(currenthealth))
            {
                if (life > 0)
                    triggergotohell = true;
            }
            if (DeadEffect())
            {
                Dead();
                Respawn();
            }

            if (2000 == GameGlobal.gametimepassed)
                speed = 6;
            if (7500 == GameGlobal.gametimepassed)
                speed = 8;

            base.Update(OFFSET);
        }
        
        public bool DeadEffect()
        {
            if (triggergotohell)
            {
                if (timedeadeffect > 0)
                {
                    myModel = Global.content.Load<Texture2D>("DeadEffect");
                    dims = new Vector2(50, 50);
                    timedeadeffect--;
                }
                if (timedeadeffect == 0)
                {
                    timedeadeffect = 30;
                    myModel = Global.content.Load<Texture2D>("Ship");
                    dims = new Vector2(28, 44);
                    triggergotohell = false;
                    return true;
                }
            }
            return false;
        }

        public bool IsCrit(float CRITCHANCE)
        {
            Random rand = new Random();
            if (rand.Next(100) <= CRITCHANCE)
                return true;
            else
                return false;
        }
        
        public void UseMana(float USED)
        {
            currentmana -= USED;
        }

        public void Swift()
        {
            if (currentmana >= 90)
            {
                Global.soundcontrol.PLaySound("SkillSwiftSound");
                triggeredSwift = true;
                UseMana(90f);
                speed *= 2f;
                timeSwift = 100;
            }
        }       

        public void SwiftEffect()
        {
            if (triggeredSwift)
            {
                if (swiftaura.dims.X < 150)
                    swiftaura.dims = new Vector2(swiftaura.dims.X + 10, swiftaura.dims.Y + 10);
                else
                    swiftaura.dims = new Vector2(100, 100);
            }
        }

        public void MultiBullet()
        {
            MouseState mousestate = Mouse.GetState();

            GameGlobal.PassProjectiles(new Fireball(new Vector2(pos.X, pos.Y), new Vector2(35, 35), false, this.pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y)));
            GameGlobal.PassProjectiles(new Fireball(new Vector2(pos.X, pos.Y), new Vector2(35, 35), false, this.pos, new Vector2(mousestate.X + cursor.pos.X + 50, mousestate.Y + cursor.pos.Y + 50)));
            GameGlobal.PassProjectiles(new Fireball(new Vector2(pos.X, pos.Y), new Vector2(35, 35), false, this.pos, new Vector2(mousestate.X + cursor.pos.X - 50, mousestate.Y + cursor.pos.Y - 50)));
        }

        public void Respawn()
        {
            if (life == 0)
                Global.soundcontrol.PLaySound("GameOver");
            if (life > 0)
            {
                Global.soundcontrol.PLaySound("Respawn");
                GameGlobal.invincibletimer = 300;
                GameGlobal.triggerinvinsible = true;
                GameGlobal.triggerboostatkspeed = false;
                GameGlobal.boostatkspeedtimer = 1500;
                this.color = Color.White;
                isstunned = false;
                currenthealth = maxhealth;
                currentmana = maxmana;
                speed = 5;
                if (2000 < GameGlobal.gametimepassed)
                    speed = 6f;
                if (7500 < GameGlobal.gametimepassed)
                    speed = 8f;
                triggeredSwift = false;
                timeSwift = 100;
                pos = new Vector2(2000, 2000);
                cursor.pos = new Vector2(2000 - Global.screenwidth / 2, 2000 - Global.screenheight / 2);
            }
        }               //Khi còn life thì sẽ Respawn ở giữa màn hình
        
        public void TriggerHealing()
        {
            if (GameGlobal.triggerhealing)
            {
                if (currenthealth <= (maxhealth - 50))
                    currenthealth += 50;
                else
                    currenthealth = maxhealth;
                GameGlobal.triggerhealing = false;
            }
        }

        public void TriggerExtraHealth()
        {
            if (GameGlobal.triggerextrahealth)
            {
                maxhealth += 10;
                currenthealth += 10;
                GameGlobal.triggerextrahealth = false;
            }
        }

        public void TriggerExtraCrit()
        {
            if (GameGlobal.triggerextracrit)
            {
                if(critchance <= 40)        //max critchance = 50
                    critchance += 10;
                GameGlobal.triggerextracrit = false;
            }
        }

        public void TriggerExtraLife()
        {
            if (GameGlobal.triggerextralife)
            {
                if(life < 3)
                    life += 1;
                GameGlobal.triggerextralife = false;
            }
        }
        
        public override void Draw(Vector2 OFFSET)
        {
            if (!isdead)    //nếu chưa chết hẳn
            {
                if (triggeredSwift)
                    swiftaura.Draw(new Vector2(this.pos.X, this.pos.Y));
                base.Draw(OFFSET);
                cursor.Draw(new Vector2(Global.mouse.newMousePos.X, Global.mouse.newMousePos.Y), new Vector2(25, 25));
            }
            if (GameGlobal.triggerinvinsible && !isdead)  
                spawncircle.Draw(new Vector2(this.pos.X, this.pos.Y));
            for (int i = 0; i < life; i++)  //quản lí số mạng
            {
                herolife[i].Draw(new Vector2(this.pos.X - 600 + i * 50, this.pos.Y - 260));
            }
            if (GameGlobal.obtainnuclearbomb)
                nuclearbomb.Draw(new Vector2(this.pos.X - 400, this.pos.Y - 245));
        }
    }
}
