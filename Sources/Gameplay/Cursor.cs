﻿#region Includes
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
    public class Cursor : Basic2d
    {
        public Cursor(string PATH, Vector2 POS, Vector2 DIMS): base(PATH, POS, DIMS)
        {

        }

        public override void Draw(Vector2 OFFSET, Vector2 ORIGIN)
        {
            base.Draw(OFFSET,ORIGIN);
        }
    }
}
