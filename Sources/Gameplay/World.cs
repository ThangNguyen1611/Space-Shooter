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
        public Bar healthbar;
        public Bar manabar;
        public Basic2d scoreimg;
        public long highscrore;

        SpriteFont mousestr;

        public World()
        {
            offset = new Vector2(0, 0);

            bg = new Backgrounds("GlxBG", new Vector2(0, 0), new Vector2(4000, 4000));

            Ship = new Hero("F5S3", new Vector2(bg.dims.X / 2, bg.dims.Y / 2), new Vector2(28, 44));

            GameGlobal.passprojectile = Addprojectile;

            healthbar = new Bar("HB", new Vector2(600, 35));

            manabar = new Bar("MB", new Vector2(500, 15));

            scoreimg = new Basic2d("text_score", new Vector2(0, 0), new Vector2(150, 75));

            highscrore = 0;

            mousestr = Global.content.Load<SpriteFont>("mousestr");
        }

        public virtual void Update()
        {
            bg.Update();
            Ship.Update();
            if (Ship.isdead)
            {
                Ship.GetHit(1f);
                Ship.UseMana(1f);
                
            }
            else
            {
                if (Ship.currenthealth <= Ship.maxhealth)
                    Ship.GetHit(-0.05f);    //0.05f

                if (Ship.currentmana <= Ship.maxmana)
                    Ship.UseMana(-0.5f); //0.5f 
            }
            healthbar.Update(Ship.currenthealth, Ship.maxhealth);
            manabar.Update(Ship.currentmana, Ship.maxmana);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Update(offset, null);

                if (projectiles[i].done)
                {
                    projectiles.RemoveAt(i);
                    i--;
                }
            }
        }

        public virtual void Addprojectile(object INFO)
        {
            if (Ship.bulletdelay >= 0)
                Ship.bulletdelay--;

            if (Ship.bulletdelay <= 0)
            {
                if (projectiles.Count() < 35)
                {
                    Global.soundcontrol.PLaySound("Shooting");
                    projectiles.Add((Projectile2d)INFO);
                }
            }

            if (Ship.bulletdelay == 0)
                Ship.bulletdelay = 12; //min = 11
            //Set gameplay thành style đè. Vì style bắn từng viên phải chỉnh delay thấp -> lợi dụng bug đè sẽ như cheat
            //-> gameplay = đè. Thằng nào ngu bắn từ từ thì chết :v

        }

        public virtual void Draw(Vector2 OFFSET)
        {
            bg.Draw();
            Ship.Draw(offset);
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            if (Ship.currenthealth > 0)
            {
                healthbar.Draw(new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth / 2 + 50, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight / 2 + 20));
                manabar.Draw(new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth / 2 + 50, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight / 2 + 55));
            }

            scoreimg.Draw(new Vector2(Ship.pos.X + 450, Ship.pos.Y - 300));
            Global.spriteBatch.DrawString(mousestr, Ship.pos.X.ToString(), new Vector2(Ship.pos.X + 550, Ship.pos.Y - 325), Color.Red, 0, new Vector2(0, 0), 4, new SpriteEffects(), 0);
            //test
            MouseState mousestate = Mouse.GetState();

            Global.spriteBatch.DrawString(mousestr, mousestate.X.ToString(), new Vector2(Ship.pos.X + 100, Ship.pos.Y + 100), Color.Yellow);
            Global.spriteBatch.DrawString(mousestr, mousestate.Y.ToString(), new Vector2(Ship.pos.X + 100, Ship.pos.Y + 120), Color.Yellow);
            Global.spriteBatch.DrawString(mousestr, Ship.rot.ToString(), new Vector2(Ship.pos.X + 100, Ship.pos.Y + 140), Color.Yellow);
            Global.spriteBatch.DrawString(mousestr, Ship.pos.X.ToString(), new Vector2(Ship.pos.X + 100, Ship.pos.Y + 160), Color.Yellow);
            Global.spriteBatch.DrawString(mousestr, Ship.pos.Y.ToString(), new Vector2(Ship.pos.X + 100, Ship.pos.Y + 180), Color.Yellow);

            //Global.spriteBatch.DrawString(mousestr, healthbar.healthbar.dims.X.ToString(), new Vector2(Ship.pos.X + 50, Ship.pos.Y + 160), Color.Yellow);
            //Global.spriteBatch.DrawString(mousestr, healthbar.healthbarbg.dims.X.ToString(), new Vector2(Ship.pos.X + 50, Ship.pos.Y + 180), Color.Yellow);
            //Global.spriteBatch.DrawString(mousestr, healthbar.healthbar.pos.X.ToString(), new Vector2(Ship.pos.X + 50, Ship.pos.Y + 200), Color.Yellow);
            //Global.spriteBatch.DrawString(mousestr, healthbar.healthbarbg.pos.Y.ToString(), new Vector2(Ship.pos.X + 50, Ship.pos.Y + 220), Color.Yellow);
        }
    }
}
