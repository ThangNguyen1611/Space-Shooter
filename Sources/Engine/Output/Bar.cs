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
    public class Bar
    {
        public Basic2d healthbar, healthbarbg;

        public Bar(string PATH, Vector2 DIMS)
        {
            //boarder = BOARDER;

            healthbar = new Basic2d(PATH, new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y));
            healthbarbg = new Basic2d(PATH + "zero", new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y));
        }

        public virtual void Update(float CURRENT, float MAX)
        {
            healthbar.dims.X = CURRENT / MAX * (healthbarbg.dims.X);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            healthbarbg.Draw(OFFSET, new Vector2(1, 1)); 
            healthbar.Draw(OFFSET, new Vector2(1, 1));
        }
    }
}
