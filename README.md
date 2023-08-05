# TapPiano
windows app that uses Tap to produce a virtual musical instrument to play sounds with.

## Compile
In order to compile you need to make sure that ./TapPiano/libs/TAPWin.dll is added as a refence in the project, 
then simply compile. 

The rest of the dependencies are part of .NET.

## Getting Started

After running the project and making sure it is intialized, connect your TAP Strap and simply start Tapping, each Tap combination 
is a diffrent sound, such that each finger is a note in a sound profile and each combination of fingers is an octave.

You can use the combo box in order to change the sound profile by changing the lowest note in the octave.


You can also change the profile with the A,D buttons and the S button is used to stop the sound from playing (can also use the button instead).


## How does it work?
 
I create the sounds in memory as .WAV files and use the .NET Soundplayer to play that.

The Notes are created using a [fundemental frequency](https://en.wikipedia.org/wiki/Fundamental_frequency) without taking into acount the tember of the target musical instrument,
However if in the future better sounds would like to be created it is as simple as reading each note's WAV file and simply saving them in memory in order to create new sound profiles.

The .WAV format is in here [link](https://isip.piconepress.com/projects/speech/software/tutorials/production/fundamentals/v1.0/section_02/s02_01_p05.html), and a good explantion into how does speakers work can be listened to 
[here](https://www.youtube.com/watch?v=RxdFP31QYAg).


In general, each WAV file is very simple, in the headers we simply make sure that we understand how to read the file, and the data is simply multiplaxed locations of the speaker and by changing the location in diffrente ways fast enough we can
create sounds.


