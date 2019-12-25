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
    public class SoundControl
    {
        public Sounds bgmusic;                                      //nhạc nền - nhân vật chính

        public List<Sounds> listsounds = new List<Sounds>();        //nhạc hiệu ứng

        public SoundControl(string MUSICPATH)                       //khởi tạo nhạc nền
        {
            if(MUSICPATH != "")
            {
                bgmusic = new Sounds("backgroundmusic", MUSICPATH, 0.2f);
                bgmusic.instance.IsLooped = true;
                bgmusic.instance.Play();
            }
        }

        public virtual void AdjustVolumn(float percent)             //chỉ chỉnh cho nhạc nền
        {
            if(bgmusic.instance != null)
            {
                bgmusic.instance.Volume = bgmusic.volumn * percent;
            }
        }

        public virtual void AddSound(string NAME, string SOUNDPATH, float VOLUMN)   //Khởi tạo cho list sounds
        {
            listsounds.Add(new Sounds(NAME, SOUNDPATH, VOLUMN));
        }
        
        public void RunSound(SoundEffect SOUND, SoundEffectInstance INSTANCE, float VOLUMN) //run sound effect
        {
            INSTANCE.Volume = VOLUMN;
            INSTANCE.Play();
            //Tại sao kh có creat instance ? // Khi AddSound khởi tạo list đã có creat instance
        }

        public void PLaySound(string SOUNDNAME)
        {
            for(int i = 0; i < listsounds.Count; i++)
            {
                if(listsounds[i].soundname == SOUNDNAME)
                {
                    RunSound(listsounds[i].sound, listsounds[i].instance, listsounds[i].volumn);
                }
            }
        }
    }
}
