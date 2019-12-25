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
    public class GamePlay
    {
        public World world;

        public GamePlay()
        {
            ResetWorld(null);
        }

        public virtual void Update()
        {
            world.Update();
        }

        public virtual void ResetWorld(object INFO)
        {
            world = new World(ResetWorld);
        }

        public virtual void Draw()
        {
            world.Draw(Vector2.Zero);
        }
    }
}