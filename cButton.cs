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
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color color = new Color(255, 255, 255, 255);

        public Vector2 size;

        public cButton(Texture2D newtexture, GraphicsDevice graphics)
        {
            texture = newtexture;

            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30 + 30);


        }

        bool down;
        public bool isclicked;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255)
                    down = false;
                if (color.A == 0)
                    down = true;

                if (down)
                    color.A += 3;
                else
                    color.A -= 3;

                if (mouse.LeftButton == ButtonState.Pressed)
                    isclicked = true;

            }
            else if (color.A < 255)
            {
                color.A += 3;
                isclicked = false;
            }


        }


        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
