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
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif
using Microsoft.Xna.Framework.Media;
#endregion

namespace PROJECT_SpaceShooter
{
    public class Sounds
    {
        public float volumn;
        public string soundname;
        public SoundEffect sound;
        public SoundEffectInstance instance;

        public Sounds(string NAME, string SOUNDPATH, float VOLUMN)
        {
            soundname = NAME;
            volumn = VOLUMN;
            sound = Global.content.Load<SoundEffect>(SOUNDPATH);
            instance = sound.CreateInstance();
            instance.Volume = volumn;
        }
        
    }
}
