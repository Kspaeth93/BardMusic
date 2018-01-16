using Const = BardMusic.Constants;
using System;

namespace BardMusic.Song
{
    public class Note
    {
        protected String pitch = Const.Pitch.R;
        public String Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                String uppercaseValue = value.ToUpper();

                if (uppercaseValue == Const.Pitch.A || uppercaseValue == Const.Pitch.B ||
                    uppercaseValue == Const.Pitch.C || uppercaseValue == Const.Pitch.D ||
                    uppercaseValue == Const.Pitch.E || uppercaseValue == Const.Pitch.F ||
                    uppercaseValue == Const.Pitch.G || uppercaseValue == Const.Pitch.R)
                {
                    pitch = uppercaseValue;
                }
                else
                {
                    throw new ArgumentException("Invalid pitch.");
                }
            }
        }

        protected String modifier = Const.Modifier.NATURAL;
        public String Modifier
        {
            get
            {
                return modifier;
            }
            set
            {
                if (value == Const.Modifier.FLAT ||
                    value == Const.Modifier.NATURAL ||
                    value == Const.Modifier.SHARP)
                {
                    modifier = value;
                }
                else
                {
                    throw new ArgumentException("Invalid modifier.");
                }
            }
        }

        protected int octave = Const.Octave.NATURAL;
        public int Octave
        {
            get
            {
                return octave;
            }
            set
            {
                if (value >= Const.Octave.LOW && value <= Const.Octave.TOP)
                {
                    octave = value;
                }
                else
                {
                    throw new ArgumentException("Invalid octave.");
                }
            }
        }

        protected int length = Const.Length.FULL;
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                if (value == Const.Length.FULL || value == Const.Length.HALF ||
                    value == Const.Length.THIRD || value == Const.Length.QUARTER ||
                    value == Const.Length.FIFTH || value == Const.Length.EIGHTH ||
                    value == Const.Length.SIXTEENTH)
                {
                    length = value;
                }
                else
                {
                    throw new ArgumentException("Invalid length.");
                }
            }
        }

        public static Note Parse (String noteString)
        {
            if (noteString == null || noteString.Length < 1)
            {
                throw new ArgumentNullException("Note string is undefined.");
            }
            else
            {
                Note note = new Note();

                // Validate parenthesis
                if (noteString.IndexOf("(") > -1 && noteString.IndexOf(")") > -1 &&
                    noteString.IndexOf("(") == noteString.LastIndexOf("(") &&
                    noteString.IndexOf(")") == noteString.LastIndexOf(")"))
                {
                    noteString = noteString.Remove(0, 1);
                    noteString = noteString.Remove(noteString.Length - 1, 1);

                    // Validate comma separator
                    if (noteString.IndexOf(",") > -1 &&
                        noteString.IndexOf(",") == noteString.LastIndexOf(","))
                    {
                        String[] noteStringParts = noteString.Split(',');

                        if (noteStringParts[0] == null || noteStringParts[0].Length < 1)
                        {
                            throw new ArgumentException("Left half of note string is undefined.");
                        }
                        else
                        {
                            // Validate modifier
                            if (noteStringParts[0].IndexOf(Const.Modifier.FLAT) > -1 && noteStringParts[0].IndexOf(Const.Modifier.SHARP) > -1)
                            {
                                throw new ArgumentException("Too many modifiers in note string.");
                            }
                            else if (noteStringParts[0].IndexOf(Const.Modifier.FLAT) > -1 &&
                                noteStringParts[0].IndexOf(Const.Modifier.FLAT) == noteStringParts[0].LastIndexOf(Const.Modifier.FLAT))
                            {
                                note.Modifier = Const.Modifier.FLAT;
                                noteStringParts[0] = noteStringParts[0].Remove(noteStringParts[0].IndexOf(Const.Modifier.FLAT), 1);
                            }
                            else if (noteStringParts[0].IndexOf(Const.Modifier.SHARP) > -1 &&
                                noteStringParts[0].IndexOf(Const.Modifier.SHARP) == noteStringParts[0].LastIndexOf(Const.Modifier.SHARP))
                            {
                                note.Modifier = Const.Modifier.SHARP;
                                noteStringParts[0] = noteStringParts[0].Remove(noteStringParts[0].IndexOf(Const.Modifier.SHARP), 1);
                            }
                            else if (noteStringParts[0].IndexOf(Const.Modifier.FLAT) == -1 && noteStringParts[0].IndexOf(Const.Modifier.SHARP) == -1)
                            {
                                note.Modifier = Const.Modifier.NATURAL;
                            }
                            else
                            {
                                throw new ArgumentException("Too many modifiers in note string.");
                            }

                            // Validate octave
                            if (noteStringParts[0].IndexOf("+") > -1 && noteStringParts[0].IndexOf("-") > 1)
                            {
                                throw new ArgumentException("Too many octaves in note string.");
                            }
                            else if (noteStringParts[0].IndexOf("+") > -1 &&
                                noteStringParts[0].IndexOf("+") == noteStringParts[0].LastIndexOf("+"))
                            {
                                int? octave = null;
                                String octaveString;
                                try
                                {
                                    octaveString = noteStringParts[0].Substring(noteStringParts[0].IndexOf("+") + 1, 1);
                                    octave = int.Parse(octaveString);
                                }
                                catch (Exception) { }

                                if (octave == null)
                                {
                                    throw new ArgumentException("Octave is invalid.");
                                }
                                else if (octave >= 0 && octave <= 2)
                                {
                                    switch (octave)
                                    {
                                        case Const.Octave.NATURAL:
                                            note.Octave = Const.Octave.NATURAL;
                                            break;
                                        case Const.Octave.HIGH:
                                            note.Octave = Const.Octave.HIGH;
                                            break;
                                        default:
                                            note.Octave = Const.Octave.TOP;
                                            break;
                                    }

                                    noteStringParts[0] = noteStringParts[0].Remove(noteStringParts[0].IndexOf("+"), 2);
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Octave is outside of the allowed range.");
                                }
                            }
                            else if (noteStringParts[0].IndexOf("-") > -1 &&
                                noteStringParts[0].IndexOf("-") == noteStringParts[0].LastIndexOf("-"))
                            {
                                int? octave = null;
                                String octaveString;
                                try
                                {
                                    octaveString = noteStringParts[0].Substring(noteStringParts[0].IndexOf("-") + 1, 1);
                                    octave = int.Parse(octaveString);
                                }
                                catch (Exception) { }

                                if (octave == null)
                                {
                                    throw new ArgumentException("Octave is invalid.");
                                }
                                else if (octave >= 0 && octave <= 1)
                                {
                                    if (octave == 0)
                                    {
                                        note.Octave = Const.Octave.NATURAL;
                                    }
                                    else
                                    {
                                        note.Octave = Const.Octave.LOW;
                                    }

                                    noteStringParts[0] = noteStringParts[0].Remove(noteStringParts[0].IndexOf("-"), 2);
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException("Octave is outside of the allowed range.");
                                }
                            }
                            else if (noteStringParts[0].IndexOf("+") == -1 && noteStringParts[0].IndexOf("-") == -1)
                            {
                                note.Octave = Const.Octave.NATURAL;
                            }
                            else
                            {
                                throw new ArgumentException("Too many octaves in note string.");
                            }

                            // Validate pitch
                            if (noteStringParts[0].Length != 1)
                            {
                                throw new ArgumentException("Octave was invalid or pitch is invalid.");
                            }
                            else
                            {
                                noteStringParts[0] = noteStringParts[0].ToUpper();
                                switch (noteStringParts[0])
                                {
                                    case Const.Pitch.A:
                                        note.Pitch = Const.Pitch.A;
                                        break;
                                    case Const.Pitch.B:
                                        note.Pitch = Const.Pitch.B;
                                        break;
                                    case Const.Pitch.C:
                                        note.Pitch = Const.Pitch.C;
                                        break;
                                    case Const.Pitch.D:
                                        note.Pitch = Const.Pitch.D;
                                        break;
                                    case Const.Pitch.E:
                                        note.Pitch = Const.Pitch.E;
                                        break;
                                    case Const.Pitch.F:
                                        note.Pitch = Const.Pitch.F;
                                        break;
                                    case Const.Pitch.G:
                                        note.Pitch = Const.Pitch.G;
                                        break;
                                    case Const.Pitch.R:
                                        note.Modifier = Const.Modifier.NATURAL;
                                        note.Octave = Const.Octave.NATURAL;
                                        note.Pitch = Const.Pitch.R;
                                        break;
                                    default:
                                        throw new ArgumentException("Pitch is invalid.");
                                }
                            }
                        }

                        if (noteStringParts[1] == null || noteStringParts[1].Length < 1)
                        {
                            throw new ArgumentException("Right half of note string is undefined.");
                        }
                        else
                        {
                            // Validate length
                            int? length = null;
                            try
                            {
                                length = int.Parse(noteStringParts[1]);
                            }
                            catch (Exception) { }

                            if (length != null)
                            {
                                switch (length)
                                {
                                    case 1:
                                        note.Length = Const.Length.FULL;
                                        break;
                                    case 2:
                                        note.Length = Const.Length.HALF;
                                        break;
                                    case 3:
                                        note.Length = Const.Length.THIRD;
                                        break;
                                    case 4:
                                        note.Length = Const.Length.QUARTER;
                                        break;
                                    case 5:
                                        note.Length = Const.Length.FIFTH;
                                        break;
                                    case 8:
                                        note.Length = Const.Length.EIGHTH;
                                        break;
                                    case 16:
                                        note.Length = Const.Length.SIXTEENTH;
                                        break;
                                    default:
                                        throw new ArgumentException("Note length is invalid.");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Note string contains too many delimeters.");
                    }
                }
                else
                {
                    throw new ArgumentException("Note string contains too many delimeters.");
                }

                return note;
            }
        }

        public static string GetKeyBinding (Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException("Note is undefined.");
            }
            else
            {
                string keyBinding = "";

                switch (note.Octave)
                {
                    case Const.Octave.TOP:
                        if (note.Pitch != Const.Pitch.C)
                        {
                            throw new ArgumentOutOfRangeException("Octave +2 only contains C.");
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
                        throw new ArgumentException("Invalid octave.");
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
                    case Const.Pitch.R:
                        keyBinding = "";
                        break;
                    default:
                        throw new ArgumentException("Invalid pitch!");
                }

                return keyBinding;
            }
        }
    }
}