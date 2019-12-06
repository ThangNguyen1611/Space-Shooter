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
    public class Basic2d
    {
        public float rot;
        public Vector2 pos, dims;

        public Texture2D myModel;

        public Basic2d(string PATH, Vector2 POS, Vector2 DIMS)
        {
            pos = POS;      //positon
            dims = DIMS;    //dimenson
            
            myModel = Global.content.Load<Texture2D>(PATH);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if (myModel != null)
            {
                Global.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X), (int)(pos.Y), (int)dims.X, (int)dims.Y), Color.White);
            }
        }                                       //Không rot, kh offset, kh origin

        public virtual void Draw(Vector2 OFFSET)
        {
            if(myModel != null)
            {
                Global.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, rot, new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }                         //Offset chỉnh vị trí khởi tạo củam hình
        
        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN)
        {
            if (myModel != null)
            {
                Global.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, Color.White, 0, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }         //Origin chỉnh xoay tại điểm nào của hình

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOR)
        {
            if (myModel != null)
            {
                Global.spriteBatch.Draw(myModel, new Rectangle((int)(pos.X + OFFSET.X), (int)(pos.Y + OFFSET.Y), (int)dims.X, (int)dims.Y), null, COLOR, 0, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }   //Color chỉnh màu 
    }
}
