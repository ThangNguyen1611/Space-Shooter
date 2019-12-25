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
    public class Fireball : Projectile2d
    {


        public Fireball(Vector2 POS, Vector2 DIMS, bool ISCRITDMG, Vector2 OWNERPOS, Vector2 TARGET) : base("Fireball", POS, DIMS, ISCRITDMG, OWNERPOS, TARGET)
        {
            speed = 35;
            damage = 35 + GameGlobal.extradamage;
            if (ISCRITDMG)
                myModel = Global.content.Load<Texture2D>("FireballCrit");
        }

        public override void Update(Vector2 OFFSET, List<Unit> UNITS, List<Boss> BOSS)
        {
            base.Update(OFFSET, UNITS, BOSS);
        }



        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
