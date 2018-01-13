using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BardMusic
{
    public class BardMusicSong
    {
        protected List<Note> Notes = new List<Note>();

        public static BardMusicSong Parse(StreamReader _inputStream)
        {
            var song = new BardMusicSong();



            return song;
        }
    }
}
