# Bard Music

Bard-Music defines a standard for music input files that maps to different musical notes. These musical notes are also mapped to specific keybindings in FFXIV and macro'd to play music using the in game performance actions available to Bards.

**Input File Syntax**

Input files consists of a series of notes wrapped in a starting and ending statement. An example input file is shown below.
```
=====BEGIN=BARD=MUSIC=SONG=FILE=====
(a,16);(b@,16);(c#,16);(a-1,8);(a+1,8);
(c#+1,16);(c#,8);(b@,16);(c#,16);
======END=BARD=MUSIC=SONG=FILE======
```
Notes are composed of two parts: a pitch and a length. The pitch and length are separated by a comma and the whole thing is surrounded by parenthesis. Additionally, notes are terminated by a semicolon. An example note is shown below.
```
(c,8);
```
This note has a pitch of 'C' and a length of 1/8 of a full note as indicated by the number '8'. A flat note is denoted by the '@' symbol while a sharp note is denoted by the '#' symbol. An example of each is shown below.
```
(b@,16);(c#,16);
```
The pitch of a note can be shifted to an octave that is above or below the natural octave by using a '+' or '-' symbol respectively followed by a number indicating the number of octaves. An example is shown below.
```
(a+1,16);(c#-1,16);
```

**Hotbar Action Setup**

An image of the hotbar action setup required to use Bard Music correctly is shown below. The bottom hotbar represents the '-1' octave, the second hotbar from the bottom represents the '0', or standard, octave and so on. From left to right, the hotbar abilities should be bound to the numbers '1' through '0', '-' and '='.

There is no modifier key for the bottom hotbar. The second hotbar from the bottom is accessible with the 'Ctrl' modifier while the second and third hotbars from the bottom are accessible with the 'Shift' and 'Alt' modifiers respectively.

![Alt](https://i.imgur.com/Y2K43kO.png "Title")
