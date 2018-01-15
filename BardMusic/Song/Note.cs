using Const = Constants;
using System;
using System.IO;
using System.Windows.Forms;

namespace Song
{
    public enum Modifiers { SHARP, NATURAL, FLAT };

    public class Note
    {
        public string Pitch;
        public string Modifier;
        /// <summary>
        /// 0 = a,...,7 = g
        /// </summary>
        public int NoteIndex = 0;
        //public Modifiers Modifier = Modifiers.NATURAL;

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
                    switch (curChar.ToString())
                    {
                        case Const.Modifier.FLAT:
                            newNote.Modifier = Const.Modifier.FLAT;
                            curNoteModiferSet = true;
                            continue;
                        case Const.Modifier.SHARP:
                            newNote.Modifier = Const.Modifier.SHARP;
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

        public static string GetKeyBinding(Note note)
        {
            string keyBinding = "";

            if (note == null)
            {
                throw new ArgumentNullException("Note is undefined!");
            }
            else
            {
                switch (note.Octave)
                {
                    case Const.Octave.TOP:
                        if (note.Pitch != Const.Pitch.C)
                        {
                            throw new ArgumentOutOfRangeException("Octave +2 only contains C!");
                        }
                        else
                        {
                            keyBinding += Const.KeyBinding.ALT;
                            break;
                        }
                    case Const.Octave.HIGH:
                        keyBinding += Const.KeyBinding.SHIFT;
                        break;
                    case Const.Octave.NATURAL:
                        keyBinding += Const.KeyBinding.CTRL;
                        break;
                    case Const.Octave.LOW:
                        break;
                    default:
                        throw new ArgumentException("Invalid octave!");
                }
                switch (note.Pitch)
                {
                    case Const.Pitch.C:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                throw new ArgumentException("C@ is invalid!");
                            case Const.Modifier.SHARP:
                                keyBinding += Const.KeyBinding.TWO;
                                break;
                            default:
                                keyBinding += Const.KeyBinding.ONE;
                                break;
                        }
                        break;
                    case Const.Pitch.D:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                keyBinding += Const.KeyBinding.TWO;
                                break;
                            case Const.Modifier.SHARP:
                                keyBinding += Const.KeyBinding.FOUR;
                                break;
                            default:
                                keyBinding += Const.KeyBinding.THREE;
                                break;
                        }
                        break;
                    case Const.Pitch.E:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                keyBinding += Const.KeyBinding.FOUR;
                                break;
                            case Const.Modifier.SHARP:
                                throw new ArgumentException("E# is invalid!");
                            default:
                                keyBinding += Const.KeyBinding.FIVE;
                                break;
                        }
                        break;
                    case Const.Pitch.F:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                throw new ArgumentException("F@ is invalid!");
                            case Const.Modifier.SHARP:
                                keyBinding += Const.KeyBinding.SEVEN;
                                break;
                            default:
                                keyBinding += Const.KeyBinding.SIX;
                                break;
                        }
                        break;
                    case Const.Pitch.G:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                keyBinding += Const.KeyBinding.SEVEN;
                                break;
                            case Const.Modifier.SHARP:
                                keyBinding += Const.KeyBinding.NINE;
                                break;
                            default:
                                keyBinding += Const.KeyBinding.EIGHT;
                                break;
                        }
                        break;
                    case Const.Pitch.A:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                keyBinding += Const.KeyBinding.NINE;
                                break;
                            case Const.Modifier.SHARP:
                                keyBinding += Const.KeyBinding.MINUS;
                                break;
                            default:
                                keyBinding += Const.KeyBinding.ZERO;
                                break;
                        }
                        break;
                    case Const.Pitch.B:
                        switch (note.Modifier)
                        {
                            case Const.Modifier.FLAT:
                                keyBinding += Const.KeyBinding.MINUS;
                                break;
                            case Const.Modifier.SHARP:
                                throw new ArgumentException("B# is invalid!");
                            default:
                                keyBinding += Const.KeyBinding.EQUALS;
                                break;
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid pitch!");
                }
            }

            return keyBinding;
        }
    }
}