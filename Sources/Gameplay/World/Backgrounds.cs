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
    public class Backgrounds : Basic2d
    {
        public Backgrounds(string PATH, Vector2 POS, Vector2 DIMS) : base(PATH, POS, DIMS)
        {
            GameGlobal.herotouchscreen = HeroTouchScreen;
            //GameGlobal.cursortouchscreen = CursorTouchScreen;
        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }

        public override void Draw()          //Không override lại Draw(offset) vì background kh xoay
        {
            base.Draw();
        }

        public bool HeroTouchScreen(Object INFO)
        {
            Hero hero = (Hero)INFO;
            if (hero.pos.X >= dims.X - 2 * hero.dims.X)                               //Viền phải
                return false;
            if (hero.pos.X <= 2 * hero.dims.X)                                        //Viền trái
                return false;
            if (hero.pos.Y >= dims.Y - 2 * hero.dims.Y)                              //Viền dưới
                return false;
            if (hero.pos.Y <= 2 * hero.dims.Y)                                      //Viền trên
                return false; 
            return true;
        }
        
        //public bool CursorTouchScreen(object INFO)
        //{
        //    MouseState mousestate = Mouse.GetState();
        //    Cursor cursor = (Cursor)INFO;

        //    if (mousestate.X + cursor.pos.X >= dims.X - cursor.dims.X)              //Viền phải
        //    {
        //        return false;
        //    }
        //    if (mousestate.X + cursor.pos.X <= cursor.dims.X)                       //Viền trái
        //    {
        //        return false;
        //    }
        //    if (mousestate.Y + cursor.pos.Y >= dims.Y - cursor.dims.X)              //Viền dưới
        //    {
        //        return false;
        //    }
        //    if (mousestate.Y + cursor.pos.Y <= cursor.dims.X)                      //Viền trên
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
