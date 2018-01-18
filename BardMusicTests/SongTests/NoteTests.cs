using Const = Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Song;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants;
using System.Diagnostics;

namespace BardMusicTests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void Note_ParseValid_AllFields()
        {
            var sr = new StringReader("(b@-1,16)");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.B, note.Pitch);
            Assert.AreEqual(Modifiers.FLAT, note.Modifier);
            Assert.AreEqual(-1, note.Octave);
            Assert.AreEqual(16, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_AllFields_RandomWhitespaceAddedBetweenFields()
        {
            var sr = new StringReader("(  G       #  1         ,       8        )");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.G, note.Pitch);
            Assert.AreEqual(Modifiers.SHARP, note.Modifier);
            Assert.AreEqual(1, note.Octave);
            Assert.AreEqual(8, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_OmitOctaveOnly()
        {
            var sr = new StringReader("( D#, 4)");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.D, note.Pitch);
            Assert.AreEqual(Modifiers.SHARP, note.Modifier);
            Assert.AreEqual(0, note.Octave);
            Assert.AreEqual(4, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_OmitModifierOnly()
        {
            var sr = new StringReader("( C+1, 2)");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.C, note.Pitch);
            Assert.AreEqual(Modifiers.NATURAL, note.Modifier);
            Assert.AreEqual(1, note.Octave);
            Assert.AreEqual(2, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_OmitAllOptionalFields()
        {
            var sr = new StringReader("( E, 1);");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.E, note.Pitch);
            Assert.AreEqual(Modifiers.NATURAL, note.Modifier);
            Assert.AreEqual(0, note.Octave);
            Assert.AreEqual(1, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_ExplicitlyZeroOctave()
        {
            var sr = new StringReader("( A+0, 2);");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.A, note.Pitch);
            Assert.AreEqual(Modifiers.NATURAL, note.Modifier);
            Assert.AreEqual(0, note.Octave);
            Assert.AreEqual(2, note.Length);
        }

        [TestMethod]
        public void Note_ParseValid_Rest()
        {
            var sr = new StringReader("( R , 16);");

            var note = Note.Parse(sr);

            Assert.AreEqual(Pitch.Rest, note.Pitch);
            Assert.AreEqual(16, note.Length);
        }

        [TestMethod]
        public void Note_ParseInvalid_InvalidPitch()
        {
            var sr = new StringReader("( Z+1#, 8);");
            var threwEx = false;

            try
            {
                var note = Note.Parse(sr);
            }
            catch(FormatException)
            {
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void Note_ParseInvalid_InvalidOctave()
        {
            var sr = new StringReader("( A+199#, 8);");
            var threwEx = false;
            try
            {
                Note.Parse(sr);
            }
            catch(ArgumentOutOfRangeException)
            {
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void Note_ParseInvalid_InvalidModifier()
        {
            var sr = new StringReader("( A+1$, 8);");
            var threwEx = false;
            try
            {
                Note.Parse(sr);
            }
            catch (FormatException)
            {
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void Note_ParseInvalid_InvalidLength()
        {
            var sr = new StringReader("( A+1#, 800000);");
            var threwEx = false;
            try
            {
                Note.Parse(sr);
            }
            catch (FormatException)
            {
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void Note_ParseInvalid_TrashInput()
        {
            var sr = new StringReader("(I am literally garbage input);");
            var threwEx = false;
            try
            {
                Note.Parse(sr);
            }
            catch (FormatException)
            {
                threwEx = true;
            }

            Assert.IsTrue(threwEx);
        }

        [TestMethod]
        public void Note_ParseValid_FromFile()
        {
            using (TextReader tr = new StreamReader(new FileStream("../../Test Files/note_valid_test.txt", FileMode.Open)))
            {
                var note = Note.Parse(tr);

                Assert.AreEqual(Pitch.A, note.Pitch);
                Assert.AreEqual(Modifiers.SHARP, note.Modifier);
                Assert.AreEqual(1, note.Octave);
                Assert.AreEqual(8, note.Length);
            }
        }
    }
}
