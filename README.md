# Bard Music

Bard-Music defines a standard for music input files that maps to different musical notes. These musical notes are also mapped to specific keybindings in FFXIV and macro'd to play music using the in game performance actions available to Bards.

**Input File Syntax**

Input files consist of a series of notes. Notes are composed of two parts: a pitch and a length. The pitch and length are separated by a comma and surrounded by parenthesis. Notes are terminated with a semicolon. See the example below for more information.
```
(b-1,16);(b@,8);(c,4);(f#+1,2);
```
A pitch is composed of a note like 'b', 'c' or 'f', and (optionally) an octive like '-1' or '+1'. Flat notes are denoted with '@' while sharp notes are denoted with '#'.

In the example above, the first note is a 'b' on the lower octave as indicated by the '-1'. The length of the note is 1/16 of a full note as indicated by the number '16'. The second note is a flat 'b' on the main octave with a length of 1/8 of a full note.

**Hotbar Action Setup**

An image of the hotbar action setup required to use Bard Music correctly is shown below. The bottom hotbar represents the '-1' octave, the second hotbar from the bottom represents the '0', or standard, octave and so on. From left to right, the hotbar abilities should be bound to the numbers '1' through '0', '-' and '='.

There is no modifier key for the bottom hotbar. The second hotbar from the bottom is accessible with the 'Ctrl' modifier while the second and third hotbars from the bottom are accessible with the 'Shift' and 'Alt' modifiers respectively.

![Alt](https://i.imgur.com/Y2K43kO.png "Title")
