using Microsoft.VisualStudio.TestTools.UnitTesting;
using Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BardMusicTests
{
    [TestClass]
    public class SongTests
    {
        [TestMethod]
        public void TestParseValidEntry()
        {
            StringReader sr = new StringReader("(b@-1,16)");

            var note = Note.Parse(sr, ' ');

            Assert.AreEqual(1, note.NoteIndex);
            Assert.AreEqual(Modifiers.FLAT, note.Modifier);
            Assert.AreEqual(-1, note.Octave);
            Assert.AreEqual(16, note.Length);
        }
    }
}
