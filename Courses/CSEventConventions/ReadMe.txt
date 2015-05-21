CSEventConventions Sample Application - Version 1.0 - September 13, 2007

This sample application demonstrates one custom event (MoveFile) that 
is raised when a file is moved. No file is actually moved, as doing so
is irrellevant to the objectives of this application - which is to 
demonstrate the raising of an event. The code that publishes this MoveFile
event is located in EventSample1.cs.

This sample application also demonstrates the handling of a FileSystemWatcher
event (Renamed) which is raised when a file in a watched folder is renamed.
The event subscriber code for this event is located in Form1.cs. In order for
this sample to work, you must (1) create the folder, C:\FSWatcherTest and (2) 
place one or more files into it - so that the application can rename them.
Alternatively, you could create any directory you like - in which case you 
would need to update the fsWatcherTestPath variable (in Form1.cs) to hold the 
path you created.

This sample applicaton was written as a Windows Forms application, and not a
Console application, in order to demonstrate the threading affinity of Windows
Forms UI controls (as discussed in the article). YOu can see the issues in 
action by commenting out the following line of code found in Form1.cs:

     fsWatcher.SynchronizingObject = this;

When commented out, a runtime exception will occur (as described in the article).


As with many sample applications, this one has no exception handling code, and
makes little use of any good defensive programming practices. This minimalistic
approach was taken to keep the code as clear as possible - to show only the
basic concepts relevant to the raising and handling of events.

Jeff Schaefer
September 13, 2007
