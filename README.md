# Bard Music

Bard Music is a program that plays music using Bard performance abilities. It uses its own special syntax for song input files which is documented below. There's lots of other helpful stuff too!

**Input File Syntax**

Notes are composed of two parts: a pitch and a length. The pitch and length are separated by a comma and the whole thing is surrounded by parenthesis. Additionally, notes are terminated by a semicolon. An example note is shown below.
```
(c,8);
```
This note has a pitch of 'C' and a length of 1/8 of a full note as indicated by the number '8'. A flat note is denoted by the '@' symbol while a sharp note is denoted by the '#' symbol. An example of each is shown below.
```
(b@,16);(c#,16);
```
These notes are 'b flat' and 'c sharp' respectively. They each have a length of 1/16 of a full note as indicated by the number '16'. Valid lengths for notes are as follows: '1', '2', '3', '4', '5', '8' and '16'.

The pitch of a note can be shifted to an octave that is above or below the natural octave by using a '+' or '-' symbol respectively followed by a number indicating the number of octaves. An example is shown below.
```
(a+1,16);(c#-1,16);
```
Input files consists of a series of notes wrapped in a starting and ending statement. An example input file is shown below.
```
=====BEGIN=BARD=MUSIC=SONG=FILE=====
(a,16);(b@,16);(c#,16);(a-1,8);(a+1,8);
(c#+1,16);(c#,8);(b@,16);(c#,16);
======END=BARD=MUSIC=SONG=FILE======
```
For more information about valid notes and octaves, read about the hotbar action setup.

**Hotbar Action Setup**

In order to use Bard Music correctly, Bard perfomance abilities need to be set to specific hotbar slots and special keybindings need to be set up. An annotated screen capture of the correct configuration is shown below.

![Alt](https://i.imgur.com/Y2K43kO.png "Title")

Hotbar 1 represents the '-1' octave. Hotbar 2 represents the '+/-0', or natural, octave. Hotbars 3 and 4 represent the '+1' and '+2' octaves respectively. Note: The only available note in the '+2' octave is the 'C' pitch.

There is no modifier key for the bottom hotbar. The second hotbar from the bottom is accessible with the 'Ctrl' modifier while the second and third hotbars from the bottom are accessible with the 'Shift' and 'Alt' modifiers respectively.
