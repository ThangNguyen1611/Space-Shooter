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
        public SpawnCircle spawncircle;
        public List<HeroLife> herolife = new List<HeroLife>();
        public bool isRespawning;
        public float currentmana, maxmana;
        public bool triggeredSwift;
        public float timeSwift;
        public Basic2d swiftaura;

        //*
        SpriteFont mousestr;

        public Hero(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            isdead = false;
            speed = 15.0f; //7.5f
            bulletdelay = 1; //delay ban đầu thôi
            currenthealth = 100;
            maxhealth = currenthealth;
            currentmana = 100;
            maxmana = currentmana;
            life = 3;
            isRespawning = false;
            triggeredSwift = false;
            timeSwift = 100;
            
            cursor = new Cursor("crosshair_red_large", new Vector2(2000 - Global.screenwidth / 2, 2000 - Global.screenheight / 2), new Vector2(30, 30));
            spawncircle = new SpawnCircle("SpawnCircle", new Vector2(0, 0), new Vector2(120, 100));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            herolife.Add(new HeroLife("Heart", new Vector2(0, 0), new Vector2(50, 50)));
            swiftaura = new Basic2d("SwiftAura", new Vector2(0, 0), new Vector2(100, 100));
            //*
            mousestr = Global.content.Load<SpriteFont>("mousestr");
        }
        
        public override void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            MouseState mousestate = Mouse.GetState();

            if (keystate.IsKeyDown(Keys.A))
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X - speed, pos.Y);
                    cursor.pos = new Vector2(cursor.pos.X - speed, cursor.pos.Y);
                }
                else
                {
                    Dead();
                    Respawn();
                }
                if (OutOfSaver())
                    isRespawning = false;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X + speed, pos.Y);
                    cursor.pos = new Vector2(cursor.pos.X + speed, cursor.pos.Y);
                }
                else
                {
                    Dead();
                    Respawn();
                }
                if (OutOfSaver())
                    isRespawning = false;
            }
            if (keystate.IsKeyDown(Keys.W))
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X, pos.Y - speed);
                    cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y - speed);
                }
                else
                {
                    Dead();
                    Respawn();
                }
                if (OutOfSaver())
                    isRespawning = false;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                if (GameGlobal.herotouchscreen(this))
                {
                    pos = new Vector2(pos.X, pos.Y + speed);
                    cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y + speed);
                }
                else
                {
                    Dead();
                    Respawn();
                }
                if (OutOfSaver())
                    isRespawning = false;

            }
            if (keystate.IsKeyDown(Keys.Space))
            {
                Swift();
                if (OutOfSaver())
                    isRespawning = false;
            }
            if (triggeredSwift)
            {
                SwiftEffect();
                timeSwift -= 3.5f;
                if (timeSwift <= 0)
                {
                    triggeredSwift = false;
                    speed /= 2.5f;
                }
            }

            rot = Global.RotateTowards(pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y));

            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                if(isdead == false)
                    GameGlobal.passprojectile(new Fireball(new Vector2(pos.X, pos.Y), this.pos, new Vector2(mousestate.X + cursor.pos.X, mousestate.Y + cursor.pos.Y)));               
            }

            base.Update();
        }

        public void Swift()
        {
            if (currentmana >= 90)
            {
                triggeredSwift = true;
                UseMana(90f);
                #region code bị khóa
                //if (flagdirection == 'a')
                //    if (GameGlobal.herotouchscreen(this))
                //    {
                //        pos = new Vector2(pos.X - 3f * speed, pos.Y);
                //        cursor.pos = new Vector2(cursor.pos.X - 3f * speed, cursor.pos.Y);
                //    }
                //    else
                //    {
                //        Dead();
                //        Respawn();
                //    }
                //if (flagdirection == 'd')
                //    if (GameGlobal.herotouchscreen(this))
                //    {
                //        pos = new Vector2(pos.X + 3f * speed, pos.Y);
                //        cursor.pos = new Vector2(cursor.pos.X + 3f * speed, cursor.pos.Y);
                //    }
                //    else
                //    {
                //        Dead();
                //        Respawn();
                //    }
                //if (flagdirection == 'w')
                //    if (GameGlobal.herotouchscreen(this))
                //    {
                //        pos = new Vector2(pos.X, pos.Y - 3f * speed);
                //        cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y - 3f * speed);
                //    }
                //    else
                //    {
                //        Dead();
                //        Respawn();
                //    }
                //if (flagdirection == 's')
                //    if (GameGlobal.herotouchscreen(this))
                //    {
                //        pos = new Vector2(pos.X, pos.Y + 3f * speed);
                //        cursor.pos = new Vector2(cursor.pos.X, cursor.pos.Y + 3f * speed);
                //    }
                //    else
                //    {
                //        Dead();
                //        Respawn();
                //    }
                #endregion
                speed *= 2.5f;
                timeSwift = 100;
                //Vì sao kh dùng code bị khóa ?
                //Code bị khóa áp dụng cho kiểu: currentmana >= 10 và usemana(10) -> Bấm space nhiều lần để gia tăng từng đoạn ngắn / mana ít hay nhiều đều dùng đc
                //Code không bị khóa tối ưu hơn: usemana(90) -> 90 mana mới đc dùng 1 lần
                //Chỉ tăng speed lên -> người chơi tự do điều khiển hướng chứ không bị gò bó bởi hướng trước khi nhấn Space như code bị khóa
                //Dùng triggeredSwift dễ kiểm soát khi nào đang dùng skill hơn -> Dễ chỉnh hiệu ứng bóng mờ
            }
        }       

        public void UseMana(float USED)
        {
            currentmana -= USED;
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

        public void Dead()
        {
            life -= 1;
            if (life <= 0)
                isdead = true;
        }                   //Mỗi khi chết 1 lần

        public void Respawn()
        {
            if (life > 0)
            {
                isRespawning = true;
                currenthealth = 100;
                currentmana = 100;
                pos = new Vector2(2000, 2000);
                cursor.pos = new Vector2(2000 - Global.screenwidth / 2, 2000 - Global.screenheight / 2);
            }
        }               //Khi còn life thì sẽ Respawn ở giữa màn hình

        public bool OutOfSaver()
        {
            if (isRespawning && (pos.X > 2500 || pos.X < 1500 || pos.Y > 2500 || pos.Y < 1500)) //Biến isrespawning quyết định hàm chỉ đc gọi ra khi vừa chết
                return true;
            return false;
        }           //1 vùng an toàn khi hồi sinh

        public override void Draw(Vector2 OFFSET)
        {
            if (!isdead)    //nếu chưa chết hẳn
            {
                if (triggeredSwift)
                    swiftaura.Draw(new Vector2(this.pos.X, this.pos.Y));
                base.Draw(OFFSET);
                cursor.Draw(new Vector2(Global.mouse.newMousePos.X, Global.mouse.newMousePos.Y), new Vector2(25, 25));
            }
            if (isRespawning && !isdead)    //nếu đang hồi sinh và chưa chết hẳn
                spawncircle.Draw(new Vector2(this.pos.X, this.pos.Y));
            for (int i = 0; i < life; i++)  //quản lí số mạng
            {
                herolife[i].Draw(new Vector2(this.pos.X - 600 + i * 50, this.pos.Y - 260));
            }

            //test
            Global.spriteBatch.DrawString(mousestr, cursor.pos.X.ToString(), new Vector2(this.pos.X + 50, this.pos.Y + 100), Color.Yellow);
            Global.spriteBatch.DrawString(mousestr, cursor.pos.Y.ToString(), new Vector2(this.pos.X + 50, this.pos.Y + 120), Color.Yellow);
        }
    }
}
