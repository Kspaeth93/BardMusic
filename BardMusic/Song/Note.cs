using System;
using System.IO;
using System.Windows.Forms;

namespace Song
{
    public enum Modifiers { SHARP, NATURAL, FLAT };

    public class Note
    {
        /// <summary>
        /// 0 = a,...,7 = g
        /// </summary>
        public int NoteIndex = 0;
        public Modifiers Modifier = Modifiers.NATURAL;

        protected int length = 0;
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = (value) % 17;
            }
        }

        protected int octave = 0;
        public int Octave
        {
            get
            {
                return octave;
            }
            set
            {
                if (value < -1 || value > 2)
                {
                    throw new ArgumentNullException("Out of range [-1, 2]");
                }
                octave = value;
            }
        }

        public static Note Parse(TextReader _inputStream, char curChar)
        {
            var curNoteOffsetSet = false;
            var curNoteModiferSet = false;
            var curNoteOctaveSet = false;
            var curNoteLengthSet = false;
            var negateOctave = false;
            var newNote = new Note();

            while ((curChar = (char)_inputStream.Read()) != -1)
            {
                if (curChar == ')' || curChar == '\uffff')
                {
                    break;
                }

                var alphaCharOffset = curChar - 'a';
                if (alphaCharOffset >= 0 && alphaCharOffset <= 'g' && !curNoteOffsetSet)
                {
                    newNote.NoteIndex = alphaCharOffset;
                    curNoteOffsetSet = true;
                    continue;
                }

                if (!curNoteModiferSet)
                {
                    switch (curChar)
                    {
                        case '@':
                            newNote.Modifier = Modifiers.FLAT;
                            curNoteModiferSet = true;
                            continue;
                        case '#':
                            newNote.Modifier = Modifiers.SHARP;
                            curNoteModiferSet = true;
                            continue;
                    }
                }

                if (!curNoteOctaveSet)
                {
                    if (curChar == '-' || curChar == '+')
                    {
                        if (curChar == '-')
                        {
                            negateOctave = true;
                        }
                        curChar = (char)_inputStream.Read();
                        if (curChar == -1) break;
                    }
                    var numberCharOffset = curChar - '0';
                    if (numberCharOffset >= 0 && numberCharOffset <= 2)
                    {
                        newNote.Octave = numberCharOffset * (negateOctave ? -1 : 1);
                        curNoteOctaveSet = true;
                        continue;
                    }
                    continue;
                }

                if (!curNoteLengthSet)
                {
                    var numberCharOffset = curChar - '0';
                    var curNumber = 0;

                    while (numberCharOffset >= 0 && numberCharOffset <= 9)
                    {
                        curNumber *= 10;
                        curNumber += numberCharOffset;
                        if (numberCharOffset >= 0 && numberCharOffset <= 9)
                        {
                            curChar = (char)_inputStream.Read();
                            numberCharOffset = curChar - '0';
                        }
                    }
                    ;

                    if (curNumber > 0 && curNumber <= 16)
                    {
                        newNote.Length = curNumber;
                        curNoteLengthSet = true;
                        continue;
                    }
                    continue;
                }
            }

            return newNote;
        }
    }
}