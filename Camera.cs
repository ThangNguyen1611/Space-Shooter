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
    class Camera
    {
        public Matrix transform;        //Not matrix, its MAGIC ((:
        Viewport view;
        Vector2 center;                 //Tâm cam

        public Camera(Viewport newview)
        {
            view = newview;
        }

        public void Update(GameTime gameTime, Hero Ship)
        {
            center = new Vector2(Ship.pos.X + (Ship.dims.X / 2) - Global.screenwidth/2, Ship.pos.Y + (Ship.dims.Y / 2) - Global.screenheight/2); //View sẽ nhìn vào con tàu và cố định con tàu giữa screen
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
